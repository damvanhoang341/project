using Clients.Models;
using SharedModels;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Diagnostics;
using Server;

namespace Clients
{
    public partial class MainWindow : Window
    {
        int countQuestion;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCboTest();
        }

        void LoadCboTest()
        {
            new Thread(() =>
            {
                var tests = GetTestsFromServer();
                Dispatcher.Invoke(() =>
                {
                    cboCode.ItemsSource = null;
                    cboCode.ItemsSource = tests;
                    cboCode.DisplayMemberPath = "Code";
                    cboCode.SelectedValuePath = "Id";
                });
            }).Start();
        }

        private void CboCode_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var testSelect = cboCode.SelectedItem as Test;
            if (testSelect != null)
            {
                new Thread(() =>
                {
                    var questions = GetQuestionsByTestIdFromServer(testSelect.Id);
                    Dispatcher.Invoke(() =>
                    {
                        dgQuestions.ItemsSource = questions;
                    });
                }).Start();
            }
        }

        private void dgQuetions_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var questionSelect = dgQuestions.SelectedItem as Question;
            if (questionSelect != null)
            {
                new Thread(() =>
                {
                    var answers = GetAnswersByQuestionIdFromServer(questionSelect.Id);
                    Dispatcher.Invoke(() =>
                    {
                        dgAnswers.ItemsSource = answers;
                    });
                }).Start();
            }
        }

        private void dgAnswer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedQuestion = dgQuestions.SelectedItem as Question;
            var selectedAnswer = dgAnswers.SelectedItem as Answer;
            if (selectedQuestion != null && selectedAnswer != null)
            {
                selectedQuestion.SelectedAnswer = selectedAnswer;
                dgQuestions.Items.Refresh();
            }
        }

        private void btnSubmits_Click(object sender, RoutedEventArgs e)
        {
            var questions = dgQuestions.ItemsSource as List<Question>;
            if (questions == null || questions.Count == 0)
            {
                MessageBox.Show("⚠️ Không có câu hỏi nào để chấm.");
                return;
            }

            new Thread(() =>
            {
                var mark = SubmitAnswers(questions);
                Dispatcher.Invoke(() =>
                {
                    if (mark != null)
                        txtMarkTotal.Text = $"Điểm: {mark}";
                    else
                        MessageBox.Show("❌ Chấm điểm thất bại.");
                });
            }).Start();
        }

        public List<Test> GetTestsFromServer()
        {
            var request = new RequestObject { Type = 0 };
            return SendRequestToServer<List<Test>>(request) ?? new List<Test>();
        }

        public List<Question> GetQuestionsByTestIdFromServer(string idTest)
        {
            var request = new RequestObject { Type = 1, Data = idTest };
            return SendRequestToServer<List<Question>>(request) ?? new List<Question>();
        }

        public List<Answer> GetAnswersByQuestionIdFromServer(string idQuestion)
        {
            var request = new RequestObject { Type = 2, Data = idQuestion };
            return SendRequestToServer<List<Answer>>(request) ?? new List<Answer>();
        }

        public int? SubmitAnswers(List<Question> questions)
        {
            var request = new RequestObject { Type = 3, Data = questions };
            return SendRequestToServer<int>(request);
        }

        public T? SendRequestToServer<T>(RequestObject request)
        {
            try
            {
                using TcpClient client = new TcpClient("127.0.0.1", 8888);
                using NetworkStream stream = client.GetStream();

                string jsonRequest = JsonSerializer.Serialize(request);
                byte[] requestBytes = Encoding.UTF8.GetBytes(jsonRequest);
                stream.Write(requestBytes, 0, requestBytes.Length);

                byte[] buffer = new byte[4096];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                var response = JsonSerializer.Deserialize<ResponseObject>(jsonResponse);

                if (response?.StatusCode == "success" && response.Data is JsonElement el)
                {
                    return JsonSerializer.Deserialize<T>(el.GetRawText());
                }

                return default;
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("❌ Lỗi gửi request: " + ex.Message);
                });
                return default;
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    Process.Start(@"D:\Semester 8\PRN222\SE1838_PRN222\Lab1\Lab1\Server\bin\Debug\net8.0\Server.exe");

            //    Thread.Sleep(1000); // đợi server khởi động
            //    LoadCboTest(); // gọi sang server như bình thường
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Không thể khởi động server: {ex.Message}");
            //}


            ////var server = new ServerClass();
            ////new Thread(() =>
            ////{
            ////    server.StartProject();
            ////}).Start();
            ////LoadCboTest();
        }
    }
}
