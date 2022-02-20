using Common.Tools;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAPI.ViewModels.Assignments
{
    public class CreateVM
    {
        public int ProjectId { get; set; }

        [Display(Name = "Title:")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldErrorMessage)]
        public string Title { get; set; }

        [Display(Name = "Description:")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldErrorMessage)]
        public string Description { get; set; }

        [Display(Name = "Assigned user: ")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldErrorMessage)]
        public int UserId { get; set; }
    }
}
