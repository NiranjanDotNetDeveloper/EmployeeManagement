using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Helpers
{
    public class APIResponseWrapper<T>
    {
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
        public T? Data { get; set; }
    }
}
