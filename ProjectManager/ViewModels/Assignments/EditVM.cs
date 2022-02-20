using Common.Entities;
using Common.Tools;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerWeb.ViewModels.Assignments
{
    public class EditVM
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldErrorMessage)]
        public string Title { get; set; }

        [Display(Name = "Assigned user: ")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldErrorMessage)]
        public int? UserId { get; set; }

        public List<User> Users { get; set; }
    }
}
