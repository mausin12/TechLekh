
using Microsoft.EntityFrameworkCore;
using TechLekh.Web.Data;
using TechLekh.Web.Models.Domain;

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

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await _dbContext.BlogPostLikes.AddAsync(blogPostLike);
            await _dbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<bool> HasUserLikedBlog(Guid userId, Guid blogPostId)
        {
            var like = await _dbContext.BlogPostLikes.FirstOrDefaultAsync(x => x.UserId == userId && x.BlogPostId == blogPostId);
            return like != null;
        }
    }
}
