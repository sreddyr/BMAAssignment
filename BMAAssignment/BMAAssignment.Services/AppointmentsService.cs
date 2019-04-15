
namespace BMAAssignment.Services
{
    using BMAAssignment.Interfaces.Dao;
    using BMAAssignment.Interfaces.Services;
    using System.Collections.Generic;

    public class AppointmentService : IAppointmentService
    {
        private IAppointmentDao _appointmentDao;

        public AppointmentService(IAppointmentDao appointmentDao)
        {
            _appointmentDao = appointmentDao;
        }

        public string InsertAppointment(Model.Appointment appointment)
        {
            return _appointmentDao.InsertAppointment(appointment);
        }

        public string UpdateAppointment(Model.Appointment appointment)
        {
            return _appointmentDao.UpdateAppointment(appointment);
        }

        public IEnumerable<Model.Appointment> GetAppointments(int userId)
        {
            return _appointmentDao.GetAppointments(userId);
        }

        public IEnumerable<Model.Appointment> GetAppointmentsByCompany(int companyId)
        {
            return _appointmentDao.GetAppointmentsByCompany(companyId);
        }

        public string DeleteAppointment(int appointmentId)
        {
            return _appointmentDao.DeleteAppointment(appointmentId);
        }

    }
}
