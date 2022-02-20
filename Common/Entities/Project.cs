using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class Project : BaseEntity
    {
        public int OwnerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }
    }
}
