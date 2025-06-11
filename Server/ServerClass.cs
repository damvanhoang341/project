using Microsoft.Extensions.DependencyInjection;
using Server.DataAccess;
using Server.Models;
using Server.Services;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server
{
    public class ServerClass
    {
        public void StartProject()
        {
            try
            {
                ServiceCollection services = new ServiceCollection();

                services.AddDbContext<QuizDbContext>(options =>
                    options.UseSqlServer("Server=LAPTOP-9BIU1IM0\\sqlexpress;Database=QuizDB;uid=sa;pwd=123;Trusted_Connection=True;"));

                services.AddScoped<ITestDA, TestDASQLServer>();
                services.AddScoped<ITestServices, TestServices>();
                services.AddScoped<IQuestionDA, QuestionDASQLServer>();
                services.AddScoped<IQuestionServices, QuestionServices>();
                services.AddScoped<IAnswerDA, AnswerDASQLServer>();
                services.AddScoped<IAnswerServices, AnswerServices>();

                ServiceProvider rootProvider = services.BuildServiceProvider();

                TcpListener listener = new TcpListener(IPAddress.Any, 8888);
                listener.Start();
                Console.WriteLine("✅ Server is running...");

                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine($"📡 Client connected from {((IPEndPoint)client.Client.RemoteEndPoint).Address}:{((IPEndPoint)client.Client.RemoteEndPoint).Port}");

                        Thread clientThread = new Thread(() =>
                        {
                            using (IServiceScope scope = rootProvider.CreateScope())
                            {
                                try
                                {
                                    var testServices = scope.ServiceProvider.GetRequiredService<ITestServices>();
                                    NetworkStream ns = client.GetStream();

                                    byte[] buffer = new byte[4096];
                                    int bytesRead = ns.Read(buffer, 0, buffer.Length);
                                    string jsonRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                                    RequestObject? request = JsonSerializer.Deserialize<RequestObject>(jsonRequest);
                                    ResponseObject response = new ResponseObject();

                                    if (request?.Type == 0)
                                    {
                                        response.Type = 0;
                                        response.Data = testServices.GetTests();
                                        response.StatusCode = "success";
                                    }
                                    else if (request?.Type == 1)
                                    {
                                        var questionServices = scope.ServiceProvider.GetRequiredService<IQuestionServices>();
                                        string idTest = request.Data?.ToString() ?? string.Empty;
                                        response.Type = 1;
                                        response.Data = questionServices.GetQuestions(idTest);
                                        response.StatusCode = "success";
                                    }
                                    else if (request?.Type == 2)
                                    {
                                        var answerServices = scope.ServiceProvider.GetRequiredService<IAnswerServices>();
                                        string idQuestion = request.Data?.ToString() ?? string.Empty;
                                        response.Type = 2;
                                        response.Data = answerServices.GetAnswerByIdQuestion(idQuestion);
                                        response.StatusCode = "success";
                                    }
                                    else if (request?.Type == 3)
                                    {
                                        try
                                        {
                                            var submittedQuestions = JsonSerializer.Deserialize<List<Question>>(request.Data?.ToString() ?? "[]");
                                            int mark = 10;
                                            int perMark = 10 / submittedQuestions.Count;

                                            foreach (var q in submittedQuestions)
                                            {
                                                if (q.SelectedAnswer == null || q.SelectedAnswer.Id != q.Correctanswer)
                                                {
                                                    mark -= perMark;
                                                }
                                            }

                                            response.Type = 3;
                                            response.StatusCode = "success";
                                            response.Data = mark;
                                        }
                                        catch (Exception ex)
                                        {
                                            response.Type = 3;
                                            response.StatusCode = "error";
                                            response.Data = $"Chấm điểm thất bại: {ex.Message}";
                                        }
                                    }

                                    string jsonResponse = JsonSerializer.Serialize(response);
                                    byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
                                    ns.Write(responseBytes, 0, responseBytes.Length);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($" Lỗi xử lý client: {ex.Message}");
                                }
                                finally
                                {
                                    client.Close();
                                    Console.WriteLine(" Client disconnected.");
                                }
                            }
                        });

                        clientThread.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" Lỗi khi chấp nhận client: {ex.Message}");
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($" Server failed to start: {ex.Message}");
            }
        }
        }
}
