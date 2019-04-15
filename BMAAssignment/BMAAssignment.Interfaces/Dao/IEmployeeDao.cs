
namespace BMAAssignment.Interfaces.Dao
{

    using System.Collections.Generic;

    public interface IEmployeeDao
    {
        Model.Employee Login(string username, string password);
        string InsertEmployee(Model.Employee employee);
        string UpdateEmployee(Model.Employee employee);
        IEnumerable<Model.Employee> GetEmployees(int companyId);
    }
}
