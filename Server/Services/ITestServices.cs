using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    interface ITestServices
    {
        List<Test> GetTests();
        Test? GetTestById(string idTest);
    }
}
