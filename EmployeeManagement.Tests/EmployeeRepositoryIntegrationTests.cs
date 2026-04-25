using EmployeeManagement.Core.Domain.Entities;
using EmployeeManagement.Core.Domain.RepositoryInterface;
using EmployeeManagement.Infra.DatabaseContext;
using EmployeeManagement.Infra.RepositoriesImpl;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Numerics;
using System.Transactions;

namespace EmployeeManagement.Tests
{
    public class EmployeeRepositoryIntegrationTests
    {
        private EmployeeManagementContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<EmployeeManagementContext>().
                UseSqlServer("Data Source=DESKTOP-MMLFPI0\\SQLEXPRESS02;Initial Catalog=EmployeeManagement;Integrated Security=true;TrustServerCertificate=true;").Options;
            return new EmployeeManagementContext(options);
        }
        [Fact]
        public void GetAllEmployeeTests()
        {
            //Arrange
            using var context = GetDbContext();
            using var transaction = context.Database.BeginTransaction(); //
            var repoObject = new EmployeeRepository(context);
            var initialCount = repoObject.GetAllEmployee().Count();
            var uniqueEmail = $"test_{DateTime.UtcNow.Ticks}_{Guid.NewGuid():N}@gmail.com"; // Super unique!
            var emp = new Employee
            {
                FirstName = "Mohit",
                LastName = "Singh",
                Email = uniqueEmail,
                Phone = "9999999999",
                DeptId = 9,
                Salary = 30000,
                Status = "Active",
                LocationId = 8,
                ManagerId = 1
            };
            // Act
            var result = repoObject.AddNewEmployee(emp);
            var employees = repoObject.GetAllEmployee();
            //Assert
            employees.Should().HaveCount(initialCount+1);
            employees.Should().NotBeNull();
            employees.Should().Contain(e => e.Email == uniqueEmail);
            transaction.Rollback();
        }
        [Fact]
        public void AddNewEmployeeTest()
        {
            using var context = GetDbContext();
            var repoObject = new EmployeeRepository(context);
            using var transaction = context.Database.BeginTransaction();
            var uniqueEmail = $"test_{DateTime.UtcNow.Ticks}_{Guid.NewGuid():N}@gmail.com";
            Employee employee = new()
            {
                FirstName = "Mohit",
                LastName = "Singh",
                Email = uniqueEmail,
                Phone = "9999999999",
                DeptId = 9,
                Salary = 30000,
                Status = "Active",
                LocationId = 8,
                ManagerId = 1
            };
            var result = repoObject.AddNewEmployee(employee);
            result.Should().NotBeNull();
            result.FirstName.Should().Be("Mohit");
            result.Email.Should().Be(uniqueEmail);
            result.Salary.Should().Be(30000);
            transaction.Rollback();
        }
        [Fact]
        public void AddEmployeeInvalid()
        {
           using var context = GetDbContext();
            var repo = new EmployeeRepository(context);
            Assert.Throws<ArgumentNullException>(() =>
            {
                repo.AddNewEmployee(null);
            }
            );
        }
    }
}
