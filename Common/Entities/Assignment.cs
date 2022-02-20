using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class Assignment : BaseEntity 
    {
        public string Title { get; set; }

        public string Description { get; set; }

        //PrarentProjectId
        public int ProjectId { get; set; }

        //AssigneeId
        public int? UserId { get; set; }

        //OwnerId
        public int? CreatorId { get; set; }

        public DateTime CreatedOn { get; set; }
        
        //Could have a key for identification - string

        [ForeignKey("ProjectId")]
        public virtual Project ParentProject { get; set; }

        [ForeignKey("UserId")]
        public virtual User AssignedUser { get; set; }

        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }

        public virtual List<AssignmentComment> Comments { get; set; }

        public virtual List<HoursLog> HoursLogs { get; set; }
    }
}
