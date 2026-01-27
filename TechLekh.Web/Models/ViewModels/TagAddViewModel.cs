using System.ComponentModel.DataAnnotations;

namespace TechLekh.Web.Models.ViewModels
{
    public class TagAddViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }
    }
}
