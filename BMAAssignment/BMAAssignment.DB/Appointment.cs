namespace BMAAssignment.DB
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Appointment
    {
        [Key]
        public int Appointment_ID { get; set; }

        public int Employee_ID { get; set; }

        public int Company_ID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [StringLength(1000)]
        public string Info { get; set; }

        public virtual Company Company { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
