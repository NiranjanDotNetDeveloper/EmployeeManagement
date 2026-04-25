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
        Task<IEnumerable<Employee>> GetAllEmployee();
        Task<Employee> AddNewEmployee(Employee emp);
        Task<bool> DeleteEmployee(int id);
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> UpdateEmployee(int id, Employee emp);

    }
}
