using Server.DataAccess;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    class AnswerServices : IAnswerServices
    {
        private readonly IAnswerDA _answerDA;
        public AnswerServices(IAnswerDA answerDA)
        {
            _answerDA = answerDA;
        }

        public List<Answer> GetAnswerByIdQuestion(string idQuention)
        {
            return _answerDA.GetAnswerByIdQuestion(idQuention);
        }
    }
}
