using AutoMapper;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.IRepository;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeesController> _logger;
        private readonly IMapper _mapper;
        public EmployeesController(IUnitOfWork unitOfWork, ILogger<EmployeesController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEmployees()
         {
            var employees = await _unitOfWork.Employees.GetAll();
            var result = _mapper.Map<IList<EmployeeDTO>>(employees);

            return Ok(result);
         }

        [HttpGet("{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _unitOfWork.Employees.Get(q=>q.Id == id, new List<string> { "Department" });
            var result = _mapper.Map<EmployeeDTO>(employee);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid) {
                _logger.LogError($"Invalid Post Attempt In {nameof(CreateEmployee)}");
                return BadRequest(ModelState);
            }
             var employee = _mapper.Map<Employee>(employeeDTO);
             await _unitOfWork.Employees.Insert(employee);
             await _unitOfWork.Save();

            return CreatedAtRoute("GetEmployee", new { id = employee.Id }, employee);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt In {nameof(UpdateEmployee)}");
                return BadRequest(ModelState);
            }

            var employee = await _unitOfWork.Employees.Get(q => q.Id == id);
            if (employee == null)
            {
                _logger.LogError($"Invalid UPDATE attempt In {nameof(UpdateEmployee)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(employeeDTO, employee);
            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE Attempt In {nameof(DeleteEmployee)}");
                return BadRequest(ModelState);
            }


            var employee = await _unitOfWork.Employees.Get(q => q.Id == id);
            if (employee == null)
            {
                _logger.LogError($"Invalid DELETE attempt In {nameof(DeleteEmployee)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Employees.Delete(id);
            await _unitOfWork.Save();


            return NoContent();


        }

    }
}
