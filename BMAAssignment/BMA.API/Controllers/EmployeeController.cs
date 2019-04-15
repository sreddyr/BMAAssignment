namespace BMAAssignment.API.Controllers
{
    using BMAAssignment.Dependency;
    using BMAAssignment.Interfaces.Services;
    using BMAAssignment.Model;
    using Microsoft.AspNetCore.Authorization;

    #region Name Spaces
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    #endregion

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {

        private IEmployeeService _employeeService;
        private readonly IConfiguration _configuration;
        
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEmployeeService employeeService
        {
            get
            {
                return _employeeService = _employeeService ?? UnityHelper.Resolve<IEmployeeService>();
            }
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public Model.Employee Login([FromBody]Login login)
        {
            var employee = employeeService.Login(login.UserName, login.Password);
            if (employee != null)
            {
                var signingKey = Convert.FromBase64String(_configuration["Jwt:SigningSecret"]);
                var expiryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = null,
                    Audience = null,
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                    Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("userid", employee.Employee_ID.ToString()),
                        new Claim("role", employee.RoleName)
                    }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                var token = jwtTokenHandler.WriteToken(jwtToken);
                employee.Token = token;
            }
            return employee;
        }

        [Route("GetEmployees")]
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<Model.Employee> GetEmployees(int companyId)
        {
            return employeeService.GetEmployees(companyId);
        }
        

    }
}
