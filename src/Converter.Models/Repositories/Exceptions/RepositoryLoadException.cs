using System;
using System.Runtime.Serialization;

namespace Escyug.Converter.Models.Repositories.Exceptions
{
    [Serializable]
    public class RepositoryLoadException : Exception
    {
        public RepositoryLoadException()
        {
            // Add implementation.
        }
        public RepositoryLoadException(string message)
            : base(message)
        {
            // Add implementation.
        }
        public RepositoryLoadException(string message, Exception inner)
            : base(message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected RepositoryLoadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}
