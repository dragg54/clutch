using clutch_employee.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace clutch_employee.Data.Contexts
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
