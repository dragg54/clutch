using clutch_employee.Data;
using clutch_employee.Data.Contexts;
using clutch_employee.Exceptions;
using clutch_employee.Extensions;
using clutch_employee.Entities;
using clutch_employee.Requests;
using clutch_employee.Resource;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;
using System.Net;
using Clutch.Employee.Position.Client;
using Clutch.Employee.Position;

namespace clutch_employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext employeeDbContext;
        private readonly IPositionClient positionClient;
        public EmployeeService(EmployeeDbContext employeeDbContext, IPositionClient positionClient)
        {
            this.employeeDbContext = employeeDbContext ?? throw new ArgumentException();
            this.positionClient = positionClient ?? throw new ArgumentNullException();
        }

        public async Task CreateEmployee(PostEmployeeRequest request)
        {
            try
            {
                var existingEmployee = employeeDbContext.Employees.SingleOrDefault(emp => emp.EmployeeId == request.EmployeeId);
                EmployeePositionResponse positionResponse = await positionClient.GetEmployeePositionResource(request.PositionUniqueReferenceNumber);

                if (existingEmployee != null)
                {
                    throw new DuplicateException($"Employee with id {existingEmployee.EmployeeId} already exists");
                }
                if (positionResponse.StatusCode == HttpStatusCode.OK)
                {
                    var newEmployee = request.ToAddEmployeeRequest();
                    await employeeDbContext.Employees.AddAsync(request.ToAddEmployeeRequest());
                    await employeeDbContext.SaveChangesAsync();
                }
                if (positionResponse.StatusCode == HttpStatusCode.NotFound)
                {

                    throw new NotFoundException($"Position with {request.PositionUniqueReferenceNumber} not found");
                }

            }
            catch (NotFoundException ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                var errMsg = "Unable to create employee";
                Log.Error(errMsg, ex);
                throw new HttpRequestException(errMsg, ex);
            }
        }

        public async Task AmendEmployee(PutEmployeeRequest request, string id)
        {
            var existingEmployee = await employeeDbContext.Employees.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            if (existingEmployee == null)
            {
                var errMsg = $"employee with id {id} does not exist";
                throw new NotFoundException(errMsg);
            }
            try
            {
                existingEmployee.FirstName = request.FirstName;
                existingEmployee.LastName = request.LastName;
                existingEmployee.PositionUniqueReferenceNumber = request.PositionUniqueReferenceNumber;
                existingEmployee.StartDate = request.StartDate;
                existingEmployee.EmployeeStatus = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), request.EmployeeStatus);
                employeeDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var errMsg = "Unable to amend employee";
                Log.Error(errMsg, ex);
                throw new Exception(errMsg, ex);
            }

        }

        public async Task<List<EmployeeResource>> GetEmployeesAync()
        {
            try
            {
                var employees = await employeeDbContext.Employees.ToListAsync();
                return employees.ToEmployeeResources();
            }
            catch (Exception ex)
            {
                var errMsg = "Unable to get employee";
                Log.Error(errMsg, ex);
                throw new Exception(errMsg, ex);
            }
        }

        public async Task<EmployeeResource> GetEmployeeAsync(string id)
        {
            try
            {
                var employee = await employeeDbContext.Employees.FirstOrDefaultAsync(emp => emp.Id.ToString() == id);
                if (employee == null)
                {
                    var errMsg = $"Empployee with id {id} does not exist";
                    throw new NotFoundException(errMsg);
                }
                return employee.ToEmployeeResource();
            }
            catch (NotFoundException ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                Log.Error("Unable to get employee", ex);
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}
