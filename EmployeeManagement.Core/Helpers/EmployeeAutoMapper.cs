using AutoMapper;
using EmployeeManagement.Core.Domain.Entities;
using EmployeeManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Helpers
{
    public class EmployeeAutoMapper:AutoMapper.Profile
    {
        public EmployeeAutoMapper()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<Employee, EmployeeUpdateDTO>();
            CreateMap<EmployeeUpdateDTO, Employee>();
            CreateMap<Location, LocationDTO>();
            CreateMap<LocationDTO, Location>();
            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();
        }
    }
}
