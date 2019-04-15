namespace Evoq.Surfdude.Hypertext.SimpleJson
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class SimpleJsonResourceReaderWriter : IHypertextResourceFormatter
    {
        public SimpleJsonResourceReaderWriter(JourneyContext context)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        //

        public JourneyContext JourneyContext { get; }

        //

        public async Task<IHypertextResource> ReadResourceAsync(HttpContent httpContent)
        {
            var contentBytes = await httpContent.ReadAsByteArrayAsync();
            var memory = contentBytes.AsMemory();

            var encodingResolver = new EncodingResolver();
            var responseMediaType = httpContent.Headers.ContentType?.MediaType ?? "application/json; charset=utf-8";
            var encoding = encodingResolver.ResolveEncoding(responseMediaType, this.JourneyContext.DefaultEncoding);

            StreamReader textReader = new StreamReader(new MemoryStream(contentBytes), encoding);

            Newtonsoft.Json.JsonTextReader jsonTextReader = new Newtonsoft.Json.JsonTextReader(textReader);
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            
            var documentModel = serializer.Deserialize<SimpleDocumentModel>(jsonTextReader);

            return new SimpleHypertextResource(documentModel);
        }
    }
}
