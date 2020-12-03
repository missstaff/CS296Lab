﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shawna_Staff.Models;
using Shawna_Staff.Repos;

namespace Shawna_Staff.Controllers
{
    public class HomeController : Controller
    {
        IForums repo;

        public HomeController(IForums r)
        {
            repo = r;
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
            // Store the model in the database
            repo.AddPost(model);

            return Redirect("ForumPost");
        }
 
        public IActionResult ForumPost()
        {
            var posts = repo.Posts.ToList<ForumPosts>();
            return View(posts);
        }

        [HttpPost]
        public IActionResult ForumPost(string postTopic)
        {

            List<ForumPosts> posts = null;
            if (postTopic != null)
            {
                posts = (from f in repo.Posts
                             where f.PostTopic == postTopic
                             select f).ToList();
            }
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
};
