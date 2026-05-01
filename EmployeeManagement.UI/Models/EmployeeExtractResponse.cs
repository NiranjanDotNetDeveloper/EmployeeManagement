namespace EmployeeManagement.UI.Models
{
    public class EmployeeExtractResponse<T>
    {
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
        public T? Data { get; set; }
    }
}
