using AutoFixture;
using AutoMapper;
using EmployeeManagement.Core.Domain.Entities;
using EmployeeManagement.Core.Domain.RepositoryInterface;
using EmployeeManagement.Core.DTOs;
using EmployeeManagement.Core.ServiceInterface;
using EmployeeManagement.Core.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _empRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly EmployeeService empService;
        private readonly Fixture _fixture;

        public EmployeeServiceTests()
        {
            _empRepository = new Mock<IEmployeeRepository>();
            _mapper = new Mock<IMapper>();
            empService = new EmployeeService(_empRepository.Object, _mapper.Object);
            _fixture = new Fixture();
        }
        [Fact]
        public void GetAllEmployee()
        {
            // Arrange
            var inputDto = _fixture.Create<EmployeeAddDTO>();

            var employee = _fixture.Build<Employee>()
                .Without(e => e.EmpId)
                .Without(e=>e.DepartmentInstance).
                Without(e=>e.Location)
                .Create();
            var outputDto = _fixture.Create<EmployeeAddDTO>();
            _mapper
                .Setup(x => x.Map<Employee>(inputDto))
                .Returns(employee);
            _empRepository
                .Setup(x => x.AddNewEmployee(employee))
                .Returns(employee);
            _mapper
                .Setup(x => x.Map<EmployeeAddDTO>(employee))
                .Returns(outputDto);

            // Act
            var result = empService.AddNewEmployee(inputDto);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(outputDto);

        }

        [Fact]
        public void AddNewEmployee()
        {
            var inputDto = _fixture.Create<EmployeeAddDTO>();
            var employee = _fixture.Build<Employee>().Without(x => x.EmpId).Without(x => x.Location).Without(x => x.DepartmentInstance).Create();
            var outputDto = _fixture.Create<EmployeeAddDTO>();
             _mapper.Setup(x => x.Map<Employee>(inputDto)).Returns(employee);
            _empRepository.Setup(x => x.AddNewEmployee(employee)).Returns(employee);
            _mapper.Setup(x => x.Map<EmployeeAddDTO>(employee)).Returns(outputDto);
            var seriveCall = empService.AddNewEmployee(inputDto);
            seriveCall.Should().NotBeNull();
            seriveCall.Should().BeEquivalentTo(outputDto);
            _empRepository.Verify(x => x.AddNewEmployee(employee), Times.Once);
        }
        [Fact]
        public void AddNewEmployee_Null()
        {
            Assert.Throws<ArgumentNullException>(()=>empService.AddNewEmployee(null));
        }
    }
}
