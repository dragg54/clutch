namespace clutch_employee.Exceptions
{
    public class InvalidRequestException: Exception
    {

        public InvalidRequestException() { }    

        public InvalidRequestException(string message) : base(message) { }
    }
}
