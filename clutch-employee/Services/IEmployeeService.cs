using clutch_employee.Models;
using clutch_employee.Requests;
using clutch_employee.Resource;

namespace clutch_employee.Services
{
    public interface IEmployeeService
    {
        Task<Employee> CreateEmployee(PostEmployeeRequest postEmployeeRequest);
        Task<List<EmployeeResource>> GetEmployeesAync();
        Task<EmployeeResource> GetEmployeeAsync(string id);
        Task<Employee> AmendEmployee(PutEmployeeRequest postEmployeeRequest, string id);


    }
}
