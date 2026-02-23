using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechLekh.Application.Interfaces.Repositories;
using TechLekh.Core.Domain;
using TechLekh.Infra.Data;

namespace TechLekh.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this._authDbContext = authDbContext;
        }
        public async Task<IEnumerable<AppUser>> GetAll()
        {
            var users = await _authDbContext.Users.ToListAsync();
            var superAdminUser = await _authDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "superadmin@techlekh.com");

            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }
            return users.Select(u =>
                new AppUser
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                });
        }
    }
}
