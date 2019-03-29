using System;

namespace Evoq.Surfdude
{
    internal class StepFailedException : Exception
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