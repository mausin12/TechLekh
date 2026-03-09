using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLekh.Application.DTOs;

namespace TechLekh.Application.Interfaces.Services
{
    public interface IBlogService
    {
        Task<BlogDetailsDto?> GetBlogDetails(string urlHandle, Guid? currentUserId);
    }
}
