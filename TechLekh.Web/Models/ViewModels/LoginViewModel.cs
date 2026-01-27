using System.ComponentModel.DataAnnotations;

namespace TechLekh.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password should be atleast 6 characters")]
        public string Password { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
