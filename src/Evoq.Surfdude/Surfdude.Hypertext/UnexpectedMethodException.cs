namespace Evoq.Surfdude.Hypertext
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class UnexpectedMethodException : HypertextException
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