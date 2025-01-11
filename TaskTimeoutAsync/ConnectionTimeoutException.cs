using System;
using System.Runtime.Serialization;

namespace TaskTimeoutAsync
{
    [Serializable]
    internal class ConnectionTimeoutException : Exception
    {
        public ConnectionTimeoutException()
        {
        }

        public ConnectionTimeoutException(string message) : base(message)
        {
        }

        public ConnectionTimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConnectionTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}