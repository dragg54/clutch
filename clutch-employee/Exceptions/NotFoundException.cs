namespace clutch_employee.Exceptions
{
    [Serializable]
    public class NotFoundException:Exception
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string msg) : base(msg) {

        }
    }
}
