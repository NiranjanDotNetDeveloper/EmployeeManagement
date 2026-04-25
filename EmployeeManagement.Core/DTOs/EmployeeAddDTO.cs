using EmployeeManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.DTOs
{
    public class EmployeeAddDTO
    {
        [Required(ErrorMessage = "Please enter FirstName")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please enter LastName")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please enter Phone")]
        [Phone]
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
