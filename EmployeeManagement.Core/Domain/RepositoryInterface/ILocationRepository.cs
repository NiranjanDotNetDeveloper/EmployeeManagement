
using EmployeeManagement.Core.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Domain.RepositoryInterface
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocations();
    }
}
