using EmployeeManagement.Core.Domain.Entities;
using EmployeeManagement.Core.Domain.RepositoryInterface;
using EmployeeManagement.Infra.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infra.RepositoriesImpl
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly EmployeeManagementContext _dbContext;
        public DepartmentRepository(EmployeeManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return _dbContext.Departments.ToList();
        }
    }
}
