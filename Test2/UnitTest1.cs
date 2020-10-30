using Shawna_Staff.Models;
using System;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void RightAnswersTest()
        {
            var quiz = new QuizMV()
            {
                UserAnswer1 = "wide",
                UserAnswer2 = "21",
                UserAnswer3 = "True"
            };
            quiz.CheckAnswers();
            Assert.True("Right" == quiz.RightOrWrong1 && "Right" == quiz.RightOrWrong2 && "Right" == quiz.RightOrWrong3);
        }

        [Fact]
        public void WrongAnswersTest()
        {
            var quiz = new QuizMV()
            {
                UserAnswer1 = "narrow",
                UserAnswer2 = "14mm",
                UserAnswer3 = "false"
            };
            quiz.CheckAnswers();
            Assert.True("Wrong" == quiz.RightOrWrong1 && "Wrong" == quiz.RightOrWrong2 && "Wrong" == quiz.RightOrWrong3);
        }
    }
}
