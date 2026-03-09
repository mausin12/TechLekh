using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TechLekh.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string?> GetUserNameAsync(Guid userId);

        Guid? GetCurrentUserId(ClaimsPrincipal user);

        Guid GetSignedInUserId(ClaimsPrincipal user);

        bool IsUserSignedIn(ClaimsPrincipal user);

    }
}
