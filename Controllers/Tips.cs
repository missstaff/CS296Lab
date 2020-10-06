using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shawna_Staff.Controllers
{
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
    }
}
