using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace clutch_identity.Requests
{
    public class PutUserRequest
    {
        [Required]
        public string FirstName {get;set;}

        [Required]
        public string LastName {get;set;}

        [Required]
        public string Email {get;set;}

        [Required]
        public string Role {get;set;}

        [Required]
        public DateTime ActiveDate { get; set; }
    }
}