using System.ComponentModel.DataAnnotations;

namespace clutch_employee.Requests
{
    public class PutEmployeeRequest
    {
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
