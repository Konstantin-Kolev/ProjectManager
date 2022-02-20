using Common.Entities;
using ProjectManagerWeb.ViewModels.Shared;
using System.Collections.Generic;

namespace ProjectManagerWeb.ViewModels.Projects
{
    public class DetailsVM
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public List<Assignment> Items { get; set; }
        public PagerVM Pager { get; set; }
        public Assignments.FilterVM Filter { get; set; }

        public List<ProjectToMember> Members { get; set; }

        public List<User> Users { get; set; }
    }
}
