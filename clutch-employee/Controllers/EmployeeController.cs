using clutch_employee.Requests;
using clutch_employee.Resource;
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
            _employeeService= employeeService;  
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(EmployeeResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult AddEmployee(PostEmployeeRequest request)
        {
            _employeeService.CreateEmployee(request);
            return Ok();
        }
    }
}
