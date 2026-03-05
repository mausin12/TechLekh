using Microsoft.EntityFrameworkCore;
using TechLekh.Infra.Data;
using TechLekh.Core.Domain;
using TechLekh.Application.Interfaces.Repositories;


namespace TechLekh.Infra.Repositories
{
    public class BlogPostCommentRepository : Repository<BlogPostComment>, IBlogPostCommentRepository
    {
        public BlogPostCommentRepository(TechLekhDbContext dbContext) : base(dbContext) 
        {
        }

        private TechLekhDbContext DbContext => _dbContext as TechLekhDbContext;

        public async new Task<BlogPostComment> AddAsync(BlogPostComment comment)
        {
            DbContext.BlogPostComments.Add(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogId)
        {
            return await DbContext.BlogPostComments.Where(x => x.BlogPostId == blogId).ToListAsync();
        }
    }
}
