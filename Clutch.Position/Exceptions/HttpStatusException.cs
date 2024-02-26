namespace clutch_position.Exceptions
{
    public class HttpStatusException: Exception
    {
        public int Status { get; private set; } = 500;

        public HttpStatusException(int status, string msg) : base(msg)
        {
            Status = status;
        }
    }
}
