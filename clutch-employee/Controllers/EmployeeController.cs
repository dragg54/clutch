using System.Net;
using clutch_employee.Exceptions;
using clutch_employee.Requests;
using clutch_employee.Resource;
using clutch_employee.Responses;
using clutch_employee.Services;
using Microsoft.AspNetCore.Mvc;

namespace clutch_employee.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> PostEmployee(PostEmployeeRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _employeeService.CreateEmployee(request);
             var response = new EmployeeActionResponse(){
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = "Employee created",
                Data = request
            };
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeResource))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetEmployees()
        {
            var resource = await _employeeService.GetEmployeesAync();
            return Ok(resource);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeResource))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var resource = await _employeeService.GetEmployeeAsync(id);
            return Ok(resource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeResource))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutEmployee(string id, [FromBody] PutEmployeeRequest request)
        {
            await _employeeService.AmendEmployee(request, id);
            var message = $"Employee with id {id} updated";
            var response = new EmployeeActionResponse(){
                StatusCode = HttpStatusCode.OK,
                Message = message,
                Data = request
            };
            return Ok(response);
        }
    }
}
