using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace clutch_employee.Identity.Responses
{
    public class UserResponse
    {
       
        public HttpStatusCode StatusCode {get;set;}
        public string Message { get; set; }
        public object? Data {get;set;} 
    }
}