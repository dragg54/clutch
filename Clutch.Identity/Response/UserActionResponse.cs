using System.Net;

namespace clutch_identity.Response
{
    public class UserActionResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
