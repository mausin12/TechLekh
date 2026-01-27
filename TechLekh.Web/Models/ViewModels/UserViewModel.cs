namespace TechLekh.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<UserListItemViewModel> Users { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsInAdminrole { get; set; }
    }
}
