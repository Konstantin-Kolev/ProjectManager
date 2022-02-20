using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class HoursLog : BaseEntity
    {
        public int Hours { get; set; }

        public int AssignmentId { get; set; }

        public int? UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey("AssignmentId")]
        public virtual Assignment Assignment { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
