using TechLekh.Core.Domain;

namespace TechLekh.Web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment comment);

        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogId);
    }
}
