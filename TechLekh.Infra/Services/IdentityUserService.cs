using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLekh.Application.Interfaces.Services;

namespace TechLekh.Infra.Services
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityUserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string?> GetUserNameAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user?.UserName;
        }
    }
}
