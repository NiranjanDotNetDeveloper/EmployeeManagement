using EmployeeManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly HttpClient httpClient;
        public EmployeeController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost/api/Employee/");
        }
        [Route("/")]
        public async Task<IActionResult> GetAllEmployees(string colName="EmpName", string sortOrder ="ASC",int page=1,string searchText=null)
       {
            var clientResponse = await httpClient.GetAsync($"GetEmployeeWithLocationAndDepartment/{colName}/{sortOrder}/{page}/{searchText}");
            if (clientResponse.IsSuccessStatusCode)
            {
                var content=await clientResponse.Content.ReadAsStringAsync();
                var convertToModel = JsonSerializer.Deserialize<ExtractApiResponse<List<EmployeeResponse>>>(content,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive=true});
                ViewBag.ToTalPages = convertToModel.TotalPage;
                ViewBag.CurrentPage = convertToModel.CurrentPage;
                ViewBag.Sort = sortOrder;
                return View(convertToModel.Data);
            }
            return View();
        }
        public async Task<IActionResult> ExportEmployees()
        {
            var clientResponse = await httpClient.GetAsync("DonwloadExcelReport");
            if (clientResponse.IsSuccessStatusCode)
            {
                var content = await clientResponse.Content.ReadAsByteArrayAsync();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
            }
            return BadRequest();
        }
        public IActionResult AddNewEmployee()
        {
            EmployeeAdd employee = new();
            return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewEmployee(EmployeeAdd employee)
        {
            var employeeContent = JsonSerializer.Serialize(employee);
            var stringContent = new StringContent(employeeContent,Encoding.UTF8,"application/json");
            var response = await httpClient.PostAsync("AddNewEmployee", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllEmployees");
            }
            return View();
        }
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var contentToBeUpdate = await httpClient.GetAsync($"GetEmployeeById/{id}");
            if (contentToBeUpdate.IsSuccessStatusCode)
            {
                var responseInString = await contentToBeUpdate.Content.ReadAsStringAsync();
                var contentResponse = JsonSerializer.Deserialize<EmployeeExtractResponse<EmployeeUpdate>>(responseInString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive=true});
                return View(contentResponse.Data);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(int id,EmployeeUpdate employee)
        {
            var employeeContent = JsonSerializer.Serialize(employee);
            var stringContent = new StringContent(employeeContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"UpdateEmployee/{id}", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllEmployees");
            }
            return View();
        }
        public async Task<IActionResult>RemoveEmployee(int id)
        {
            var contentToBeUpdate = await httpClient.DeleteAsync($"DeleteEmployee/{id}");
            if (contentToBeUpdate.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllEmployees");
            }
            return View();
        }
    }
}
