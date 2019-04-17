namespace Evoq.Surfdude.Hypertext
{
    [System.Serializable]
    public abstract class HypertextException : SurfException
    {
        public HypertextException() { }

        public HypertextException(string message) : base(message) { }

        public HypertextException(string message, System.Exception inner) : base(message, inner) { }

        protected HypertextException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}