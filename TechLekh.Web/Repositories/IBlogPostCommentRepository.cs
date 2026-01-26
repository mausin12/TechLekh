using TechLekh.Web.Models.Domain;

namespace TechLekh.Web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment comment);
    }
}
