using System;
using System.Runtime.Serialization;

namespace Escyug.Converter.Models.Repositories.Exceptions
{
    [Serializable]
    public class EmptyFieldException : Exception
    {
        public EmptyFieldException()
        {
            // Add implementation.
        }
        public EmptyFieldException(string message)
            : base(message)
        {
            // Add implementation.
        }
        public EmptyFieldException(string message, Exception inner)
            : base(message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected EmptyFieldException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}
