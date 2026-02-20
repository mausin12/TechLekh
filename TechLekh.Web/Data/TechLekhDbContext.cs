using Microsoft.EntityFrameworkCore;
using TechLekh.Core.Domain;

namespace TechLekh.Web.Data
{
    public class TechLekhDbContext : DbContext
    {
        public TechLekhDbContext(DbContextOptions<TechLekhDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLikes { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }
    }
}
