namespace TechLekh.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikesAsync(Guid blogPostId);
    }
}
