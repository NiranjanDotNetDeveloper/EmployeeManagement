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
    public class EmployeeAutoMapper:Profile
    {
        public EmployeeAutoMapper()
        {
            CreateMap<Employee, EmployeeAddDTO>();
            CreateMap<EmployeeAddDTO, Employee>();
            CreateMap<Employee, EmployeeUpdateDTO>();
            CreateMap<EmployeeUpdateDTO, Employee>();
            CreateMap<Location, LocationDTO>();
            CreateMap<LocationDTO, Location>();
            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();
        }
    }
}
