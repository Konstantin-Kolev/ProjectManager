using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class AssignmentComment : BaseEntity
    {
        public string Content { get; set; }

        public int AssignmentId { get; set; }

        public int? CreatorId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [ForeignKey("AssignmentId")]
        public virtual Assignment Assignment { get; set; }

        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }
    }
}
