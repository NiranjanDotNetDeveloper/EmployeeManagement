using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using EmployeeManagement.Core.Domain.Entities;
using EmployeeManagement.Core.Domain.RepositoryInterface;
using EmployeeManagement.Core.Helpers;
using EmployeeManagement.Infra.DatabaseContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EmployeeManagement.Infra.RepositoriesImpl
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeManagementContext employeeManagementContext;
        public EmployeeRepository(EmployeeManagementContext employeeManagementContext)
        {
            this.employeeManagementContext = employeeManagementContext;
        }
        public async Task<Employee> AddNewEmployee(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("Please enter a valid Emp Details");
            }
            SqlParameter firstName = new SqlParameter("@txtFirstName", emp.FirstName ?? (object)DBNull.Value);
            SqlParameter lastName = new SqlParameter("@txtLastName", emp.LastName ?? (object)DBNull.Value);
            SqlParameter email = new SqlParameter("@txtEmail", emp.Email ?? (object)DBNull.Value);
            SqlParameter phone = new SqlParameter("@txtPhone", emp.Phone ?? (object)DBNull.Value);
            SqlParameter deptId = new SqlParameter("@intDeptId", emp.DeptId);
            SqlParameter salary = new SqlParameter("@salary", emp.Salary);
            SqlParameter status = new SqlParameter("@status", emp.Status ?? (object)DBNull.Value);
            SqlParameter locationId = new SqlParameter("@intLocationId", emp.LocationId);
            SqlParameter managerId = new SqlParameter("@intManagerId", emp.ManagerId);
            int result = await employeeManagementContext.Database.
            ExecuteSqlRawAsync("EXEC usp_AddNewEmployee @txtFirstName,@txtLastName,@txtEmail," +
            "@txtPhone,@intDeptId,@salary,@status,@intLocationId,@intManagerId", firstName, lastName, email, phone,
            deptId, salary, status, locationId, managerId);
            if (result == 2)
                return emp;
            else
                throw new Exception("Employee with this email already exists");
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Please enter a valid EmpId");
            }
            SqlParameter empId = new SqlParameter("@empId", id);
            var result = await employeeManagementContext.Database.ExecuteSqlRawAsync("EXEC usp_DeleteEmployee @empId", empId);
            if (result == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            var result = employeeManagementContext.Employees.FromSqlRaw("Exec usp_GetAllEmployee");
            return await result.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Please enter a valid EmpId");
            }
            SqlParameter empId = new SqlParameter("@EmpId", id);
            var result = employeeManagementContext.Employees.FromSqlRaw("EXEC usp_GetEmployeeById @EmpId", empId).AsEnumerable().FirstOrDefault();
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("Employee does not exists.");
            }

        }
        public async Task<Employee> UpdateEmployee(int id, Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("Please enter a valid Emp Details");
            }
            if (id == 0)
            {
                throw new ArgumentNullException("Please enter a valid Emp Id");
            }
            SqlParameter empId = new SqlParameter("@intEmpId", id);
            SqlParameter firstName = new SqlParameter("@txtFirstName", emp.FirstName ?? (object)DBNull.Value);
            SqlParameter lastName = new SqlParameter("@txtLastName", emp.LastName ?? (object)DBNull.Value);
            SqlParameter email = new SqlParameter("@txtEmail", emp.Email ?? (object)DBNull.Value);
            SqlParameter phone = new SqlParameter("@txtPhone", emp.Phone ?? (object)DBNull.Value);
            SqlParameter deptId = new SqlParameter("@intDeptId", emp.DeptId);
            SqlParameter salary = new SqlParameter("@salary", emp.Salary);
            SqlParameter status = new SqlParameter("@status", emp.Status ?? (object)DBNull.Value);
            SqlParameter locationId = new SqlParameter("@intLocationId", emp.LocationId);
            SqlParameter managerId = new SqlParameter("@intManagerId", emp.ManagerId);
            int result = await employeeManagementContext.Database.
            ExecuteSqlRawAsync("EXEC usp_UpdateEmployee @intEmpId,@txtFirstName,@txtLastName,@txtEmail," +
            "@txtPhone,@intDeptId,@salary,@status,@intLocationId,@intManagerId", empId, firstName, lastName, email, phone,
            deptId, salary, status, locationId, managerId);
            if (result == 2)
                return emp;
            else
                throw new Exception("Employee does not exists");
        }
        public async Task<(IEnumerable<EmployeeView>,int totalCount)> GetAllEmployeeWithLocationAndDepartment(string columnName, string sortDirection, int page, string? searchText=null)
        {
            var result = employeeManagementContext.AllEmpDetails.AsQueryable();
            int recordsPerPage = 10;
            if (!string.IsNullOrEmpty(searchText))
            {
                var search = searchText.ToLower();

                result = result.Where(x =>
                    x.EmpName.ToLower().Contains(search) ||
                    x.Email.ToLower().Contains(search) ||
                    x.DepartmentName.ToLower().Contains(search) ||
                    x.Name.ToLower().Contains(search));
            }
            result = (columnName, sortDirection?.ToUpper()) switch
            {
                ("EmpName", "ASC") => result.OrderBy(x => x.EmpName),
                ("EmpName", "DESC") => result.OrderByDescending(x => x.EmpName),
                ("DepartmentName", "ASC") => result.OrderBy(x => x.DepartmentName),
                ("DepartmentName", "DESC") => result.OrderByDescending(x => x.DepartmentName),
                ("Name", "ASC") => result.OrderBy(x => x.Name),
                ("Name", "DESC") => result.OrderByDescending(x => x.Name),
                ("JoiningDate", "ASC") => result.OrderBy(x => x.JoiningDate),
                ("JoiningDate", "DESC") => result.OrderByDescending(x => x.JoiningDate),
                _ => result.OrderBy(x => x.EmpName)
            };
            int totalCount = await result.CountAsync();

            var data = await result
             .Skip((page - 1) * recordsPerPage)
            .Take(recordsPerPage)
            .ToListAsync();
            return (data, totalCount);
        }
        public async Task<byte[]> ExportEmployee()
        {
            var getAllEmployees = await employeeManagementContext.AllEmpDetails.AsNoTracking().ToListAsync();
            using var workBook=new XLWorkbook();
            var worksheet = workBook.Worksheets.Add("Employees");
            //header
            worksheet.Cell(1, 1).Value = "EmployeeName";
            worksheet.Cell(1, 2).Value = "Email";
            worksheet.Cell(1, 3).Value = "Phone";
            worksheet.Cell(1, 4).Value = "Salary";
            worksheet.Cell(1, 5).Value = "Location";
            worksheet.Cell(1, 6).Value = "Department";
            worksheet.Cell(1, 7).Value = "JoiningDate";
            worksheet.Cell(1, 8).Value = "Status";
            int row = 2;
            //Values
            foreach(var item in getAllEmployees)
            {
                worksheet.Cell(row, 1).Value = item.EmpName;
                worksheet.Cell(row, 2).Value = item.Email;
                worksheet.Cell(row, 3).Value = item.Phone;
                worksheet.Cell(row, 4).Value = item.Salary;
                worksheet.Cell(row, 5).Value = item.Name;
                worksheet.Cell(row, 6).Value = item.DepartmentName;
                worksheet.Cell(row, 7).Value = item.JoiningDate;
                worksheet.Cell(row, 8).Value = item.Status;
                row++;
            }
            var headerRange = worksheet.Range("A1:H1");
            headerRange.Style.Fill.BackgroundColor = XLColor.DarkBlue;
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.FontColor = XLColor.White;
            worksheet.Columns().AdjustToContents();
            using var memory = new MemoryStream();
            workBook.SaveAs(memory);
            return memory.ToArray();
        }
    }
}
