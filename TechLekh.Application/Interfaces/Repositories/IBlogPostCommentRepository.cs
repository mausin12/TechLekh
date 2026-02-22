using TechLekh.Core.Domain;

namespace TechLekh.Application.Interfaces.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment comment);

        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogId);
    }
}
