
using Microsoft.EntityFrameworkCore;
using TechLekh.Infra.Data;
using TechLekh.Core.Domain;
using TechLekh.Application.Interfaces.Repositories;

namespace TechLekh.Infra.Repositories
{
    public class BlogPostLikeRepository : Repository<BlogPostLike>, IBlogPostLikeRepository
    {

        public BlogPostLikeRepository(TechLekhDbContext dbContext) : base(dbContext)
        {
        }

        private TechLekhDbContext DbContext => _dbContext as TechLekhDbContext;

        public async Task<int> GetTotalLikesAsync(Guid blogPostId)
        {
            return await DbContext.BlogPostLikes
                .CountAsync(x => x.BlogPostId == blogPostId);
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await DbContext.BlogPostLikes.AddAsync(blogPostLike);
            await DbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<bool> HasUserLikedBlog(Guid userId, Guid blogPostId)
        {
            var like = await DbContext.BlogPostLikes.FirstOrDefaultAsync(x => x.UserId == userId && x.BlogPostId == blogPostId);
            return like != null;
        }
    }
}
