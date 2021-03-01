using Shawna_Staff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Repos
{
    public interface IComment
    {
        IQueryable<Comment> Comments { get; }
        public Task<IQueryable<Comment>> GetCommentsAsync();
        public Task<Comment> GetCommentAsync(int? id);
        public Task<int> AddCommentAsync(Comment comment);
        public void UpdatCommentAsync(Comment comment, int id);
        public Task<Comment> DeleteCommentAsync(int? id);
        public bool CommentExists(int id);
        public Task SaveChangesAsync();
    }
}
