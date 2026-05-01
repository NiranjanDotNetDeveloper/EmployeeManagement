namespace EmployeeManagement.UI.Models
{
    public class EmployeeResponse
    {
        public int EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int DeptId { get; set; }
        public string? DepartmentName { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? Status { get; set; }
        public int LocationId { get; set; }
        public string? Name { get; set; }
        public int? ManagerId { get; set; }
    }
}
