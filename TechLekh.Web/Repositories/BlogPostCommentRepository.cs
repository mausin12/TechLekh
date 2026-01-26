using Microsoft.EntityFrameworkCore;
using TechLekh.Web.Data;
using TechLekh.Web.Models.Domain;

namespace TechLekh.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly TechLekhDbContext _dbContext;

        public BlogPostCommentRepository(TechLekhDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment comment)
        {
            _dbContext.BlogPostComments.Add(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogId)
        {
            return await _dbContext.BlogPostComments.Where(x => x.BlogPostId == blogId).ToListAsync();
        }
    }
}
