using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shawna_Staff.Models;

namespace Shawna_Staff.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Overview()
        {
            return View();
        }

        public IActionResult Forum()
        {
<<<<<<< HEAD
            return View();
        }

        public IActionResult Privacy()
=======
            Forums model = new Forums();
            User userName = new User();
            model.UserName = userName;

            return View(model);
        }

        [HttpPost]
        public IActionResult Forum(Forums model)
        {
            return View(model);
        }

        /*public IActionResult Privacy()
>>>>>>> parent of d3a93a8... has all requirements for lab could, could use maybe padding in another place just to demonstrate padding?
        {
            return View();
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
