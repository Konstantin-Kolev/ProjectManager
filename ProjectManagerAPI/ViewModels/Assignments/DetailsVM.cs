using Common.DTOs;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.ViewModels.Assignments
{
    public class DetailsVM
    {
        public int AssignmentId { get; set; }

        public AssignmentDetailsDTO Assignment { get; set; }
    }
}
