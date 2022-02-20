using Common.Tools;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAPI.ViewModels.Projects
{
    public class CreateVM
    {
        public int OwnerId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldErrorMessage)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
