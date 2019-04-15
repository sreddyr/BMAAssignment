
namespace BMAAssignment.Interfaces.Services
{
    using System.Collections.Generic;

    public interface IAppointmentService
    {
        string InsertAppointment(Model.Appointment appointment);
        string UpdateAppointment(Model.Appointment appointment);
        IEnumerable<Model.Appointment> GetAppointments(int userId);
        IEnumerable<Model.Appointment> GetAppointmentsByCompany(int companyId);
        string DeleteAppointment(int appointmentId);
    }
}
