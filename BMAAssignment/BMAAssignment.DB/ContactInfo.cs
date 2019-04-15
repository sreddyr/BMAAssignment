namespace BMAAssignment.DB
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ContactInfo")]
    public partial class ContactInfo
    {
        [Key]
        public int ContactInfo_ID { get; set; }

        public int Employee_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        [StringLength(10)]
        public string MobileNumber { get; set; }

        [StringLength(10)]
        public string WorkNumber { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
