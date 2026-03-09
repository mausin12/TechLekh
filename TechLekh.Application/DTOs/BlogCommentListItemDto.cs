using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechLekh.Application.DTOs
{
    public class BlogCommentListItemDto
    {
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string Username { get; set; }
    }
}
