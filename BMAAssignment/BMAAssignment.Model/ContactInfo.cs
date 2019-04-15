namespace BMAAssignment.Model
{
    using System.ComponentModel.DataAnnotations;

    public class ContactInfo
    {
        public int ContactInfo_ID { get; set; }

        public int Employee_ID { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public string MobileNumber { get; set; }

        public string WorkNumber { get; set; }
        
    }
}
