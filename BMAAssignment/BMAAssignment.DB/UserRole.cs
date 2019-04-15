namespace BMAAssignment.DB
{
    using System.ComponentModel.DataAnnotations;

    public partial class UserRole
    {
        [Key]
        public int UserRole_ID { get; set; }

        public int Employee_ID { get; set; }

        public int Role_ID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Role Role { get; set; }
    }
}
