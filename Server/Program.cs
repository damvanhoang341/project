using SharedModels;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Server.DataAccess;
using Server.Services;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerClass start = new ServerClass();
            start.StartProject();
        }
    }
}
