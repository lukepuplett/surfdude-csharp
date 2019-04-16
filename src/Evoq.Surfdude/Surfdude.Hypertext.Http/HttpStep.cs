namespace Evoq.Surfdude.Hypertext.Http
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public abstract class HttpStep : IStep
    {
        public HttpStep(HttpClient httpClient, JourneyContext journeyContext, IHypertextResourceFormatter resourceFormatter)
        {
            this.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.JourneyContext = journeyContext ?? throw new ArgumentNullException(nameof(journeyContext));
            this.ResourceFormatter = resourceFormatter ?? throw new ArgumentNullException(nameof(resourceFormatter));
        }

        //

        public IHypertextResource Resource { get; private set; }

        public string Name => this.GetType().Name;

        //

        protected internal HttpResponseMessage Response { get; set; }

        protected IHypertextResourceFormatter ResourceFormatter { get; }

        protected JourneyContext JourneyContext { get; }

        protected HttpClient HttpClient { get; }

        //

        public async Task RunAsync(IStep previous)
        {
            this.JourneyContext.CancellationToken.ThrowIfCancellationRequested();

            this.Response = await this.ExecuteStepRequestAsync((HttpStep)previous);

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

            this.Resource = await this.ResourceFormatter.FromResponseAsync(this.Response);
        }

        internal abstract Task<HttpResponseMessage> ExecuteStepRequestAsync(HttpStep previous);
    }
}