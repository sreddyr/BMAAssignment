
namespace BMAAssignment.Dao
{
    using BMAAssignment.DB;
    using BMAAssignment.Interfaces.Dao;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AppointmentDao : IAppointmentDao
    {
        public string InsertAppointment(Model.Appointment appointment)
        {
            try
            {
                appointment.StartTime = this.ConvertTimeZone(appointment.StartTime);
                appointment.EndTime = this.ConvertTimeZone(appointment.EndTime);
                using (BMAContext context = new BMAContext())
                {

                    var validAppointment = context.Appointments.Any(x => x.Employee_ID == appointment.Employee_ID && (x.StartTime >= appointment.StartTime && x.StartTime <= appointment.EndTime || x.EndTime >= appointment.StartTime && x.EndTime <= appointment.EndTime));

                    if (validAppointment)
                    {
                        return "Appointment already booked for these dates";
                    }

                    context.Appointments.Add(new Appointment
                    {
                        Employee_ID = appointment.Employee_ID,
                        Company_ID = appointment.Company_ID,
                        StartTime = appointment.StartTime,
                        EndTime = appointment.EndTime,
                        Info = appointment.Info
                    });
                    context.SaveChanges();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        public string UpdateAppointment(Model.Appointment appointment)
        {
            try
            {
                using (BMAContext context = new BMAContext())
                {
                    appointment.StartTime = this.ConvertTimeZone(appointment.StartTime);
                    appointment.EndTime = this.ConvertTimeZone(appointment.EndTime);

                    var validAppointment = context.Appointments.Any(x => x.Employee_ID == appointment.Employee_ID && (x.StartTime >= appointment.StartTime && x.StartTime <= appointment.EndTime || x.EndTime >= appointment.StartTime && x.EndTime <= appointment.EndTime) && x.Appointment_ID != appointment.Appointment_ID);

                    if (validAppointment)
                    {
                        return "Appointment already booked for these times";
                    }

                    var updateAppointment = context.Appointments.Where(a => a.Appointment_ID == appointment.Appointment_ID).FirstOrDefault();

                    if (updateAppointment != null)
                    {
                        updateAppointment.Employee_ID = appointment.Employee_ID;
                        updateAppointment.Company_ID = appointment.Company_ID;
                        updateAppointment.StartTime = appointment.StartTime;
                        updateAppointment.EndTime = appointment.EndTime;
                        updateAppointment.Info = appointment.Info;
                    }

                    context.SaveChanges();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        public IEnumerable<Model.Appointment> GetAppointments(int userId)
        {
            IEnumerable<Model.Appointment> appointments;
            try
            {
                using (BMAContext context = new BMAContext())
                {
                    int companyId = 0;
                    var isAdmin = context.UserRoles.Any(r => r.Employee_ID.Equals(userId) && r.Role_ID.Equals((int)Model.Enums.RoleTypeEnum.RoleType.Admin));
                    if (isAdmin)
                    {
                        companyId = context.Employees.FirstOrDefault(e => e.Employee_ID.Equals(userId)).Company_ID;
                    }
                    appointments = (from appoint in context.Appointments
                                    join emp in context.Employees on appoint.Employee_ID equals emp.Employee_ID
                                    join userrole in context.UserRoles on emp.Employee_ID equals userrole.Employee_ID
                                    join contact in context.ContactInfoes on emp.Employee_ID equals contact.Employee_ID
                                    where (isAdmin) ? appoint.Company_ID.Equals(companyId) : appoint.Employee_ID.Equals(userId)
                                    select new Model.Appointment
                                    {
                                        Appointment_ID = appoint.Appointment_ID,
                                        Company_ID = appoint.Company_ID,
                                        Employee_ID = appoint.Employee_ID,
                                        FirstName = emp.FirstName,
                                        SurName = emp.SurName,
                                        UserName = emp.UserName,
                                        EmailAddress = contact.EmailAddress,
                                        MobileNumber = contact.MobileNumber,
                                        WorkNumber = contact.WorkNumber,
                                        StartTime = appoint.StartTime,
                                        EndTime = appoint.EndTime,
                                        FullName = emp.FirstName + " " + emp.SurName,
                                        Info = appoint.Info
                                    }).OrderBy(e => e.Employee_ID).ToList();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return appointments;
        }

        public IEnumerable<Model.Appointment> GetAppointmentsByCompany(int companyId)
        {
            IEnumerable<Model.Appointment> appointments;
            try
            {
                using (BMAContext context = new BMAContext())
                {

                    appointments = (from appoint in context.Appointments
                                    join emp in context.Employees on appoint.Employee_ID equals emp.Employee_ID
                                    join userrole in context.UserRoles on emp.Employee_ID equals userrole.Employee_ID
                                    join contact in context.ContactInfoes on emp.Employee_ID equals contact.Employee_ID
                                    where appoint.Company_ID.Equals(companyId)
                                    select new Model.Appointment
                                    {
                                        Appointment_ID = appoint.Appointment_ID,
                                        Company_ID = appoint.Company_ID,
                                        Employee_ID = appoint.Employee_ID,
                                        FirstName = emp.FirstName,
                                        SurName = emp.SurName,
                                        UserName = emp.UserName,
                                        EmailAddress = contact.EmailAddress,
                                        MobileNumber = contact.MobileNumber,
                                        WorkNumber = contact.WorkNumber,
                                        StartTime = appoint.StartTime,
                                        EndTime = appoint.EndTime,
                                        Info = appoint.Info
                                    }).OrderBy(e => e.Employee_ID).ToList();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return appointments;
        }

        public string DeleteAppointment(int appointmentId)
        {
            try
            {
                using (BMAContext context = new BMAContext())
                {
                    var appointment = context.Appointments.Where(a => a.Appointment_ID.Equals(appointmentId)).FirstOrDefault();
                    context.Appointments.Remove(appointment);
                    context.SaveChanges();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "error:" + ex.Message;
            }
        }

        private DateTime ConvertTimeZone(DateTime date)
        {
            var zone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            bool isDaylightSaving = zone.IsDaylightSavingTime(date);
            return TimeZoneInfo.ConvertTimeFromUtc(date, zone).ToUniversalTime();
        }
    }
}