using Server.DataAccess;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    class QuestionServices : IQuestionServices
    {
        private readonly IQuestionDA _questionDA;
        public QuestionServices(IQuestionDA questionDA)
        {
            _questionDA = questionDA;
        }

        List<Question> IQuestionServices.GetQuestions(string idTest)
        {
            return _questionDA.GetQuestions(idTest);
        }
    }
}
