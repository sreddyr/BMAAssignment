namespace BMAAssignment.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Role
    {
       
        public int Role_ID { get; set; }

        [Required]
        public string RoleName { get; set; }
       
    }
}
