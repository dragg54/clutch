using clutch_employee.Entities;
using clutch_employee.Requests;
using clutch_employee.Resource;

namespace clutch_employee.Services
{
    public interface IEmployeeService
    {
        Task CreateEmployee(PostEmployeeRequest postEmployeeRequest);
        Task<List<EmployeeResource>> GetEmployeesAync();
        Task<EmployeeResource> GetEmployeeAsync(string id);
        Task AmendEmployee(PutEmployeeRequest postEmployeeRequest, string id);
        Task DeleteEmployee(string id);
    }
}
