using Common.Entities;
using ProjectManagerWeb.ViewModels.Shared;
using System.Collections.Generic;

namespace ProjectManagerWeb.ViewModels.Users
{
    public class IndexVM
    {
        public List<User> Items { get; set; }

        public PagerVM Pager { get; set; }

        public FilterVM Filter { get; set; }
    }
}
