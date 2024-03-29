﻿using System.ComponentModel.DataAnnotations;
using clutch_identity.Data;

namespace clutch_identity.Requests
{
    public class PostUserRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
                
        [Required]
        public string Role { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime ActiveDate { get; set; }
    }
}
