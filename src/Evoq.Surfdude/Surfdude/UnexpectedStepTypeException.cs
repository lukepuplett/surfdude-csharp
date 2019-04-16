using System;

namespace Evoq.Surfdude
{
    public class UnexpectedStepTypeException : Exception
    {
        public UnexpectedStepTypeException()
        {
        }

        public UnexpectedStepTypeException(string message) : base(message)
        {
        }

        public UnexpectedStepTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}