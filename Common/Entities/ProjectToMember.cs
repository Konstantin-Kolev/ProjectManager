using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class ProjectToMember : BaseEntity 
    {
        public int ProjectId { get; set; }

        public int? MemberId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [ForeignKey("MemberId")]
        public virtual User Member { get; set; }
    }
}
