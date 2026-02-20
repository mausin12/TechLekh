using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLekh.Core.Domain;
using TechLekh.Web.Models.Dto;
using TechLekh.Web.Repositories;

namespace TechLekh.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this._blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeDto dto)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var blogPostLike = new BlogPostLike
            {
                BlogPostId = dto.BlogPostId,
                UserId = dto.UserId,
            };
            await _blogPostLikeRepository.AddLikeForBlog(blogPostLike);
            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            var totalLikes = await _blogPostLikeRepository.GetTotalLikesAsync(blogPostId);
            return Ok(totalLikes);
        }
    }
}
