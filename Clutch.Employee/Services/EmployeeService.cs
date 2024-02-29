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
using Clutch.Employee.Position.Response;
using clutch_employee.Position.Entities;
using clutch_employee.Position.Enums;
using clutch_employee.Identity.Responses;
using clutch_employee.Identity.Client;

namespace clutch_employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext employeeDbContext;
        private readonly IPositionClient positionClient;
        private readonly IIdentityClient identityClient;
        public EmployeeService(EmployeeDbContext employeeDbContext, IPositionClient positionClient, IIdentityClient identityClient)
        {
            this.employeeDbContext = employeeDbContext ?? throw new ArgumentException(nameof(employeeDbContext));
            this.positionClient = positionClient ?? throw new ArgumentNullException(nameof(positionClient));
            this.identityClient = identityClient ?? throw new ArgumentNullException(nameof(identityClient));
        }

        public async Task CreateEmployee(PostEmployeeRequest request)
        {
            try
            {
                var existingEmployee = employeeDbContext.Employees.SingleOrDefault(emp => emp.EmployeeId == request.EmployeeId);
                EmployeePositionResponse positionResponse = await positionClient.GetEmployeePositionResource(request.PositionUniqueReferenceNumber);
                UserResponse userResponse = await identityClient.PostUser(request.ToUserRequest());
                if (existingEmployee != null)
                {
                    throw new DuplicateException($"Employee with id {existingEmployee.EmployeeId} already exists");
                }
                if (positionResponse.StatusCode == HttpStatusCode.NotFound)
                {

                    throw new NotFoundException($"Position with {request.PositionUniqueReferenceNumber} not found");
                }
                if (((EmployeePosition)positionResponse.Data).PositionStatus == EmployeePositionStatus.Filled.ToString())
                {
                    var message = $"Position with id {request.PositionUniqueReferenceNumber} is not empty";
                    throw new InvalidOperationException(message);
                }
                if (positionResponse.StatusCode == HttpStatusCode.OK && userResponse.StatusCode == HttpStatusCode.OK)
                {
                    var newEmployee = request.ToAddEmployeeRequest();
                    await employeeDbContext.Employees.AddAsync(request.ToAddEmployeeRequest());
                    await employeeDbContext.SaveChangesAsync();
                }
            }
            catch (NotFoundException ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.BadRequest);
            }
            catch (InvalidOperationException ex)
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
            try
            {
                var existingEmployee = await employeeDbContext.Employees.FirstOrDefaultAsync(x => x.Id.ToString() == id);
                EmployeePositionResponse positionResponse = await positionClient.GetEmployeePositionResource(request.PositionUniqueReferenceNumber);
                if (existingEmployee == null)
                {
                    var errMsg = $"employee with id {id} does not exist";
                    throw new NotFoundException(errMsg);
                }
                if (positionResponse.StatusCode == HttpStatusCode.NotFound)
                {

                    throw new NotFoundException($"Position with {request.PositionUniqueReferenceNumber} not found");
                }
                if (((EmployeePosition)positionResponse.Data).PositionStatus == EmployeePositionStatus.Filled.ToString())
                {
                    var message = $"Position with id {request.PositionUniqueReferenceNumber} is not empty";
                    throw new InvalidOperationException(message);
                }
                if (positionResponse.StatusCode == HttpStatusCode.OK)
                {
                    var amendedEmployee = request.ToAmendEmployeeRequest(existingEmployee.EmployeeId, existingEmployee.StartDate);
                    await employeeDbContext.SaveChangesAsync();
                }

            }
            catch (NotFoundException ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.BadRequest);
            }
            catch (InvalidOperationException ex)
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
