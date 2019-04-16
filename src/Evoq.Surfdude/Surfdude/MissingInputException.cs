using System;
using System.Runtime.Serialization;

namespace Evoq.Surfdude
{
    [Serializable]
    public class MissingInputException : Exception
    {
        public MissingInputException()
        {
        }

        public MissingInputException(string message) : base(message)
        {
        }

        public MissingInputException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingInputException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}