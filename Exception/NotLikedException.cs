using System.Runtime.Serialization;

namespace sample_one
{

    public class NotLikedException : Exception
    {
        public NotLikedException():base("not liked yet")
        {
        }

        public NotLikedException(string? message) : base(message)
        {
        }

        public NotLikedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotLikedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }




}