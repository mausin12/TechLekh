using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechLekh.Web.Models.ViewModels;
using TechLekh.Web.Repositories;

namespace TechLekh.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AdminUsersController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
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
    }
}
