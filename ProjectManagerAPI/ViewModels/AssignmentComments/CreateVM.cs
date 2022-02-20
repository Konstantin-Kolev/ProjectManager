using Common.Tools;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAPI.ViewModels.AssignmentComments
{
    public class CreateVM
    {
        public int AssignmentId { get; set; }

        [Display(Name = "Content:")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldErrorMessage)]
        public string Content { get; set; }
    }
}
