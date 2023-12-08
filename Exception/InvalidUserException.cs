using System.Runtime.Serialization;

namespace sample_one
{

    public class InvalidUserException : Exception
    {
        public InvalidUserException():base("user is invalid")
        {
        }

        public InvalidUserException(string? message) : base(message)
        {
        }

        public InvalidUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }




}