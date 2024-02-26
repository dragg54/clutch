using clutch_employee.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clutch_employee.Entities
{
    public class Employee
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("employeeId")]
        public string EmployeeId { get; set; }

        [Column("positionUniqueReferenceNumber")]
        public string PositionUniqueReferenceNumber { get; set; }

        [Column("firstName")]
        public string FirstName { get; set; }

        [Column("lastName")]
        public string LastName { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("startDate")]
        public DateTime StartDate { get; set; }

        [Column("employeeStatus")]
        public EmployeeStatus EmployeeStatus { get; set; }  
    }
}
