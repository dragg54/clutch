using System.ComponentModel.DataAnnotations;

namespace clutch_employee.Identity.Requests
{
    public class AmendUserRequest
    {
        [Required]
        public string Id { get; set; } 

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public DateTime ActiveDate { get; set; }
    }
}
