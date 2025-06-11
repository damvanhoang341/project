using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccess
{
    class TestDASQLServer : ITestDA
    {
        private readonly QuizDbContext _context;
           
        public TestDASQLServer(QuizDbContext context)
        {
            _context = context;
        }

        public Test? GetTestById(string idTest)
        {
            return _context.Tests.SingleOrDefault(m => m.Id == idTest);
        }

        public List<Test> GetTests()
        {
            return _context.Tests.ToList();
        }
    }
}
