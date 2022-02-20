using Common.Entities;
using ProjectManagerWeb.ViewModels.Shared;
using System.Collections.Generic;

namespace ProjectManagerWeb.ViewModels.Projects
{
    public class IndexVM
    {
        public List<Project> Items { get; set; }

        public PagerVM Pager { get; set; }
        
        public FilterVM Filter { get; set; }
    }
}
