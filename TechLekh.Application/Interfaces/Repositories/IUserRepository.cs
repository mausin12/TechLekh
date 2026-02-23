using TechLekh.Core.Domain;

namespace TechLekh.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAll();
    }
}
