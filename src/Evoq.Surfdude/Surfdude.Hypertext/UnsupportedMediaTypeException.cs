using System;
using System.Runtime.Serialization;

namespace Evoq.Surfdude.Hypertext
{
    [Serializable]
    internal class UnsupportedMediaTypeException : Exception
    {
        public UnsupportedMediaTypeException()
        {
        }

        public UnsupportedMediaTypeException(string message) : base(message)
        {
        }

        public UnsupportedMediaTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedMediaTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}