using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TechLekh.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles (User, Admin, SuperAdmin)
            var adminRoleId = "d76669e6-4f1c-4252-acb4-7e54250132ae";
            var superAdminRoleId = "aff6a05f-4f01-4d24-afc3-08bb51d1aa81";
            var userRoleId = "30734de2-7033-408c-bc46-a3be8301ae46";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name ="Admin",
                    NormalizedName="Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name ="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name ="User",
                    NormalizedName="User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed SuperAdminUser
            var superAdminId = "f29403a0-1173-4165-8c19-8d096a4b6c0b";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@techlekh.com",
                Email = "superadmin@techlekh.com",
                NormalizedEmail = "superadmin@techlekh.com".ToUpper(),
                NormalizedUserName = "superadmin@techlekh.com".ToUpper(),
                Id = superAdminId
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add all roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
