using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Evoq.Surfdude.Hypertext
{
    public class HypertextResource : IHypertextControls
    {
        public HypertextResource(ReadOnlyMemory<byte> resourceBytes, Encoding encoding)
        {
            this.ResourceBytes = resourceBytes;
            this.Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        //

        public ReadOnlyMemory<byte> ResourceBytes { get; }

        public Encoding Encoding { get; }

        //

        private HypertextDocumentModel GetDocument()
        {
            // TODO / Negotiate deserializer.

            StreamReader textReader = new StreamReader(new MemoryStream(this.ResourceBytes.ToArray()), this.Encoding);

            Newtonsoft.Json.JsonTextReader jsonTextReader = new Newtonsoft.Json.JsonTextReader(textReader);
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

            return serializer.Deserialize<HypertextDocumentModel>(jsonTextReader);
        }

        public HypertextControl GetControl(string rel)
        {
            HypertextDocumentModel document = this.GetDocument();

            return document.Links.GetControl(rel);
        }

        public IHypertextControls GetItemControls(int index)
        {
            var subDocuments = this.GetDocument().Items.ToArray();

            return subDocuments[index].Links;
        }
    }
}
