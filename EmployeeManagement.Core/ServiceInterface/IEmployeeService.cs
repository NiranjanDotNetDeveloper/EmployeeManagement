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
        IEnumerable<EmployeeDTO> GetAllEmployee();
        EmployeeAddDTO AddNewEmployee(EmployeeAddDTO emp);
        bool DeleteEmployee(int id);
        EmployeeUpdateDTO GetEmployeeById(int id);
        EmployeeUpdateDTO UpdateEmployee(int id, EmployeeUpdateDTO emp);
    }
}
