
using Microsoft.EntityFrameworkCore;
using TechLekh.Web.Data;

namespace TechLekh.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly TechLekhDbContext _dbContext;

        public BlogPostLikeRepository(TechLekhDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> GetTotalLikesAsync(Guid blogPostId)
        {
            return await _dbContext.BlogPostLikes
                .CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
