using TechLekh.Core.Domain;

namespace TechLekh.Application.Interfaces.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        //Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetAsync(Guid id);
        new Task<Tag> AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);
    }
}
