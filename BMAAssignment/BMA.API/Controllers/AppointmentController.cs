
namespace BMA.API.Controllers
{
    using BMAAssignment.Dependency;
    using BMAAssignment.Interfaces.Services;
    using BMAAssignment.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private IAppointmentService _appointmentService;

        public IAppointmentService appointmentService
        {
            get
            {
                return _appointmentService = _appointmentService ?? UnityHelper.Resolve<IAppointmentService>();
            }
        }

        public AppointmentController()
        {
            
        }


        [Route("GetAppointments")]
        [HttpGet("{id}")]
        public IEnumerable<Appointment> GetAppointments(int id)
        {
            var result = appointmentService.GetAppointments(id);
            return result;
        }

        [HttpGet]
        [Route("GetCompanyAppointments")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<Appointment> GetCompanyAppointments(int id)
        {
            var result = appointmentService.GetAppointmentsByCompany(id);
            return result;
        }

        // POST api/values
        [HttpPost]
        [Route("InsertAppointment")]
        public string InsertAppointment([FromBody]Appointment appointment)
        {
            if (appointment.Appointment_ID > 0)
            {
                return appointmentService.UpdateAppointment(appointment); 
            }
            else
            {
                return appointmentService.InsertAppointment(appointment);
            }
        }

        
        [HttpPost]
        [Route("UpdateAppointment")]
        public string UpdateAppointment([FromBody]Appointment appointment)
        {
            return appointmentService.UpdateAppointment(appointment);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Route("Delete")]
        public string Delete(int id)
        {
            return appointmentService.DeleteAppointment(id);
        }


    }
}