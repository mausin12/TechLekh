using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechLekh.Core.Domain;
using TechLekh.Web.Models.ViewModels;
using TechLekh.Application.Interfaces.Repositories;
using TechLekh.Application.Services;
using TechLekh.Application.Interfaces.Services;

namespace TechLekh.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;

        public BlogsController(
            ICommentService commentService,
            IBlogService blogService,
            IUserService userService)
        {
            this._commentService = commentService;
            this._blogService = blogService;
            this._userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var userId = _userService.GetCurrentUserId(User);
            var dto = await _blogService.GetBlogDetails(urlHandle, userId);
            
            if (dto == null)
                return NotFound();
            
            var viewModel = new BlogDetaisViewModel
            {
                Id = dto.Id,
                Heading = dto.Heading,
                PageTitle = dto.PageTitle,
                Content = dto.Content,
                ShortDescription = dto.ShortDescription,
                FeaturedImageUrl = dto.FeaturedImageUrl,
                UrlHandle = dto.UrlHandle,
                PublishedDate = dto.PublishedDate,
                Author = dto.Author,
                Visible = dto.Visible,
                Tags = dto.Tags,
                TotalLikes = dto.TotalLikes,
                IsLikedByCurrentUser = dto.IsLikedByCurrentUser,
                Comments = dto.Comments.Select(c => new BlogCommentListItemViewModel
                {
                    Description = c.Description,
                    DateAdded = c.DateAdded,
                    Username = c.Username
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(BlogDetaisViewModel viewModel)
        {
            if (!_userService.IsUserSignedIn(User))
                return View();

            var userId = _userService.GetSignedInUserId(User);

            await _commentService.AddCommentAsync(viewModel.Id, viewModel.CommentDescription, userId);                

            return RedirectToAction("Index", "Blogs", new { urlHandle = viewModel.UrlHandle });  
        }
    }
}
