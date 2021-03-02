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

        public void UpdatePost(ForumPosts post);

        //async methods for api controller//after this works might try to update the forum controller to async methods
        public Task<IQueryable<ForumPosts>> GetPostsAsync();
        public Task<ForumPosts> GetPostAsync(int? id);
        public Task<int> AddPostAsync(ForumPosts post);
        public void UpdatePostAsync(ForumPosts post, int id);
        public Task<ForumPosts> DeletePostAsync(int? id);
        public bool PostExists(int id);
        public Task SaveChangesAsync();
    }
}
