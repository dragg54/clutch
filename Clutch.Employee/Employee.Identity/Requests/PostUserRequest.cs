using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clutch_employee.Identity.Requests
{
    public class PostUserRequest
    {
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public string Password{get;set;}
        public string Role { get => "User"; }
        public string Email {get; set;}
        public DateTime ActiveDate{get;set;}
    }
}