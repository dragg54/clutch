using clutch_employee.Data;
using System.ComponentModel.DataAnnotations;

namespace clutch_employee.Requests
{
    public class PostEmployeeRequest
    {
        public Guid Id { get => Guid.NewGuid(); }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string PositionUniqueReferenceNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string EmployeeStatus { get; set; }
    }
}
