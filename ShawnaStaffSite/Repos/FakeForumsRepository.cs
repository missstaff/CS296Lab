using Shawna_Staff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Repos
{
    public class FakeForumsRepository : IForums
    {
        List<ForumPosts> posts = new List<ForumPosts>();

        /// <summary>
        /// Forum Controller
        /// </summary>
        public IQueryable<ForumPosts> Posts { get { return posts.AsQueryable<ForumPosts>(); } }

        public void AddPost(ForumPosts forumPost)
        {
            forumPost.PostID = posts.Count;
            posts.Add(forumPost);
        }

        public ForumPosts GetForumPostsByPostTitle(string postTitle)
        {
            var post = posts.Find(p => p.PostTopic == postTitle);
            return post;
        }

        public void UpdatePost(ForumPosts post)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Api Controller
        /// </summary>
        /// <returns></returns>

        public Task<IQueryable<ForumPosts>> GetPostsAsync()
        {
            return Task.FromResult(posts.AsQueryable());
        }

        public Task<ForumPosts> GetPostAsync(int? id)
        {
            var post = posts.Find(e => e.PostID == id);
            return Task.FromResult(post);
        }

        public Task<int> AddPostAsync(ForumPosts post)
        {
            int success = 0;
            if (post != null)
            {

                post.PostID = posts.Count + 1;
                posts.Add(post);
                success = 1;
            }

            return Task.FromResult<int>(success);
        }   

        public Task<ForumPosts> DeletePostAsync(int? id)
        {
            var post = posts.Find(e => e.PostID == id);
            posts.Remove(post);
            return Task.FromResult<ForumPosts>(post);
        }

        public bool PostExists(int id)
        {
            var post = GetPostAsync(id);
            if (post != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task SaveChangesAsync()
        {
            return SaveChangesAsync();
        }

        public void UpdatePostAsync(ForumPosts post, int id)
        {
            var e = posts.Find(e => e.PostID == id);
            e.PostTopic = post.PostTopic;
            e.PostText = post.PostText;
            e.Name = post.Name;
            e.Date = post.Date;
            e.PostRating = post.PostRating;
        }
    }
}
