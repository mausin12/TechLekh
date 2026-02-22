namespace TechLekh.Application.DTOs
{
    public class AddLikeDto
    {
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
