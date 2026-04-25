using EmployeeManagement.Core.Domain.Entities;
using EmployeeManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.ServiceInterface
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployee();
        Task<EmployeeAddDTO> AddNewEmployee(EmployeeAddDTO emp);
        Task<bool> DeleteEmployee(int id);
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<EmployeeUpdateDTO> UpdateEmployee(int id, EmployeeUpdateDTO emp);
    }
}
