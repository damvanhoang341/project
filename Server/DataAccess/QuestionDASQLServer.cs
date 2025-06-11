using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccess
{
    class QuestionDASQLServer : IQuestionDA
    {
        private readonly QuizDbContext _context;
        public QuestionDASQLServer(QuizDbContext context)
        {
            _context = context;
        }

        public List<Question> GetQuestions(string idTest)
        {
            return _context.Questions.Where(m => m.Tests.Any(t => t.Id == idTest)).ToList();
        }

        
    }
}
