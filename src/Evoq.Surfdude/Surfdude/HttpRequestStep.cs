namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public abstract class HttpRequestStep : IStep
    {
        public HttpRequestStep(HttpClient httpClient, JourneyContext journeyContext, Func<HttpContent, Task<IHypertextResource>> readResource = null)
        {
            this.ReadResource = readResource ?? this.ReadResourceAsync;
            this.HttpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            this.JourneyContext = journeyContext ?? throw new System.ArgumentNullException(nameof(journeyContext));
        }

        //

        public IHypertextResource Resource { get; private set; }

        public string Name => this.GetType().Name;
        
        //

        protected HttpResponseMessage Response { get; set; }

        protected Func<HttpContent, Task<IHypertextResource>> ReadResource { get; }

        protected HttpClient HttpClient { get; }

        protected JourneyContext JourneyContext { get; }
        
        //

        public async Task RunAsync(IStep previous)
        {
            this.Response = await this.InvokeRequestAsync((HttpRequestStep)previous);

            if (!this.JourneyContext.IgnoreBadResults)
            {
                try
                {
                    this.Response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException httpRequestException)
                {
                    throw new StepFailedException("The step failed. See inner exception.", httpRequestException);
                }
            }

            this.Resource = await this.ReadResource(this.Response.Content);
        }

        private async Task<IHypertextResource> ReadResourceAsync(HttpContent httpContent)
        {
            var contentBytes = await httpContent.ReadAsByteArrayAsync();

            var responseMediaType = httpContent.Headers.ContentType?.MediaType ?? "application/json; charset=utf-8";
            var memory = contentBytes.AsMemory();

            var encodingResolver = new EncodingResolver();
            var encoding = encodingResolver.ResolveEncoding(responseMediaType, this.JourneyContext.DefaultEncoding);

            return new HypertextResource(memory, encoding);
        }

        internal abstract Task<HttpResponseMessage> InvokeRequestAsync(HttpRequestStep previous);
    }
}