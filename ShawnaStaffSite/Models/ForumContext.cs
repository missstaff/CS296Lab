using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Shawna_Staff.Models
{
    public class ForumContext : IdentityDbContext
    {
        public ForumContext(
            DbContextOptions<ForumContext> options)
            : base(options) { }

        public DbSet<ForumPosts> ForumPosts { get; set; }

        public DbSet<ForumPosts> Comments { get; set; }

    }
}
