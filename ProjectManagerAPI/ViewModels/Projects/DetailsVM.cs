using Common.DTOs;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.ViewModels.Projects
{
    public class DetailsVM
    {
        public int ProjectId { get; set; }
        public ProjectDTO Project { get; set; }
    }
}
