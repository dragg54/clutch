using System.Net;

namespace clutch_employee.Responses
{
    public class EmployeeActionResponse
    {
        public HttpStatusCode StatusCode{get;set;}
        public string Message{get; set;}
        public object Data{get;set;}
    }
}