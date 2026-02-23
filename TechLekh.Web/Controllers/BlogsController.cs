using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechLekh.Core.Domain;
using TechLekh.Web.Models.ViewModels;
using TechLekh.Application.Interfaces.Repositories;

namespace TechLekh.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IBlogPostCommentRepository _blogPostCommentRepository;

        public BlogsController(IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IBlogPostCommentRepository blogPostCommentRepository)
        {
            this._blogPostRepository = blogPostRepository;
            this._blogPostLikeRepository = blogPostLikeRepository;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._blogPostCommentRepository = blogPostCommentRepository;
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
                //Get comments for blog post
                var blogComments = await _blogPostCommentRepository.GetCommentsByBlogIdAsync(blogPost.Id);
                var blogCommentsForView = new List<BlogCommentListItemViewModel>();
                foreach (var blogComment in blogComments)
                {
                    blogCommentsForView.Add(new BlogCommentListItemViewModel
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        Username = (await _userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
                    });
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
                    Comments = blogCommentsForView,
                };
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(BlogDetaisViewModel viewModel)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var comment = new BlogPostComment
                {
                    BlogPostId = viewModel.Id,
                    Description = viewModel.CommentDescription,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };
                await _blogPostCommentRepository.AddAsync(comment);
                return RedirectToAction("Index", "Blogs", new { urlHandle = viewModel.UrlHandle });
            }
            return View();
        }
    }
}
