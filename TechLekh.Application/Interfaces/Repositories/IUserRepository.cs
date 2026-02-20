using Microsoft.AspNetCore.Identity;

namespace TechLekh.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
