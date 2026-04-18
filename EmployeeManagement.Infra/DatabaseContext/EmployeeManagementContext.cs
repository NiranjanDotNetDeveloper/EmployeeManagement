using EmployeeManagement.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infra.DatabaseContext
{
    public class EmployeeManagementContext:DbContext
    {
        public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext>options):base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEmployees(modelBuilder);
            ConfigureLocation(modelBuilder);
            ConfigureDepartment(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        public void ConfigureEmployees(ModelBuilder builder)
        {
            var entity = builder.Entity<Employee>();
            entity.HasKey(x => x.EmpId);
            entity.Property(x => x.EmpId).UseIdentityColumn();
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(255).IsRequired();
            entity.Property(x => x.Salary).HasColumnType("decimal(10,2)");
            entity.Property(x => x.JoinDate).HasColumnName("JoiningDate").HasDefaultValueSql("GETDATE()");
            entity.HasOne(x => x.DepartmentInstance).WithMany(x => x.Employees).HasForeignKey(x => x.DeptId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.Location).WithMany(x => x.Employees).HasForeignKey(x => x.LocationId)
              .OnDelete(DeleteBehavior.Restrict);
        }
        public void ConfigureLocation(ModelBuilder builder)
        {
            var entity = builder.Entity<Location>();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).UseIdentityColumn();
            entity.HasIndex(x => x.Name);
           
        }
        public void ConfigureDepartment(ModelBuilder builder)
        {
            var entity = builder.Entity<Department>();
            entity.HasKey(x => x.DeptId);
            entity.Property(x => x.DeptId).UseIdentityColumn();
            entity.HasIndex(x => x.DepartmentName);
        }
    }
}
