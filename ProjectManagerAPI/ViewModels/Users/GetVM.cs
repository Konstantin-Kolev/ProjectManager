using Common.DTOs;
using Common.Entities;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.ViewModels.Users
{
    public class GetVM
    {
        public List<UserDTO> Items { get; set; }

        public PagerVM Pager { get; set; }

        public FilterVM Filter { get; set; }
    }
}
