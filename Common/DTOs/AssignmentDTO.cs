using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class AssignmentDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        //Can be changed to DTOs
        public int ProjectId { get; set; }

        public int AssignedUser { get; set; }
    }
}
