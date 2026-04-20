using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.DTOs
{
    public class EmployeeDTO
    {
        public int EmpId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int DeptId { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? JoinDate { get; set; }
        public string? Status { get; set; }
        public int LocationId { get; set; }
        public int? ManagerId { get; set; }
        public string? Avatar { get; set; }
    }
}
