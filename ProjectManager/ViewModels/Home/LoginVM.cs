using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagerWeb.ViewModels.Home
{
    public class LoginVM
    {
        public string Url { get; set; }

        [Display(Name = "Username: ")]
        [Required(ErrorMessage ="This field is required")]
        public string Username { get; set; }

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
    }
}
