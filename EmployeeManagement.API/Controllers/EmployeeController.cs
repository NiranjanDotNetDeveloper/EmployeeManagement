using EmployeeManagement.Core.DTOs;
using EmployeeManagement.Core.Helpers;
using EmployeeManagement.Core.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _empService;
        private readonly IMemoryCache _memoryCache;
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService empService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _empService = empService;
            _memoryCache = memoryCache;
        }
        /// <summary>
        /// Fetch All Employee Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<APIResponseWrapper<List<EmployeeDTO>>>> GetAllEmployeeDetails()
        {
            _logger.LogInformation("Request received to retrieve all the employees");
            if (_memoryCache.TryGetValue(EmployeeConstants.EmployeeCache, out APIResponseWrapper<List<EmployeeDTO>> listOfEmployee))
            {
                _logger.LogInformation("Reponse Found in the cache..");
                return new APIResponseWrapper<List<EmployeeDTO>>()
                {
                    Message = "Successfully Fetched the details",
                    StatusCode = 200,
                    Data = listOfEmployee.Data
                };
            }
            var result = await _empService.GetAllEmployee();
            if (result != null)
            {
                _logger.LogInformation("Response is not available in Cache.");
                var memoryOption = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                var cacheEntry = new APIResponseWrapper<List<EmployeeDTO>>()
                {
                    Message = "Successfully Fetched the details",
                    StatusCode = 200,
                    Data = result.ToList()
                };

                _memoryCache.Set(EmployeeConstants.EmployeeCache, cacheEntry, memoryOption);
                _logger.LogInformation("Response has been added in the Cache.");
                return cacheEntry;
            }
            else
            {
                return new APIResponseWrapper<List<EmployeeDTO>>()
                {
                    Message = "No Employee details found.",
                    StatusCode = 404,
                    Data = null
                };
            }
        }
        /// <summary>
        /// Fetch a Employee Detail by EmployeeId
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponseWrapper<EmployeeDTO>>> GetEmployeeById(int id)
        {
            _logger.LogInformation($"Request received to retrieve an Employee {id} from the employees");

            var result = await _empService.GetEmployeeById(id);
            if (result != null)
            {
                var cacheEntry = new APIResponseWrapper<EmployeeDTO>()
                {
                    Message = "Successfully Fetched the details",
                    StatusCode = 200,
                    Data = result
                };
                _logger.LogInformation("Response has been added in the Cache.");
                return cacheEntry;
            }
            else
            {
                return new APIResponseWrapper<EmployeeDTO>()
                {
                    Message = "Something Went wrong while fetching the employee details.",
                    StatusCode = 404,
                    Data = null
                };
            }
        }
        /// <summary>
        /// Add New Employee
        /// </summary>
        /// <param name="employeeAddDTO">Pass Employee Details</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<APIResponseWrapper<EmployeeAddDTO>>> AddNewEmployee([FromBody] EmployeeAddDTO employeeAddDTO)
        {
            _logger.LogInformation("Request received to Add new employee");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Employee Details are not proper.");
                List<string> error = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                return new APIResponseWrapper<EmployeeAddDTO>()
                {
                    Message = string.Join(", ", error),
                    Data = null,
                    StatusCode = 400
                };

            }
            var result = await _empService.AddNewEmployee(employeeAddDTO);
            if (result == null)
            {
              
                _logger.LogInformation("Some error occuered. Employee has not been added.");
                return new APIResponseWrapper<EmployeeAddDTO>()
                {
                    Message = "Some error occuered. Employee has not been added.",
                    Data = result,
                    StatusCode = 400
                };
            }
            else
            {
                _memoryCache.Remove(EmployeeConstants.EmployeeCache);
                _logger.LogInformation("Employee has been added successfully");
                return new APIResponseWrapper<EmployeeAddDTO>()
                {
                    Message = "Employee has been added successfully.",
                    Data = result,
                    StatusCode = 201
                };
            }

        }
        /// <summary>
        /// Update existing Employee
        /// </summary>
        /// <param name="id">Pass Employee ID whose details needs to be updated</param>
        /// <param name="employeeUpdateDTO">Pass Employee Details</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponseWrapper<EmployeeUpdateDTO>>> UpdateEmployee(int id, [FromBody] EmployeeUpdateDTO employeeUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Employee Details are not proper.");
                List<string> error = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                return new APIResponseWrapper<EmployeeUpdateDTO>()
                {
                    Message = string.Join(", ", error),
                    Data = null,
                    StatusCode = 400
                };

            }

            var result = await _empService.UpdateEmployee(id, employeeUpdateDTO);
            if (result == null)
            {
               
                _logger.LogInformation("Some error occuered. Employee has not been updated.");
                return new APIResponseWrapper<EmployeeUpdateDTO>()
                {
                    Message = "Some error occuered. Employee has not been updated.",
                    Data = result,
                    StatusCode = 400
                };
            }
            else
            {
                _logger.LogInformation("Employee has been updated successfully.");
                _memoryCache.Remove(EmployeeConstants.EmployeeCache);
                return new APIResponseWrapper<EmployeeUpdateDTO>()
                {
                    Message = "Employee has been updated successfully.",
                    Data = result,
                    StatusCode = 200
                };
            }
        }
        /// <summary>
        /// Remove an Employee
        /// </summary>
        /// <param name="id">Pass Employee ID whose details needs to be removed</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponseWrapper<bool>>> DeleteEmployee(int id)
        {
            var result = await _empService.DeleteEmployee(id);
            if (result)
            {
                _memoryCache.Remove(EmployeeConstants.EmployeeCache);
                _logger.LogInformation("Employee has been deleted successfully.");
                return new APIResponseWrapper<bool>()
                {
                    Message = "Employee has been deleted successfully.",
                    Data = result,
                    StatusCode = 200
                };
            }
            else
            {
                _logger.LogInformation("Employee has not been deleted successfully.");
                return new APIResponseWrapper<bool>()
                {
                    Message = "Employee has not been deleted successfully.",
                    Data = result,
                    StatusCode = 404
                };

            }
        }
    }
}
