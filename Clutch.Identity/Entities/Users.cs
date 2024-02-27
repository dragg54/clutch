using clutch_identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace clutch_identity.Entities
{
    public class Users
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime ActiveDate { get; set; }
    }
}
