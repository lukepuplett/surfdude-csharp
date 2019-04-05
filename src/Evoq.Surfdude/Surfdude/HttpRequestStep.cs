namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public abstract class HttpRequestStep : IStep
    {
        public HttpRequestStep(HttpClient httpClient, JourneyContext journeyContext)
        {
            this.HttpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            this.JourneyContext = journeyContext ?? throw new System.ArgumentNullException(nameof(journeyContext));
        }

        //

        public HypertextResource Resource { get; private set; }

        protected HttpResponseMessage Response { get; set; }

        public HttpClient HttpClient { get; }

        public JourneyContext JourneyContext { get; }

        public bool IgnoreBadResults => this.JourneyContext.IgnoreBadResults;

        public string Name => this.GetType().Name;

        //

        public async Task<HypertextResource> RunAsync(IStep previous)
        {
            this.Response = await this.InvokeRequestAsync((HttpRequestStep)previous);

            if (!this.IgnoreBadResults)
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

            var contentBytes = await this.Response.Content.ReadAsByteArrayAsync();

            var responseMediaType = this.Response.Content.Headers.ContentType?.MediaType ?? "application/json; charset=utf-8";
            var memory = contentBytes.AsMemory();

            var encodingResolver = new EncodingResolver();
            var encoding = encodingResolver.ResolveEncoding(responseMediaType, this.JourneyContext.DefaultEncoding);

            this.Resource = new HypertextResource(memory, encoding);

            return this.Resource;
        }



        internal abstract Task<HttpResponseMessage> InvokeRequestAsync(HttpRequestStep previous);
    }
}