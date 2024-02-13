using clutch_employee.Data;
using clutch_employee.Entities;
using clutch_employee.Requests;
using clutch_employee.Resource;

namespace clutch_employee.Extensions
{
    public static class EmployeeExtensions
    {
        public static Employee ToAddEmployeeRequest(this PostEmployeeRequest request)
        {
            return new Employee
            {
                EmployeeId = request.EmployeeId,
                PositionUniqueReferenceNumber = request.PositionUniqueReferenceNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                StartDate = request.StartDate,
                EmployeeStatus = EmployeeStatus.Active
            };
        }

         public static Employee AddEmployeeRequest(this PostEmployeeRequest request)
        {
            return new Employee
            {
                EmployeeId = request.EmployeeId,
                PositionUniqueReferenceNumber = request.PositionUniqueReferenceNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                StartDate = request.StartDate,
                EmployeeStatus = EmployeeStatus.Active
            };
        }

        public static Employee ToAmendEmployeeRequest(this PutEmployeeRequest request, Guid id)
        {
            return new Employee
            {
                PositionUniqueReferenceNumber = request.PositionUniqueReferenceNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                StartDate = request.StartDate,
                EmployeeStatus = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), request.EmployeeStatus)
            };
        }

        public static List<EmployeeResource> ToEmployeeResources(this List<Employee> employees)
        {
            return employees.Select(emp => new EmployeeResource
            {
                Id = emp.Id,
                EmployeeId = emp.EmployeeId,
                PositionId = emp.PositionUniqueReferenceNumber,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Age = emp.Age,
                StartDate = emp.StartDate,
                EmployeeStatus = emp.EmployeeStatus.ToString()
            }).ToList();
        }

        public static EmployeeResource ToEmployeeResource(this Employee employee)
        {
            return new EmployeeResource
            {
                Id = employee.Id,
                EmployeeId = employee.EmployeeId,
                PositionId = employee.PositionUniqueReferenceNumber,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                StartDate = employee.StartDate,
                EmployeeStatus = employee.EmployeeStatus.ToString()
            };
        }
    }
}
