namespace BMAAssignment.Model
{
    using System;

    public class Appointment
    {
        public int Appointment_ID { get; set; }

        public int Employee_ID { get; set; }

        public int Company_ID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Info { get; set; }

        public string CompnayName { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string MobileNumber { get; set; }

        public string WorkNumber { get; set; }

        public string FullName { get; set; }
    }
}
