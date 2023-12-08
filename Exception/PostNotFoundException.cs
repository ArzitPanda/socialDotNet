using System.Runtime.Serialization;

namespace sample_one
{


    public class PostNotFoundException : Exception
    {
        public PostNotFoundException():base("post is not found")
        {
        }

        public PostNotFoundException(string? message) : base(message)
        {
        }

        public PostNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PostNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


}