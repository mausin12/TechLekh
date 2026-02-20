using TechLekh.Core.Domain;

namespace TechLekh.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikesAsync(Guid blogPostId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);

        Task<bool> HasUserLikedBlog(Guid userId, Guid blogPostId);
    }
}
