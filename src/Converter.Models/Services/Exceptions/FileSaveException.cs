using System;
using System.Runtime.Serialization;

namespace Escyug.Converter.Models.Services.Exceptions
{
    [Serializable]
    public class FileSaveException : Exception
    {
        public FileSaveException()
        {
            // Add implementation.
        }
        public FileSaveException(string message)
            : base(message)
        {
            // Add implementation.
        }
        public FileSaveException(string message, Exception inner)
            : base(message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected FileSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}
