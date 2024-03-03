using System.ComponentModel.DataAnnotations;

namespace clutch_identity.Requests
{
    public class LoginUserRequest
    {
        [Required]
        public string Email { get; set; }   

        [Required]
        public string Password { get; set; }    
    }
}
