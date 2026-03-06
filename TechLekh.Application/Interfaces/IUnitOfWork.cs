using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLekh.Application.Interfaces.Repositories;

namespace TechLekh.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Commented for OCP
        //IBlogPostCommentRepository Comments { get; }
        //IBlogPostLikeRepository Likes { get; }
        //IBlogPostRepository BlogPosts { get; }
        //ITagRepository Tags { get; }
        Task<int> Complete();
    }
}
