using DocumentFormat.OpenXml.Spreadsheet;
using EmployeeManagement.Core.Domain.Entities;
using EmployeeManagement.Core.Domain.RepositoryInterface;
using EmployeeManagement.Infra.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Location = EmployeeManagement.Core.Domain.Entities.Location;


namespace EmployeeManagement.Infra.RepositoriesImpl
{
    public class LocationRepositoryImpl : ILocationRepository
    {
        private readonly EmployeeManagementContext _dbContext;
        public LocationRepositoryImpl(EmployeeManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            var locations=  await _dbContext.Locations.ToListAsync();
            return locations;
        }
    }
}
