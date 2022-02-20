using Common.DTOs;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.ViewModels.Assignments
{
    public class GetVM
    {
        public List<AssignmentDTO> Items { get; set; }

        public PagerVM Pager { get; set; }
    }
}
