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

        [Required]
        [Column("employeeId")]
        public string EmployeeId { get; set; }

        [Required]
        [Column("positionUniqueReferenceNumber")]
        public string PositionUniqueReferenceNumber { get; set; }

        [Required]
        [Column("firstName")]
        public string FirstName { get; set; }

        [Required]
        [Column("lastName")]
        public string LastName { get; set; }

        [Required]
        [Column("age")]
        public int Age { get; set; }

        [Required]
        [Column("startDate")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("employeeStatus")]
        public EmployeeStatus EmployeeStatus { get; set; }  
    }
}
