﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shawna_Staff.Models;
using Shawna_Staff.Repos;

namespace Shawna_Staff.Controllers
{
    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        IForums repo;
        UserManager<AppUser> userManager;

        public HomeController(IForums r, UserManager<AppUser> u)
        {
            repo = r;
            userManager = u;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Overview()
        {
            return View();
        }

        [Authorize]
        public IActionResult Forum()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forum(ForumPosts model)
        {
            if (ModelState.IsValid)
            { 
                model.Name = userManager.GetUserAsync(User).Result;
                model.Name.Name = model.Name.UserName;
                model.Date = DateTime.Now;
                // Store the model in the database
                repo.AddPost(model);
            }
            else
            {
                return View(model);
            }
            return Redirect("ForumPost"); // displays all messages
        }
        


        public IActionResult ForumPost()
        {
            var posts = repo.Posts.ToList<ForumPosts>();
            return View(posts);
        }

        

        [HttpPost]
        public IActionResult ForumPost(string postTopic, string date)
        {
            List<ForumPosts> posts = null;
            if (postTopic != null)
            {
                posts = (from f in repo.Posts
                         where f.PostTopic == postTopic
                         select f).ToList();
            }
            else if (date != null)
            {
                DateTime d;
                DateTime.TryParse(date, out d);
                posts = (from f in repo.Posts
                         where f.Date.Month == d.Month &&
                         f.Date.Day == d.Day &&
                         f.Date.Year == d.Year
                         select f).ToList();
            }

            return View(posts);
        }

        [Authorize]
        public IActionResult Comment(int postId)
        {
            var commentVM = new CommentVM { PostID = postId };
            return View(commentVM);
        }

        [HttpPost]
        public RedirectToActionResult Comment(CommentVM commentVM)
        {
            var comment = new Comment { CommentText = commentVM.CommentText };
            comment.Commenter = userManager.GetUserAsync(User).Result;
            comment.Commenter.UserName = comment.Commenter.UserName;

            comment.Date = DateTime.Now;
            //retrieve the post the comment belongs to//
            var post = (from r in repo.Posts
                        where r.PostID == commentVM.PostID
                        select r).First<ForumPosts>();

            post.Comments.Add(comment);
            repo.UpdatePost(post);


            return RedirectToAction("ForumPost");
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
