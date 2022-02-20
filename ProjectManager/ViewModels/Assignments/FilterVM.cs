using Common.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ProjectManagerWeb.ViewModels.Assignments
{
    public class FilterVM
    {
        public int ProejctId { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Assigned user")]
        public string AssignedUser { get; set; }

        public Expression<Func<Assignment, bool>> GetFitler()
        {
            return i => 
                (i.ProjectId == ProejctId) &&
                (string.IsNullOrEmpty(Title) || i.Title.Contains(Title))&&
                (string.IsNullOrEmpty(AssignedUser)|| i.AssignedUser.Username.Contains(AssignedUser));
        }
    }
}
