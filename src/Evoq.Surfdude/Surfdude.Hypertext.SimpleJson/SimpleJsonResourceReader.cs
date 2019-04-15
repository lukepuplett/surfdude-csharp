namespace Evoq.Surfdude.Hypertext.SimpleJson
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class SimpleJsonResourceReader
    {
        public SimpleJsonResourceReader(JourneyContext context)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        //

        public JourneyContext JourneyContext { get; }

        //

        public async Task<IHypertextResource> ReadResourceAsync(HttpContent httpContent)
        {
            var contentBytes = await httpContent.ReadAsByteArrayAsync();

            var responseMediaType = httpContent.Headers.ContentType?.MediaType ?? "application/json; charset=utf-8";
            var memory = contentBytes.AsMemory();

            var encodingResolver = new EncodingResolver();
            var encoding = encodingResolver.ResolveEncoding(responseMediaType, this.JourneyContext.DefaultEncoding);

            return new HypertextResource(memory, encoding);
        }
    }
}
