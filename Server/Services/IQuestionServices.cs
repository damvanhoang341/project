﻿using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    interface IQuestionServices
    {
        public List<Question> GetQuestions(string idTest);
    }
}
