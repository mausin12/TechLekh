using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechLekh.Web.Models.ViewModels;
using TechLekh.Web.Repositories;

namespace TechLekh.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public BlogsController(IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this._blogPostRepository = blogPostRepository;
            this._blogPostLikeRepository = blogPostLikeRepository;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await _blogPostRepository.GetByUrlHandle(urlHandle);
            var viewModel = new BlogDetaisViewModel();
            if (blogPost != null)
            {
                var totalLikes = await _blogPostLikeRepository.GetTotalLikesAsync(blogPost.Id);
                var IsLikedByCurrentUser = false;
                if (_signInManager.IsSignedIn(User))
                {
                    var userId = _userManager.GetUserId(User);
                    IsLikedByCurrentUser = await _blogPostLikeRepository.HasUserLikedBlog(Guid.Parse(userId), blogPost.Id);
                }
                viewModel = new BlogDetaisViewModel
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    ShortDescription = blogPost.ShortDescription,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                    IsLikedByCurrentUser = IsLikedByCurrentUser,
                };
            }

            return View(viewModel);
        }
    }
}
