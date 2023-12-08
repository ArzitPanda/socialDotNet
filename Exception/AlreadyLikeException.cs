using System.Runtime.Serialization;

namespace sample_one
{

    public class AlreadyLikeException : Exception
    {
        public AlreadyLikeException() :base("already liked")
        {
        }

        public AlreadyLikeException(string? message) : base(message)
        {
        }

        public AlreadyLikeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AlreadyLikeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }



}