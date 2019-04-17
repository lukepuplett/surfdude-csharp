namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class MissingInputException : HypertextException
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