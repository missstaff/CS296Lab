using Shawna_Staff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Repos
{
    public class FakeCommentRepo : IComment
    {
        List<Comment> comments = new List<Comment>();

        public IQueryable<Comment> Comments { get { return comments.AsQueryable<Comment>(); } }

        public Task<IQueryable<Comment>> GetCommentsAsync()
        {
            return Task.FromResult(comments.AsQueryable());
        }

        public Task<Comment> GetCommentAsync(int? id)
        {
            var comment = comments.Find(e => e.ID == id);
            return Task.FromResult(comment);
        }

        public Task<int> AddCommentAsync(Comment comment)
        {
            int success = 0;
            if (comment != null)
            {

                comment.ID = comments.Count + 1;
                comments.Add(comment);
                success = 1;
            }

            return Task.FromResult<int>(success);
        }

        public Task<Comment> DeleteCommentAsync(int? id)
        {
            var comment = comments.Find(e => e.ID == id);
            comments.Remove(comment);
            return Task.FromResult<Comment>(comment);
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
            return SaveChangesAsync();
        }

        public void UpdatCommentAsync(Comment comment, int id)
        {
            var e = comments.Find(e => e.ID == id);
            e.CommentText = comment.CommentText;
            e.Commenter = comment.Commenter;
            e.Date = comment.Date;
        }
    }
}
