using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Helpers
{
    public class APIResponsePaginated<T>
    {
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
        public T? Data { get; set; }
        public double? TotalPage { get; set; }
        public int? CurrentPage { get; set; }
    }
}
