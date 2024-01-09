using clutch_employee.Data;
using clutch_employee.Models;
using clutch_employee.Requests;
using clutch_employee.Resource;

namespace clutch_employee.Extensions
{
    public static class EmployeeExtensions
    {
        public static Employee ToEmployee(this PostEmployeeRequest request)
        {
            return new Employee
            {
                EmployeeId = request.EmployeeId,
                PositionUniqueReferenceNumber = request.PositionUniqueReferenceNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                StartDate = request.StartDate,
                EmployeeStatus = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), request.EmployeeStatus)
            };
        }
    }
}
