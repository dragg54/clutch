using clutch_employee.Requests;

namespace clutch_employee.Services
{
    public interface IEmployeeService
    {
        void CreateEmployee(PostEmployeeRequest postEmployeeRequest);
    }
}
