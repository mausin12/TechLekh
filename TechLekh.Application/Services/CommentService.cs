using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLekh.Application.Interfaces;
using TechLekh.Application.Interfaces.Repositories;
using TechLekh.Application.Interfaces.Services;
using TechLekh.Core.Domain;

namespace TechLekh.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly IBlogPostCommentRepository _blogPostCommentRepository;

        public CommentService(
            IUnitOfWork unitOfWork,
            IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            IBlogPostCommentRepository blogPostCommentRepository)
        {
            this._unitOfWork = unitOfWork;
            this._blogPostRepository = blogPostRepository;
            this._blogPostLikeRepository = blogPostLikeRepository;
            this._blogPostCommentRepository = blogPostCommentRepository;
        }

        public async Task AddCommentAsync(Guid blogPostId, string description, Guid userId)
        {

            var comment = new BlogPostComment
            {
                BlogPostId = blogPostId,
                Description = description,
                UserId = userId,
                DateAdded = DateTime.UtcNow
            };

            await _blogPostCommentRepository.AddAsync(comment);

            await _unitOfWork.Complete();
        }
    }
}
