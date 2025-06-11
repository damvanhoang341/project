using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    interface IAnswerServices
    {
        List<Answer> GetAnswerByIdQuestion(string idQuention);
    }
}
