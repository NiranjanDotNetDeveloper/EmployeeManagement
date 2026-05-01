using AutoMapper;
using EmployeeManagement.Core.Domain.RepositoryInterface;
using EmployeeManagement.Core.DTOs;
using EmployeeManagement.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Services
{
    public class LocationService:ILocationService
    {
        private readonly ILocationRepository locationRepo;
        private readonly IMapper _mapper;
        public LocationService(ILocationRepository locationRepo, IMapper _mapper)
        {
            this.locationRepo = locationRepo;
            this._mapper = _mapper;
        }
        public async Task<IEnumerable<LocationDTO>> GetAllLocations()
        {
            var locations= await locationRepo.GetAllLocations();
            var locationDTO= _mapper.Map<List<LocationDTO>>(locations);
            return locationDTO;
        }
    }
}
