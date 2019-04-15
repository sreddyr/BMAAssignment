namespace BMAAssignment.API.Controllers
{
    using BMAAssignment.Dependency;
    using BMAAssignment.Interfaces.Services;
    using Microsoft.AspNetCore.Authorization;

    #region Name Spaces
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic; 
    #endregion

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class EmployeeController : ControllerBase
    {

        private IEmployeeService _employeeService;

        public IEmployeeService employeeService
        {
            get
            {
                return _employeeService = _employeeService ?? UnityHelper.Resolve<IEmployeeService>();
            }
        }

        // GET: api/Employee
        //[HttpGet]
        //public IEnumerable<string> GetEmployee([FromBody] compnayId)
        //{
        //    return employeeService.;
        //}

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Employee
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
