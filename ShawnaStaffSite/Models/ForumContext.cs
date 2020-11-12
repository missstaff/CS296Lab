using Microsoft.EntityFrameworkCore;

namespace Shawna_Staff.Models
{
    public class ForumContext : DbContext
    {
        public ForumContext(
            DbContextOptions<ForumContext> options)
            : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<ForumPosts> ForumPosts { get; set; }
    }
}
