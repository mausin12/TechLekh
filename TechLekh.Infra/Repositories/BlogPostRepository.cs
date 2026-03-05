using Microsoft.EntityFrameworkCore;
using TechLekh.Infra.Data;
using TechLekh.Core.Domain;
using TechLekh.Application.Interfaces.Repositories;

namespace TechLekh.Infra.Repositories
{
    public class BlogPostRepository : Repository<BlogPost>, IBlogPostRepository
    {

        public BlogPostRepository(TechLekhDbContext dbContext) : base(dbContext)
        {
        }

        private TechLekhDbContext DbContext => _dbContext as TechLekhDbContext;

        public async new Task<BlogPost> AddAsync(BlogPost post)
        {
            DbContext.BlogPosts.Add(post);
            await DbContext.SaveChangesAsync();
            return post;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var postFromDb = await DbContext.BlogPosts.FindAsync(id);
            if (postFromDb != null)
            {
                DbContext.BlogPosts.Remove(postFromDb);
                await DbContext.SaveChangesAsync();
            }
            return postFromDb;
        }

        public async new Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await DbContext.BlogPosts.Include(p => p.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await DbContext.BlogPosts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandle(string urlHandle)
        {
            return await DbContext.BlogPosts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost post)
        {
            var postFromDb = DbContext.BlogPosts.Include(x => x.Tags).SingleOrDefault(p => p.Id == post.Id);
            if (postFromDb != null)
            {
                postFromDb.Heading = post.Heading;
                postFromDb.PageTitle = post.PageTitle;
                postFromDb.Content = post.Content;
                postFromDb.ShortDescription = post.ShortDescription;
                postFromDb.FeaturedImageUrl = post.FeaturedImageUrl;
                postFromDb.UrlHandle = post.UrlHandle;
                postFromDb.PublishedDate = post.PublishedDate;
                postFromDb.Author = post.Author;
                postFromDb.Visible = post.Visible;
                postFromDb.Tags = post.Tags;
            }
            await DbContext.SaveChangesAsync();
            return postFromDb;
        }


    }
}
