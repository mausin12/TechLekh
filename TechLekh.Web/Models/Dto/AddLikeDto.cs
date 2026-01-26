namespace TechLekh.Web.Models.Dto
{
    public class AddLikeDto
    {
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
