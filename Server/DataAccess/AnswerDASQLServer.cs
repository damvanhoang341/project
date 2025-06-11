using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccess
{
    class AnswerDASQLServer : IAnswerDA
    {
        private readonly QuizDbContext _context;
        public AnswerDASQLServer(QuizDbContext context)
        {
            _context = context;
        }

        public List<Answer> GetAnswerByIdQuestion(string idQuention)
        {
            return _context.Answers.Where(m => m.Questionid == idQuention).ToList();
        }
    }
}
