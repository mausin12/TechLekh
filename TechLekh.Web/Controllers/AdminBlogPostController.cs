using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechLekh.Web.Models.Domain;
using TechLekh.Web.Models.ViewModels;
using TechLekh.Web.Repositories;

namespace TechLekh.Web.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this._tagRepository = tagRepository;
            this._blogPostRepository = blogPostRepository;
        }

        public async Task<IActionResult> Add()
        {
            var tags = await _tagRepository.GetAllAsync();
            var viewModel = new BlogPostAddViewModel
            {
                Tags = tags.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                })
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BlogPostAddViewModel viewModel)
        {
            var blogPost = new BlogPost
            {
                Heading = viewModel.Heading,
                PageTitle = viewModel.PageTitle,
                Content = viewModel.Content,
                ShortDescription = viewModel.ShortDescription,
                FeaturedImageUrl = viewModel.FeaturedImageUrl,
                UrlHandle = viewModel.UrlHandle,
                PublishedDate = viewModel.PublishedDate,
                Author = viewModel.Author,
                Visible = viewModel.Visible,
            };

            var selectedTags = new List<Tag>();
            foreach (var tagId in viewModel.SelectedTags)
            {
                var tagIdAsGuid = Guid.Parse(tagId);
                var tagFromDb = await _tagRepository.GetAsync(tagIdAsGuid);
                if (tagFromDb != null)
                {
                    selectedTags.Add(tagFromDb);
                }
            }

            blogPost.Tags = selectedTags;

            await _blogPostRepository.AddAsync(blogPost);
            return View(viewModel);
        }
    }
}
