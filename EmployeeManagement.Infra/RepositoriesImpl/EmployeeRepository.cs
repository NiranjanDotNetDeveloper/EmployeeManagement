using EmployeeManagement.Core.Domain.Entities;
using EmployeeManagement.Core.Domain.RepositoryInterface;
using EmployeeManagement.Infra.DatabaseContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infra.RepositoriesImpl
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeManagementContext employeeManagementContext;
        public EmployeeRepository(EmployeeManagementContext employeeManagementContext)
        {
            this.employeeManagementContext = employeeManagementContext;
        }
        public Employee AddNewEmployee(Employee emp)
        {
           if(emp== null)
            {
                throw new ArgumentNullException("Please enter a valid Emp Details");
            }
            SqlParameter firstName = new SqlParameter("@txtFirstName", emp.FirstName?? (object)DBNull.Value);
            SqlParameter lastName = new SqlParameter("@txtLastName", emp.LastName ?? (object)DBNull.Value);
            SqlParameter email = new SqlParameter("@txtEmail", emp.Email ?? (object)DBNull.Value);
            SqlParameter phone = new SqlParameter("@txtPhone", emp.Phone ?? (object)DBNull.Value);
            SqlParameter deptId = new SqlParameter("@intDeptId", emp.DeptId);
            SqlParameter salary = new SqlParameter("@salary", emp.Salary);
            SqlParameter status = new SqlParameter("@status", emp.Status??(object) DBNull.Value);
            SqlParameter locationId = new SqlParameter("@intLocationId", emp.LocationId);
            SqlParameter managerId = new SqlParameter("@intManagerId", emp.ManagerId);
            int result=employeeManagementContext.Database.
            ExecuteSqlRaw("EXEC usp_AddNewEmployee @txtFirstName,@txtLastName,@txtEmail," +
            "@txtPhone,@intDeptId,@salary,@status,@intLocationId,@intManagerId",firstName,lastName,email,phone,
            deptId,salary,status,locationId,managerId);
            if (result == 2)
                return emp;
            else
                throw new Exception("Employee with this email already exists");
        }

        public bool DeleteEmployee(int id)
        {
           if (id <= 0)
            {
                throw new ArgumentException("Please enter a valid EmpId");
            }
            SqlParameter empId = new SqlParameter("@empId", id);
            var result=employeeManagementContext.Database.ExecuteSqlRaw("EXEC usp_DeleteEmployee @empId", empId);
            if (result == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            var result=employeeManagementContext.Employees.FromSqlRaw("Exec usp_GetAllEmployee");
            return result.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Please enter a valid EmpId");
            }
            SqlParameter empId = new SqlParameter("@empId", id);
            var result = employeeManagementContext.Employees.FromSqlRaw("EXEC usp_GetEmployeeById @empId", empId).FirstOrDefault();
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("Employee does not exists.");
            }
            
        }
        public Employee UpdateEmployee(int id, Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("Please enter a valid Emp Details");
            }
            if (id == 0)
            {
                throw new ArgumentNullException("Please enter a valid Emp Id");
            }
            SqlParameter empId = new SqlParameter("@intEmpId", id);
            SqlParameter firstName = new SqlParameter("@txtFirstName", emp.FirstName ?? (object)DBNull.Value);
            SqlParameter lastName = new SqlParameter("@txtLastName", emp.LastName ?? (object)DBNull.Value);
            SqlParameter email = new SqlParameter("@txtEmail", emp.Email ?? (object)DBNull.Value);
            SqlParameter phone = new SqlParameter("@txtPhone", emp.Phone ?? (object)DBNull.Value);
            SqlParameter deptId = new SqlParameter("@intDeptId", emp.DeptId);
            SqlParameter salary = new SqlParameter("@salary", emp.Salary);
            SqlParameter status = new SqlParameter("@status", emp.Status ?? (object)DBNull.Value);
            SqlParameter locationId = new SqlParameter("@intLocationId", emp.LocationId);
            SqlParameter managerId = new SqlParameter("@intManagerId", emp.ManagerId);
            int result = employeeManagementContext.Database.
            ExecuteSqlRaw("EXEC usp_UpdateEmployee @intEmpId,@txtFirstName,@txtLastName,@txtEmail," +
            "@txtPhone,@intDeptId,@salary,@status,@intLocationId,@intManagerId", empId, firstName, lastName, email, phone,
            deptId, salary, status, locationId, managerId);
            if (result == 2)
                return emp;
            else
                throw new Exception("Employee does not exists");
        }
    }
}
