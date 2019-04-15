﻿using System;
using System.Runtime.Serialization;

namespace Evoq.Surfdude
{
    [Serializable]
    internal class UnexpectedInputsException : Exception
    {
        public UnexpectedInputsException()
        {
        }

        public UnexpectedInputsException(string message) : base(message)
        {
        }

        public UnexpectedInputsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnexpectedInputsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}