namespace clutch_employee.Exceptions
{
    [Serializable]
    public class InvalidRequestException: Exception
    {

        public InvalidRequestException() { }    

        public InvalidRequestException(string message) : base(message) { }
    }
}
