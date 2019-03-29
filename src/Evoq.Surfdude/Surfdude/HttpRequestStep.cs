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

        public HttpClient HttpClient { get; }

        public JourneyContext JourneyContext { get; }

        //

        public async Task<object> RunAsync(IStep previous)
        {
            this.Result = await this.RunInternalAsync(previous);

            return Task.FromResult(this.Result);
        }

        internal abstract Task<object> RunInternalAsync(IStep previous);
    }
}