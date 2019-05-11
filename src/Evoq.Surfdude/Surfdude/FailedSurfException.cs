namespace Evoq.Surfdude
{
    [System.Serializable]
    public abstract class FailedSurfException : System.Exception
    {
        public FailedSurfException() { }

        public FailedSurfException(string message) : base(message) { }

        public FailedSurfException(string message, System.Exception inner) : base(message, inner) { }

        protected FailedSurfException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}