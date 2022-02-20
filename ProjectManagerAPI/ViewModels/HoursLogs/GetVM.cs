using Common.DTOs;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.ViewModels.HoursLogs
{
    public class GetVM
    {
        public List<HoursLogDTO> Items { get; set; }

        public PagerVM Pager { get; set; }
    }
}
