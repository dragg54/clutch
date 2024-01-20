﻿using clutch_employee.Data;
using clutch_employee.Data.Contexts;
using clutch_employee.Exceptions;
using clutch_employee.Extensions;
using clutch_employee.Models;
using clutch_employee.Requests;
using clutch_employee.Resource;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

namespace clutch_employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext employeeDbContext;
        public EmployeeService(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext ?? throw new ArgumentException();
        }

        public async Task<Employee> CreateEmployee(PostEmployeeRequest request)
        {
            var existingEmployee = employeeDbContext.Employees.SingleOrDefault(emp => emp.EmployeeId == request.EmployeeId);
            try
            {
                if (existingEmployee != null)
                {
                    throw new DuplicateException($"Employee already exists");
                }
                var newEmployee = request.ToAddEmployeeRequest();
                await employeeDbContext.Employees.AddAsync(request.ToAddEmployeeRequest());
                await employeeDbContext.SaveChangesAsync();
                return newEmployee;
            }
            catch (DuplicateException ex)
            {
                throw new DuplicateException($"Employee already exists");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Employee> AmendEmployee(PutEmployeeRequest request, string id)
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
                return existingEmployee;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<EmployeeResource>> GetEmployeesAync()
        {
            var employees = await employeeDbContext.Employees.ToListAsync();
            return employees.ToEmployeeResources();
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
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}
