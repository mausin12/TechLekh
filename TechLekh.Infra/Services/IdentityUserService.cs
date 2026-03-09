using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechLekh.Application.Interfaces.Services;

namespace TechLekh.Infra.Services
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IdentityUserService(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<string?> GetUserNameAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user?.UserName;
        }

        public Guid? GetCurrentUserId(ClaimsPrincipal user)
        {
            if (!_signInManager.IsSignedIn(user))
                return null;

            var userId = _userManager.GetUserId(user);
            return Guid.Parse(userId);
        }

        public Guid GetSignedInUserId(ClaimsPrincipal user)
        { 
            var userId = _userManager.GetUserId(user);
            return Guid.Parse(userId);
        }

        public bool IsUserSignedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }
    }
}
