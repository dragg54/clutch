using clutch_employee.Data.Contexts;
using clutch_employee.Extensions;
using clutch_employee.Models;
using clutch_employee.Requests;

namespace clutch_employee.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly EmployeeDbContext employeeDbContext;
        public EmployeeService(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext ?? throw new ArgumentException();
        }
        
        public void CreateEmployee(PostEmployeeRequest employee)
        {
            if(employee == null)
            {
                throw new Exception("employee is null");
            }
            employeeDbContext.Employees.Add(employee.ToEmployee());
            employeeDbContext.SaveChanges();

        }
    }
}
