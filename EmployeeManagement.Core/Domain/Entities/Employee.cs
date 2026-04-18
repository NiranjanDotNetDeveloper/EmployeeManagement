using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Domain.Entities
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int DeptId { get; set; }
        public Department? DepartmentInstance { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? JoinDate { get; set; }
        public string? Status { get; set; }
        public int LocationId { get; set; }
        public Location? Location { get; set; }
        public int? ManagerId { get; set; }
        [NotMapped]
        public string? Avatar { get; set; }
    }
}
