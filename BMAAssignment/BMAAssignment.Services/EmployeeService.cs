
namespace BMAAssignment.Services
{
    using BMAAssignment.Interfaces.Dao;
    using BMAAssignment.Interfaces.Services;
    using System.Collections.Generic;

    public class EmployeeService : IEmployeeService
    {
        private IEmployeeDao _employeeDao;

        public EmployeeService(IEmployeeDao employeeDao)
        {
            _employeeDao = employeeDao;
        }

        public Model.Employee Login(string username, string password)
        {
            return _employeeDao.Login(username, password);
        }
        public string InsertEmployee(Model.Employee employee)
        {
            return _employeeDao.InsertEmployee(employee);
        }

        public string UpdateEmployee(Model.Employee employee)
        {
            return _employeeDao.UpdateEmployee(employee);
        }

        public IEnumerable<Model.Employee> GetEmployees(int companyId)
        {
            return _employeeDao.GetEmployees(companyId);
        }
       

    }
}
