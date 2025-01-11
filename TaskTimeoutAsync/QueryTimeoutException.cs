using System;
using System.Runtime.Serialization;

namespace TaskTimeoutAsync
{
    [Serializable]
    internal class QueryTimeoutException : Exception
    {
        public QueryTimeoutException()
        {
        }

        public QueryTimeoutException(string message) : base(message)
        {
        }

        public QueryTimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QueryTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}