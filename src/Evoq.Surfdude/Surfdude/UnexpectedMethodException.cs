using System;
using System.Runtime.Serialization;

namespace Evoq.Surfdude
{
    [Serializable]
    internal class UnexpectedMethodException : Exception
    {
        public UnexpectedMethodException()
        {
        }

        public UnexpectedMethodException(string message) : base(message)
        {
        }

        public UnexpectedMethodException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnexpectedMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}