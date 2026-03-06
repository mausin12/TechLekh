using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechLekh.Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task AddCommentAsync(Guid blogPostId, string description, Guid userId);
    }
}
