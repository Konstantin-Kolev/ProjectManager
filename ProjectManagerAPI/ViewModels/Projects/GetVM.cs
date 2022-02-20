using Common.DTOs;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.ViewModels.Projects
{
    public class GetVM
    {
        public List<ProjectDTO> Items { get; set; }

        public PagerVM Pager { get; set; }
        
        public FilterVM Filter { get; set; }
    }
}
