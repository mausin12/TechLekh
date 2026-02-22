using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLekh.Core.Domain;
using TechLekh.Web.Models.ViewModels;
using TechLekh.Application.Interfaces.Repositories;

namespace TechLekh.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this._tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(TagAddViewModel viewModel)
        {
            ValidateAddTagRequest(viewModel);
            if (!ModelState.IsValid)
            {
                return View();
            }
            var tag = new Tag
            {
                Name = viewModel.Name,
                DisplayName = viewModel.DisplayName,
            };
            await _tagRepository.AddAsync(tag);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await _tagRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await _tagRepository.GetAsync(id);
            if (tag != null)
            {
                var viewModel = new TagEditViewModel
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(viewModel);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TagEditViewModel viewModel)
        {
            var tag = new Tag
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                DisplayName = viewModel.DisplayName,
            };
            tag = await _tagRepository.UpdateAsync(tag);
            if (tag != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = viewModel.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tag = await _tagRepository.DeleteAsync(id);
            if (tag != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = id });
        }

        private void ValidateAddTagRequest(TagAddViewModel requestData)
        {
            if (requestData.Name != null && requestData.DisplayName != null)
            {
                if (requestData.Name == requestData.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name and Display Name cannot be same");
                }
            }
        }
    }
}
