namespace Evoq.Surfdude
{
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

        public object Result { get; private set; }

        protected internal HttpResponseMessage Response => (HttpResponseMessage)this.Result;

        public HttpClient HttpClient { get; }

        public JourneyContext JourneyContext { get; }

        public bool IgnoreBadResults => this.JourneyContext.IgnoreBadResults;

        public byte[] ContentBytes { get; private set; }

        //

        public async Task<object> RunAsync(IStep previous)
        {
            this.Result = await this.RunStepAsync((HttpRequestStep)previous);

            if (!this.IgnoreBadResults)
            {
                this.Response.EnsureSuccessStatusCode();
            }

            this.ContentBytes = await this.Response.Content.ReadAsByteArrayAsync();

            return Task.FromResult(this.Result);
        }

        internal abstract Task<object> RunStepAsync(HttpRequestStep previous);
    }
}