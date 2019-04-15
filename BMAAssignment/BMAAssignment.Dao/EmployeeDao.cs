
namespace BMAAssignment.Dao
{
    using BMAAssignment.DB;
    using BMAAssignment.Interfaces.Dao;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeeDao : IEmployeeDao
    {
        public Model.Employee Login(string username, string password)
        {
            Model.Employee employee = new Model.Employee();
            using (BMAContext context = new BMAContext())
            {
                employee = (from user in context.Employees
                            join userRole in context.UserRoles on user.Employee_ID equals userRole.Employee_ID
                            join role in context.Roles on userRole.Role_ID equals role.Role_ID
                            where user.UserName == username && user.Password == password
                            select new Model.Employee
                            {
                                Employee_ID = user.Employee_ID,
                                FirstName = user.FirstName,
                                SurName = user.SurName,
                                UserName = user.UserName,
                                Role_ID = userRole.Role_ID,
                                RoleName = role.Role1,
                                Company_ID = user.Company_ID
                            }).FirstOrDefault();
            }
            return employee;
        }

        public string InsertEmployee(Model.Employee employee)
        {
            try
            {
                using (BMAContext context = new BMAContext())
                {

                    var validUser = context.Employees.Any(x => x.Company_ID == employee.Company_ID && x.FirstName.Equals(employee.FirstName) && x.SurName.Equals(employee.SurName));

                    if (validUser)
                    {
                        return "Name already exists";
                    }

                    validUser = context.Employees.Any(x => x.UserName == employee.UserName);

                    if (validUser)
                    {
                        return "User Name already exists";
                    }

                    validUser = context.ContactInfoes.Any(x => x.EmailAddress == employee.EmailAddress);

                    if (validUser)
                    {
                        return "Email already exists";
                    }

                    var newEmp = new Employee();
                    newEmp.Company_ID = employee.Company_ID;
                    newEmp.FirstName = employee.FirstName;
                    newEmp.SurName = employee.SurName;
                    newEmp.UserName = employee.UserName;
                    newEmp.Password = employee.Password;
                    context.Employees.Add(newEmp);
                    context.SaveChanges();
                    employee.Employee_ID = newEmp.Employee_ID;

                    context.ContactInfoes.Add(new ContactInfo
                    {
                        Employee_ID = employee.Employee_ID,
                        EmailAddress = employee.EmailAddress,
                        MobileNumber = employee.MobileNumber,
                        WorkNumber = employee.WorkNumber
                    });
                    context.SaveChanges();
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        public string UpdateEmployee(Model.Employee employee)
        {
            try
            {
                using (BMAContext context = new BMAContext())
                {

                    var validUser = context.Employees.Any(x => x.Company_ID == employee.Company_ID && x.FirstName.Equals(employee.FirstName) && x.SurName.Equals(employee.SurName) && x.Employee_ID != employee.Employee_ID);

                    if (validUser)
                    {
                        return "Name already exists";
                    }

                    validUser = context.Employees.Any(x => x.UserName == employee.UserName && x.Employee_ID != employee.Employee_ID);

                    if (validUser)
                    {
                        return "User Name already exists";
                    }

                    validUser = context.ContactInfoes.Any(x => x.EmailAddress == employee.EmailAddress && x.ContactInfo_ID != employee.ContactInfo_ID);

                    if (validUser)
                    {
                        return "Email already exists";
                    }

                    var updateEmp = context.Employees.Where(e => e.Employee_ID == employee.Employee_ID).FirstOrDefault();
                    if (updateEmp != null)
                    {
                        updateEmp.Company_ID = employee.Company_ID;
                        updateEmp.FirstName = employee.FirstName;
                        updateEmp.SurName = employee.SurName;
                        updateEmp.UserName = employee.UserName;
                        updateEmp.Password = employee.Password;

                        var updateContact = context.ContactInfoes.Where(c => c.Employee_ID == employee.Employee_ID).FirstOrDefault();
                        if (updateContact != null)
                        {
                            updateContact.Employee_ID = employee.Employee_ID;
                            updateContact.EmailAddress = employee.EmailAddress;
                            updateContact.MobileNumber = employee.MobileNumber;
                            updateContact.WorkNumber = employee.WorkNumber;
                        }
                        context.SaveChanges();
                    }

                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        public IEnumerable<Model.Employee> GetEmployees(int companyId)
        {
            IEnumerable<Model.Employee> employees;
            try
            {
                using (BMAContext context = new BMAContext())
                {

                    employees = (from emp in context.Employees
                                 join contact in context.ContactInfoes on emp.Employee_ID equals contact.Employee_ID
                                 where emp.Company_ID.Equals(companyId)
                                 select new Model.Employee
                                 {
                                    Employee_ID = emp.Employee_ID,
                                    FirstName = emp.FirstName,
                                    SurName = emp.SurName,
                                    EmailAddress = contact.EmailAddress
                                 }).OrderBy(e => e.FirstName).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return employees;
        }
    }
}
