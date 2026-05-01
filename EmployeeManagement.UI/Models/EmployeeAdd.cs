using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.UI.Models
{
    public class EmployeeAdd
    {
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
        public string? Avtar { get; set; } = null;
    }
}
