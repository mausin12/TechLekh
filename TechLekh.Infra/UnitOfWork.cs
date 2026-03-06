using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLekh.Application.Interfaces;
using TechLekh.Application.Interfaces.Repositories;
using TechLekh.Infra.Data;
using TechLekh.Infra.Repositories;

namespace TechLekh.Infra
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TechLekhDbContext _dbContext;

        public UnitOfWork(TechLekhDbContext dbContext)
        {
            this._dbContext = dbContext;
            //Comments = new BlogPostCommentRepository(dbContext);
            //Likes = new BlogPostLikeRepository(dbContext);
            //BlogPosts = new BlogPostRepository(dbContext);
            //Tags = new TagRepository(dbContext);
        }
        //Commented for OCP
        //public IBlogPostCommentRepository Comments { get; private set; }

        //public IBlogPostLikeRepository Likes { get; private set; }

        //public IBlogPostRepository BlogPosts { get; private set; }

        //public ITagRepository Tags { get; private set; }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
