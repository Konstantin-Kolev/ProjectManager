using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class AssignmentDetailsDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //Can be changed to use DTOs
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatorId { get; set; }
    }
}
