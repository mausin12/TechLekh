using TechLekh.Core.Domain;

namespace TechLekh.Web.Models.ViewModels
{
    public class BlogsHomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
