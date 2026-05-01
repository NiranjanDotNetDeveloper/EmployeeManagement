namespace EmployeeManagement.UI.Models
{
    public class ExtractApiResponse<T>
    {
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
        public T? Data { get; set; }
        public double? TotalPage { get; set; }
        public int? CurrentPage { get; set; }

    }
}
