using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechLekh.Web.Models.Domain;
using TechLekh.Web.Models.ViewModels;
using TechLekh.Web.Repositories;

namespace TechLekh.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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
        
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var posts = await _blogPostRepository.GetAllAsync();
            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var post = await _blogPostRepository.GetAsync(id);
            var tags = await _tagRepository.GetAllAsync();
            if (post != null)
            {
                var viewModel = new BlogPostEditViewModel
                {
                    Id = id,
                    Heading = post.Heading,
                    PageTitle = post.PageTitle,
                    Content = post.Content,
                    ShortDescription = post.ShortDescription,
                    FeaturedImageUrl = post.FeaturedImageUrl,
                    UrlHandle = post.UrlHandle,
                    PublishedDate = post.PublishedDate,
                    Author = post.Author,
                    Visible = post.Visible,
                    Tags = tags.Select(t =>
                        new SelectListItem
                        {
                            Text = t.Name,
                            Value = t.Id.ToString()
                        }
                    ),
                    SelectedTags = post.Tags.Select(t => t.Id.ToString()).ToArray()
                };
                return View(viewModel);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogPostEditViewModel viewModel)
        {
            var post = new BlogPost
            {
                Id = viewModel.Id,
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
            foreach (var selectedTagId in viewModel.SelectedTags)
            {

                if (Guid.TryParse(selectedTagId, out var selectedTagGuid))
                {
                    var tagFromDb = await _tagRepository.GetAsync(selectedTagGuid);
                    if (tagFromDb != null)
                    {
                        selectedTags.Add(tagFromDb);
                    }
                }
            }

            post.Tags = selectedTags;
            var updatedPost = await _blogPostRepository.UpdateAsync(post);
            if (updatedPost != null)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedPost = await _blogPostRepository.DeleteAsync(id);
            if (deletedPost != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = id });
        }

    }
}
