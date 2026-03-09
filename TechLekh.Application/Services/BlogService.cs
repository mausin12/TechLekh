using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLekh.Application.DTOs;
using TechLekh.Application.Interfaces.Repositories;
using TechLekh.Application.Interfaces.Services;

namespace TechLekh.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _likeRepository;
        private readonly IBlogPostCommentRepository _commentRepository;
        private readonly IUserService _userService;

        public BlogService(
            IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository likeRepository,
            IBlogPostCommentRepository commentRepository,
            IUserService userService)
        {
            _blogPostRepository = blogPostRepository;
            _likeRepository = likeRepository;
            _commentRepository = commentRepository;
            _userService = userService;
        }

        public async Task<BlogDetailsDto> GetBlogDetails(string urlHandle, Guid? currentUserId)
        {
            var blogPost = await _blogPostRepository.GetByUrlHandle(urlHandle);

            if (blogPost == null)
                return null;

            var totalLikes = await _likeRepository.GetTotalLikesAsync(blogPost.Id);

            bool isLikedByCurrentUser = false;

            if (currentUserId.HasValue) //User is Signed In? => _signInManager.IsSignedIn(User)
            {
                isLikedByCurrentUser = await _likeRepository
                    .HasUserLikedBlog(currentUserId.Value, blogPost.Id);
            }
            //Get comments for blog post
            var comments = await _commentRepository
                .GetCommentsByBlogIdAsync(blogPost.Id);

            var commentsForView = new List<BlogCommentListItemDto>();

            foreach (var comment in comments)
            {

                commentsForView.Add(new BlogCommentListItemDto
                {
                    Description = comment.Description,
                    DateAdded = comment.DateAdded,
                    Username = await _userService.GetUserNameAsync(comment.UserId)
                });
            }

            return new BlogDetailsDto
            {
                Id = blogPost.Id,
                Heading = blogPost.Heading,
                PageTitle = blogPost.PageTitle,
                Content = blogPost.Content,
                ShortDescription = blogPost.ShortDescription,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                Visible = blogPost.Visible,
                Tags = blogPost.Tags,
                TotalLikes = totalLikes,
                IsLikedByCurrentUser = isLikedByCurrentUser,
                Comments = commentsForView
            };
        }
    }
}
