using Microsoft.EntityFrameworkCore;
using TechLekh.Infra.Data;
using TechLekh.Core.Domain;
using TechLekh.Application.Interfaces.Repositories;

namespace TechLekh.Infra.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly TechLekhDbContext _dbContext;

        public BlogPostRepository(TechLekhDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            _dbContext.BlogPosts.Add(post);
            await _dbContext.SaveChangesAsync();
            return post;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var postFromDb = await _dbContext.BlogPosts.FindAsync(id);
            if (postFromDb != null)
            {
                _dbContext.BlogPosts.Remove(postFromDb);
                await _dbContext.SaveChangesAsync();
            }
            return postFromDb;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _dbContext.BlogPosts.Include(p => p.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await _dbContext.BlogPosts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandle(string urlHandle)
        {
            return await _dbContext.BlogPosts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost post)
        {
            var postFromDb = _dbContext.BlogPosts.Include(x => x.Tags).SingleOrDefault(p => p.Id == post.Id);
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
            await _dbContext.SaveChangesAsync();
            return postFromDb;
        }


    }
}
