using System.Runtime.Serialization;

namespace clutch_identity.Exceptions
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException()
        {
        }

        public DuplicateUserException(string message) : base(message)
        {
        }

        public DuplicateUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
