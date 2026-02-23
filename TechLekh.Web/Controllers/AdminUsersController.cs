using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechLekh.Web.Models.ViewModels;
using TechLekh.Application.Interfaces.Repositories;

namespace TechLekh.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminUsersController(IUserRepository userRepository,
            UserManager<IdentityUser> userManager)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = (await _userRepository.GetAll())
                .Select(u => new UserListItemViewModel
                {
                    Id = Guid.Parse(u.Id),
                    Username = u.UserName,
                    Email = u.Email
                });

            return View(new UserViewModel
            {
                Users = users
            });
        }

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel viewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = viewModel.Username,
                Email = viewModel.Email
            };
            var identityResult = await _userManager.CreateAsync(identityUser, viewModel.Password);

            if (identityResult is not null && identityResult.Succeeded)
            {
                var roles = new List<string> { "User" };
                if (viewModel.IsInAdminrole)
                {
                    roles.Add("Admin");
                }
                identityResult = await _userManager.AddToRolesAsync(identityUser, roles);

                if (identityResult is not null && identityResult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var identityResult = await _userManager.DeleteAsync(user);
                if (identityResult is not null && identityResult.Succeeded)
                {
                    return RedirectToAction("List");
                }
            }
            return View();
        }
    }
}
