using Microsoft.EntityFrameworkCore;
using Shawna_Staff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Repos
{
    public class CommentRepo : IComment
    {
        private ForumContext context;

        public CommentRepo(ForumContext c)
        {
            context = c;
        }

        public IQueryable<Comment> Comments
        {
            get
            {
                return context.Comments.Include(e => e.Commenter);
            }
        }

        public async Task<IQueryable<Comment>> GetCommentsAsync()
        {
            return await Task.FromResult<IQueryable<Comment>>(context.Comments);
        }

        public async Task<Comment> GetCommentAsync(int? id)
        {
            return await Task.FromResult<Comment>(context.Comments.Find(id));
        }

        public async Task<int> AddCommentAsync(Comment comment)
        {
            context.Comments.Add(comment);
            return await context.SaveChangesAsync();
        }

        public async Task<Comment> DeleteCommentAsync(int? id)
        {
            Comment comment = context.Comments.FirstOrDefault(e => e.ID == id);
            if (comment != null)
            {
                context.Comments.Remove(comment);
                await context.SaveChangesAsync();
            }

            return await Task.FromResult(comment);
        }
        public bool CommentExists(int id)
        {
            var comment = GetCommentAsync(id);
            if (comment != null)
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

        public void UpdatCommentAsync(Comment comment, int id)
        {
            var e = context.Comments.Find(id);
            e.CommentText = comment.CommentText;
            e.Commenter = comment.Commenter;
            e.Date = comment.Date;
        }
    }
}
