using Common.DTOs;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.ViewModels.AssignmentComments
{
    public class GetVM
    {
        public List<CommentDTO> Items { get; set; }

        public PagerVM Pager { get; set; }
    }
}
