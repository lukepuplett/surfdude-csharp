using System;
using System.Runtime.Serialization;

namespace Evoq.Surfdude.Hypertext
{
    [Serializable]
    public class RelationNotFoundException : HypertextException
    {
        private string v;
        private object invalidOperation;

        public RelationNotFoundException()
        {
        }

        public RelationNotFoundException(string message) : base(message)
        {
        }

        public RelationNotFoundException(string v, object invalidOperation)
        {
            this.v = v;
            this.invalidOperation = invalidOperation;
        }

        public RelationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RelationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}