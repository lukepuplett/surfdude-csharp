namespace Evoq.Surfdude.Hypertext
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class UnsupportedMediaTypeException : HypertextException
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