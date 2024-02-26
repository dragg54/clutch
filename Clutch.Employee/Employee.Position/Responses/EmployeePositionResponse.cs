using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clutch.Employee.Position.Response
{
    public class EmployeePositionResponse
    {
        public HttpStatusCode StatusCode {get;set;}
        public string Message { get; set; }
        public object? Data {get;set;}
    }
}