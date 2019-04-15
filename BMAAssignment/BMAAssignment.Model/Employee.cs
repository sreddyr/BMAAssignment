namespace BMAAssignment.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        
        public int Employee_ID { get; set; }

        public int Company_ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SurName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public int Role_ID{ get; set; }

        public string RoleName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public string MobileNumber { get; set; }

        public string WorkNumber { get; set; }

        public int ContactInfo_ID { get; set; }

        public string Token { get; set; }

        public string FullName { get; set; }

    }
}
