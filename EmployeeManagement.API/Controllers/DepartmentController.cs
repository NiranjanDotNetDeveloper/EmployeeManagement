using EmployeeManagement.Core.DTOs;
using EmployeeManagement.Core.Helpers;
using EmployeeManagement.Core.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }
        /// <summary>
        /// Fetch All the Avialable locations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<APIResponseWrapper<List<DepartmentDTO>>>> GetAllDepartments()
        {
            var result = await departmentService.GetAllDepartments();

            return new APIResponseWrapper<List<DepartmentDTO>>()
            {
                StatusCode = 200,
                Message = "Departments Successfully fetched.",
                Data = result.ToList()
            };
        }
    }
}

