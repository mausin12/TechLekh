using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechLekh.Web.Models.ViewModels;

namespace TechLekh.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid) //Check for model validation
            {
                var identityUser = new IdentityUser
                {
                    UserName = viewModel.Username,
                    Email = viewModel.Email
                };
                var identityResult = await _userManager.CreateAsync(identityUser, viewModel.Password);
                if (identityResult.Succeeded)
                {
                    var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "User");
                    if (roleIdentityResult.Succeeded)
                    {
                        //Show success Notification
                        return RedirectToAction("Register");
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var signInResult = await _signInManager.PasswordSignInAsync(viewModel.Username,
                viewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(viewModel.ReturnUrl))
                {
                    return Redirect(viewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
