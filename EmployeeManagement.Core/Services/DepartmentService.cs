using AutoMapper;
using EmployeeManagement.Core.Domain.RepositoryInterface;
using EmployeeManagement.Core.DTOs;
using EmployeeManagement.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper _mapper)
        {
            this.departmentRepository = departmentRepository;
            this._mapper = _mapper;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartments()
        {
            var result = await departmentRepository.GetAllDepartments();
            var resultInDTO = _mapper.Map<List<DepartmentDTO>>(result);
            return resultInDTO.ToList();
        }
    }
}
