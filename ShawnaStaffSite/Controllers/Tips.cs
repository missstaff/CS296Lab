using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shawna_Staff.Models;

namespace Shawna_Staff.Controllers
{
    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class TipsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Lighting()
        {
            return View();
        }

        public IActionResult BestLenses()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Quiz(QuizMV quiz)
        {
            quiz.CheckAnswers();
            return View(quiz);
        }
    }
}
