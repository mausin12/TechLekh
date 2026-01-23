using Microsoft.EntityFrameworkCore;
using TechLekh.Web.Data;
using TechLekh.Web.Models.Domain;

namespace TechLekh.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly TechLekhDbContext _dbContext;

        public TagRepository(TechLekhDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        public async Task<Tag> AddAsync(Tag tag)
        {
            _dbContext.Tags.Add(tag);
            await _dbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag = await _dbContext.Tags.FindAsync(id);
            if (tag != null)
            {
                _dbContext.Tags.Remove(tag);
                await _dbContext.SaveChangesAsync();
            }
            return tag;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var tagFromDb = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Id == tag.Id);
            if (tagFromDb != null)
            {
                tagFromDb.Name = tag.Name;
                tagFromDb.DisplayName = tag.DisplayName;
                await _dbContext.SaveChangesAsync();
            }
            return tagFromDb;
        }
    }
}
