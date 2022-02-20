using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAPI.ViewModels.Home
{
    public class LoginVM
    {
        public string Url { get; set; }

        [Display(Name = "Username: ")]
        [Required(ErrorMessage = "This field is required")]
        public string Username { get; set; }

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
    }
}
