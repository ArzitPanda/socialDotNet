
using System.Runtime.Serialization;

namespace sample_one
{

    public class AlreadyConnectionException : Exception
    {
        public AlreadyConnectionException():base("Connection already present")
        {
        }

        public AlreadyConnectionException(long id,long friendId):base($"Connection {id} already present with friendId {friendId}")
        {

        }

        public AlreadyConnectionException(string? message) : base(message)
        {
            
        }

        public AlreadyConnectionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AlreadyConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }




}