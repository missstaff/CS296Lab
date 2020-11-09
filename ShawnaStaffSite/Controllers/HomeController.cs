using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shawna_Staff.Models;

namespace Shawna_Staff.Controllers
{
    public class HomeController : Controller
    {
        ForumContext context;
        public HomeController(ForumContext ctx)
        {
            context = ctx;
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
            return View();
        }

        [HttpPost]
        public IActionResult Forum(ForumPosts model)
        {
            model.Date = DateTime.Now;
            context.ForumPosts.Add(model);
            context.SaveChanges();
            return View(model);
        }
 
        public IActionResult ForumPost()
        {
            var posts = context.ForumPosts.Include(user => user.UserName).ToList<ForumPosts>();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
