namespace BMAAssignment.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        public int Company_ID { get; set; }

        [Required]
        public string CompanyName { get; set; }

    }
}
