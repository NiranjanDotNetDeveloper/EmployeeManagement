using AutoMapper;
using EmployeeManagement.Core.Domain.Entities;
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
    public class EmployeeService:IEmployeeService
    {
        private readonly IEmployeeRepository _empRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository empRepository, IMapper mapper)
        {
            this._empRepository = empRepository;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployee()
        {
            var result= await _empRepository.GetAllEmployee();
            var resultReturned= _mapper.Map<IEnumerable<EmployeeDTO>>(result);
            return resultReturned;
        }
        public async Task<EmployeeAddDTO> AddNewEmployee(EmployeeAddDTO emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("Please enter valid Employee details..");
            }
            var empObject = _mapper.Map<Employee>(emp);
            var result=await _empRepository.AddNewEmployee(empObject);
            if (result == null)
            {
                throw new ArgumentNullException("Something went wrong while adding..");
            }
            return _mapper.Map<EmployeeAddDTO>(result);
        }
        public async Task<EmployeeUpdateDTO> UpdateEmployee(int id, EmployeeUpdateDTO emp)
        {
            if (id == 0 || emp ==null)
            {
                throw new ArgumentNullException("Please enter valid Employee details..");
            }
            var empObject = _mapper.Map<Employee>(emp);
            var result = await _empRepository.UpdateEmployee(id,empObject);
            if (result == null)
            {
                throw new ArgumentNullException("Something went wrong while updating..");
            }
            return _mapper.Map<EmployeeUpdateDTO>(result);
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Please enter valid Employee Id..");
            }
            var result = await _empRepository.DeleteEmployee(id);
            if (result)
                return true;
            else
                throw new Exception("Something went wrong while deleting an employee");
        }
        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Please enter valid Employee Id..");
            }
            var result = await _empRepository.GetEmployeeById(id);
            var resultInDTO = _mapper.Map<EmployeeDTO>(result);
            if (resultInDTO == null)
                throw new Exception("Something went wrong while Fetching an employee");
            else
                return resultInDTO;
        }
    }
}
