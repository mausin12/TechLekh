using Microsoft.EntityFrameworkCore;
using System;
using TechLekh.Core.Domain;
using TechLekh.Infra.Data.EntityConfigurations;

namespace TechLekh.Infra.Data
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new BlogPostConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechLekhDbContext).Assembly);
        }
    }
}
