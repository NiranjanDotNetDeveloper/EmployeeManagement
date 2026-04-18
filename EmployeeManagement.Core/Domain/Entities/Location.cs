using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Domain.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
