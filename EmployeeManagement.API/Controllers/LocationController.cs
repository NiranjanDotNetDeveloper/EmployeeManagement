using EmployeeManagement.Core.Domain.Entities;
using EmployeeManagement.Core.DTOs;
using EmployeeManagement.Core.Helpers;
using EmployeeManagement.Core.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;
        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }
        /// <summary>
        /// Fetch All the Avialable locations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<APIResponseWrapper<List<LocationDTO>>>> GetAllLocations()
        {
            var result= await locationService.GetAllLocations();

            return new APIResponseWrapper<List<LocationDTO>>()
            {
                StatusCode = 200,
                Message = "Locations Successfully fetched.",
                Data = result.ToList()
            };
        }
    }
}
