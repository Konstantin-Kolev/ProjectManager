using Common.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ProjectManagerWeb.ViewModels.Projects
{
    public class FilterVM
    {
        public int OwnerId { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Owner")]
        public string Owner { get; set; }

        public Expression<Func<Project, bool>> GetFilter()
        {
            return i =>
                (i.OwnerId == OwnerId) &&
                (string.IsNullOrEmpty(Title) || i.Title.Contains(Title)) &&
                (string.IsNullOrEmpty(Owner) || i.Owner.Username.Contains(Owner));
        }
    }
}
