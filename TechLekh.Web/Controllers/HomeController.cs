using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TechLekh.Web.Models;
using TechLekh.Web.Models.ViewModels;
using TechLekh.Application.Interfaces.Repositories;

namespace TechLekh.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ITagRepository _tagRepository;

        public HomeController(ILogger<HomeController> logger,
            IBlogPostRepository blogPostRepository,
            ITagRepository tagRepository)
        {
            _logger = logger;
            this._blogPostRepository = blogPostRepository;
            this._tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();
            var tags = await _tagRepository.GetAllAsync();
            var viewModel = new BlogsHomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
