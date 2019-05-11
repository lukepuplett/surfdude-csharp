using System;

namespace Evoq.Surfdude.Hypertext.Http
{
    public class UnexpectedHttpResponseException : FailedSurfException
    {
        public UnexpectedHttpResponseException()
        {
        }

        public UnexpectedHttpResponseException(string message) : base(message)
        {
        }

        public UnexpectedHttpResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}