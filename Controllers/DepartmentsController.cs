using AutoMapper;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.IRepository;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IMapper _mapper;

        public DepartmentsController(IUnitOfWork unitOfWork, ILogger<DepartmentsController> logger, IMapper mapper)
        {
            _mapper = mapper; 
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDepartments([FromQuery] RequestParams requestParams)
        {
            var department = await _unitOfWork.Departments.GetPagedList(requestParams);
            var result = _mapper.Map<IList<DepartmentDTO>>(department);

            return Ok(result);
        }
        [HttpGet("{id:int}", Name = "GetDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _unitOfWork.Departments.Get(q => q.Id == id, new List<string> { "Employees" });
            var result = _mapper.Map<DepartmentDTO>(department);
            return Ok(result);
        }
        [Authorize(Roles="Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post Request In {nameof(CreateDepartment)}");
                return BadRequest(ModelState);
            }

            var department = _mapper.Map<Department>(departmentDTO);
            await _unitOfWork.Departments.Insert(department);
            await _unitOfWork.Save();  //hotel will be given an aoutomatic Id and get saved

            return CreatedAtRoute("GetHotel", new { id = department.Id }, department);


        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateDepartment(int id,[FromBody] UpdateDepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt In {nameof(UpdateDepartment)}");
                return BadRequest(ModelState);
            }

            var department = await _unitOfWork.Departments.Get(q => q.Id == id);
            if (department == null)
            {
                _logger.LogError($"Invalid UPDATE attempt In {nameof(UpdateDepartment)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(departmentDTO, department);
            _unitOfWork.Departments.Update(department);
            await _unitOfWork.Save();

            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE Attempt In {nameof(DeleteDepartment)}");
                return BadRequest(ModelState);
            }


            var department = await _unitOfWork.Departments.Get(q => q.Id == id);
            if (department == null)
            {
                _logger.LogError($"Invalid DELETE attempt In {nameof(DeleteDepartment)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Departments.Delete(id);
            await _unitOfWork.Save();


            return NoContent();


        }
    }
}
