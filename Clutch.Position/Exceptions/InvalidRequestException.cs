namespace clutch_position.Exceptions
{
    public class InvalidRequestException: Exception
    {
        public InvalidRequestException() { }

        public InvalidRequestException(string message) : base(message) { }
    }
}
