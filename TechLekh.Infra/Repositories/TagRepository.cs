using Microsoft.EntityFrameworkCore;
using TechLekh.Infra.Data;
using TechLekh.Core.Domain;
using TechLekh.Application.Interfaces.Repositories;

namespace TechLekh.Infra.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {

        public TagRepository(TechLekhDbContext dbContext) : base(dbContext) 
        {
        }

        private TechLekhDbContext DbContext => _dbContext as TechLekhDbContext;

        public async new Task<Tag> AddAsync(Tag tag)
        {
            DbContext.Tags.Add(tag);
            await DbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag = await DbContext.Tags.FindAsync(id);
            if (tag != null)
            {
                DbContext.Tags.Remove(tag);
                await DbContext.SaveChangesAsync();
            }
            return tag;
        }

        //public async Task<IEnumerable<Tag>> GetAllAsync()
        //{
        //    return await DbContext.Tags.ToListAsync();
        //}

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await DbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var tagFromDb = await DbContext.Tags.FirstOrDefaultAsync(t => t.Id == tag.Id);
            if (tagFromDb != null)
            {
                tagFromDb.Name = tag.Name;
                tagFromDb.DisplayName = tag.DisplayName;
                await DbContext.SaveChangesAsync();
            }
            return tagFromDb;
        }
    }
}
