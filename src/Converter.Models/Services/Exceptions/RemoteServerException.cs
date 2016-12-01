using System;
using System.Runtime.Serialization;

namespace Escyug.Converter.Models.Services.Exceptions
{
    [Serializable]
    public class RemoteServerException : Exception
    {
        public RemoteServerException()
        {
            // Add implementation.
        }
        public RemoteServerException(string message)
            : base(message)
        {
            // Add implementation.
        }
        public RemoteServerException(string message, Exception inner)
            : base(message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected RemoteServerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}
