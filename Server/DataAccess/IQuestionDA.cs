using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccess
{
    interface IQuestionDA
    {
        List<Question> GetQuestions(string idTest);
    }
}
