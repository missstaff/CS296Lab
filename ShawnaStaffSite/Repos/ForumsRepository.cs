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

        /// <summary>
        /// Forum Controller methods - might try to convert the contoller to async methods
        /// </summary>
        public IQueryable<ForumPosts> Posts
        {
            get
            {
                return context.ForumPosts.Include(e => e.Name)
                        .Include(e => e.Comments)
                        .ThenInclude(e => e.Commenter);
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

        public void UpdatePost(ForumPosts post)
        {
            context.ForumPosts.Update(post);
            context.SaveChanges();
        }

        /// <summary>
        /// API Controller repo methods/ should change forum controller to use these
        /// </summary>
        public async Task<IQueryable<ForumPosts>> GetPostsAsync()
        {
            return await Task.FromResult<IQueryable<ForumPosts>>(context.ForumPosts);
        }

        public async Task<ForumPosts> GetPostAsync(int? id)
        {
            return await Task.FromResult<ForumPosts>(context.ForumPosts.Find(id));
        }


        public async Task<int> AddPostAsync(ForumPosts post)
        {
            context.ForumPosts.Add(post);
            return await context.SaveChangesAsync();
        }

        public async Task<ForumPosts> DeletePostAsync(int? id)
        {
            ForumPosts movie = context.ForumPosts.FirstOrDefault(e => e.PostID == id);
            if (movie != null)
            {
                context.ForumPosts.Remove(movie);
                await context.SaveChangesAsync();
            }

            return await Task.FromResult(movie);
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
            return context.SaveChangesAsync();
        }

        public void UpdatePostAsync(ForumPosts post, int id)
        {
            var e = context.ForumPosts.Find(id);
            e.PostTopic = post.PostTopic;
            e.PostText = post.PostText;
            e.Name = post.Name;
            e.Date = post.Date;
            e.PostRating = post.PostRating;
        }
    }
}
