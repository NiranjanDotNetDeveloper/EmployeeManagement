using EmployeeManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Domain.RepositoryInterface
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployee();
        Employee AddNewEmployee(Employee emp);
        bool DeleteEmployee(int id);
        Employee GetEmployeeById(int id);
        Employee UpdateEmployee(int id, Employee emp);

    }
}
