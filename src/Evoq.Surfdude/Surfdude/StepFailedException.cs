using System;

namespace Evoq.Surfdude
{
    public class StepFailedException : Exception
    {
        public StepFailedException()
        {
        }

        public StepFailedException(string message) : base(message)
        {
        }

        public StepFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}