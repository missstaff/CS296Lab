using Shawna_Staff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Repos
{
    public interface IForums
    {
        IQueryable<ForumPosts> Posts { get; }
        public void AddPost(ForumPosts forumPost);

        public ForumPosts GetForumPostsByPostTitle(string postTitle);
    }
}
