using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Models
{
    public class QuizMV
    {
        public String UserAnswer1 { get; set; }
        public String RightOrWrong1 { get; set; }

        public String UserAnswer2 { get; set; }
        public String RightOrWrong2 { get; set; }

        public String UserAnswer3 { get; set; }
        public String RightOrWrong3 { get; set; }

        public void CheckAnswers()
        {
            RightOrWrong1 = UserAnswer1 == "wide" ? "Right" : "Wrong";
            RightOrWrong2 = UserAnswer2 == "21" ? "Right" : "Wrong";
            RightOrWrong3 = UserAnswer3 == "True" ? "Right" : "Wrong";
        }
    }
}
