using AutoMapper;
using EmployeeManagement.Core.Domain.Entities;
using EmployeeManagement.Core.Domain.RepositoryInterface;
using EmployeeManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _empRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository empRepository, IMapper mapper)
        {
            this._empRepository = empRepository;
            this._mapper = mapper;
        }
        public IEnumerable<EmployeeDTO> GetAllEmployee()
        {
            var result= _empRepository.GetAllEmployee();
            return _mapper.Map<List<EmployeeDTO>>(result);
        }
        public EmployeeAddDTO AddNewEmployee(EmployeeAddDTO emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("Please enter valid Employee details..");
            }
            var empObject = _mapper.Map<Employee>(emp);
            var result=_empRepository.AddNewEmployee(empObject);
            if (result == null)
            {
                throw new ArgumentNullException("Something went wrong while adding..");
            }
            return _mapper.Map<EmployeeAddDTO>(result);
        }
        public EmployeeUpdateDTO UpdateEmployee(int id, EmployeeUpdateDTO emp)
        {
            if (id == 0 || emp ==null)
            {
                throw new ArgumentNullException("Please enter valid Employee details..");
            }
            var empObject = _mapper.Map<Employee>(emp);
            var result = _empRepository.UpdateEmployee(id,empObject);
            if (result == null)
            {
                throw new ArgumentNullException("Something went wrong while updating..");
            }
            return _mapper.Map<EmployeeUpdateDTO>(result);
        }
        public bool DeleteEmployee(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Please enter valid Employee Id..");
            }
            var result = _empRepository.DeleteEmployee(id);
            if (result)
                return true;
            else
                throw new Exception("Something went wrong while deleting an employee");
        }
        public EmployeeUpdateDTO GetEmployeeById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Please enter valid Employee Id..");
            }
            var result = _empRepository.GetEmployeeById(id);
            var resultInDTO = _mapper.Map<EmployeeUpdateDTO>(result);
            if (resultInDTO == null)
                throw new Exception("Something went wrong while Fetching an employee");
            else
                return resultInDTO;
        }
    }
}
