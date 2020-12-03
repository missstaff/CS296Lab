using Microsoft.EntityFrameworkCore;
using Shawna_Staff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Repos
{
    public class ForumsRepository : IForums
    {
        private ForumContext context;
        public ForumsRepository(ForumContext c)
        {
            context = c;
        }

        public IQueryable<ForumPosts> Posts
        {
            get
            {
                return context.ForumPosts.Include(e => e.Name);
            }
        }

        public void AddPost(ForumPosts forumPost)
        {
            context.ForumPosts.Add(forumPost);
            context.SaveChanges();
        }

        public ForumPosts GetForumPostsByPostTitle(string postTitle)
        {
            var posts = context.ForumPosts.Find(postTitle);
            return posts;
        }
    }
}
