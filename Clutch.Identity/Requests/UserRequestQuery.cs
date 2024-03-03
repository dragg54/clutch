using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clutch_identity.Requests
{
    public class UserRequestQuery
    {
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? Email {get; set;}
    }
}