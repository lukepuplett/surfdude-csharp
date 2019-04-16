using System;

namespace Evoq.Surfdude
{
    public class ArgumentNullOrWhitespaceException : ArgumentException
    {
        public ArgumentNullOrWhitespaceException()
        {
        }

        public ArgumentNullOrWhitespaceException(string paramName) 
            : base("The argument cannot be null or whitespace.", paramName)
        { }
    }
}