using Server.DataAccess;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    class TestServices : ITestServices
    {
        private readonly ITestDA _testDA;
        public TestServices(ITestDA testDA)
        {
            _testDA = testDA;
        }

        public Test? GetTestById(string idTest)
        {
            return _testDA.GetTestById(idTest);
        }

        public List<Test> GetTests()
        {
            return _testDA.GetTests();
        }
    }
}
