using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Domain.Entities
{
    public class Department
    {
        public int DeptId { get; set; }
        public string? DepartmentName { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
