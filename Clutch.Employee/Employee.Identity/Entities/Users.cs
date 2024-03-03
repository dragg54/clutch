using System.ComponentModel.DataAnnotations;
using System.Data;

namespace clutch_employee.Identity.Entities
{
    public class Users
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime ActiveDate { get; set; }
    }
}
