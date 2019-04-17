namespace Evoq.Surfdude
{
    [System.Serializable]
    public abstract class SurfException : System.Exception
    {
        public SurfException() { }

        public SurfException(string message) : base(message) { }

        public SurfException(string message, System.Exception inner) : base(message, inner) { }

        protected SurfException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}