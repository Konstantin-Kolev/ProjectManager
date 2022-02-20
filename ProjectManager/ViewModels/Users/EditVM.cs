using Common.Tools;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerWeb.ViewModels.Users
{
    public class EditVM
    {
        public int Id { get; set; }

        [Display(Name = "Username: ")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldErrorMessage)]
        public string Username { get; set; }

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldErrorMessage)]
        public string Password { get; set; }

        [Display(Name = "First Name: ")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldErrorMessage)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name: ")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldErrorMessage)]
        public string LastName { get; set; }
    }
}
