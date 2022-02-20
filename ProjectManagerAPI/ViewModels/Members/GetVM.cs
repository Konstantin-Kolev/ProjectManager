using Common.DTOs;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.ViewModels.Members
{
    public class GetVM
    {
        public List<MemberDTO> Items { get; set; }

        public PagerVM Pager { get; set; }
    }
}
