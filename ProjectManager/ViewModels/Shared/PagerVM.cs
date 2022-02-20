using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagerWeb.ViewModels.Shared
{
    public class PagerVM
    {
        public int Page { get; set; }

        public int ItemsPerPage { get; set; }

        public int PagesCount { get; set; }
    }
}
