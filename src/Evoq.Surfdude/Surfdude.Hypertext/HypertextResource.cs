namespace Evoq.Surfdude.Hypertext
{
    using Evoq.Surfdude.Hypertext.SimpleJson;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class HypertextResource : IHypertextResource
    {
        public HypertextResource(ReadOnlyMemory<byte> resourceBytes, Encoding encoding)
        {
            this.ResourceBytes = resourceBytes;
            this.Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        //

        internal ReadOnlyMemory<byte> ResourceBytes { get; }

        internal Encoding Encoding { get; }

        //

        public IHypertextControl GetControl(string rel)
        {
            return this.GetDocument().GetControl(rel);
        }

        public IHypertextControls GetItem(int index)
        {
            return this.GetDocument().GetItem(index);
        }

        //

        private SimpleDocumentModel GetDocument()
        {
            // TODO / Negotiate deserializer.

            StreamReader textReader = new StreamReader(new MemoryStream(this.ResourceBytes.ToArray()), this.Encoding);

            Newtonsoft.Json.JsonTextReader jsonTextReader = new Newtonsoft.Json.JsonTextReader(textReader);
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

            return serializer.Deserialize<SimpleDocumentModel>(jsonTextReader);
        }
    }
}
