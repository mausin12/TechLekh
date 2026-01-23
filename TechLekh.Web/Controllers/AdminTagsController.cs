using Microsoft.AspNetCore.Mvc;
using TechLekh.Web.Models.Domain;
using TechLekh.Web.Models.ViewModels;

namespace TechLekh.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(TagAddViewModel viewModel)
        {
            return View();
        }
    }
}
