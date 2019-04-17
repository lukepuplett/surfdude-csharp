namespace Evoq.Surfdude.Hypertext.Http
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class HttpStep : IStep
    {
        public HttpStep(HttpClient httpClient, RideContext journeyContext, IHypertextResourceFormatter resourceFormatter)
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

        protected RideContext JourneyContext { get; }

        protected HttpClient HttpClient { get; }

        //

        public async Task ExecuteAsync(IStep previous, CancellationToken cancellationToken)
        {
            this.Response = await this.ExecuteStepRequestAsync((HttpStep)previous, cancellationToken);

            if (!this.IsExpectedStatus(this.Response.StatusCode))
            {
                try
                {
                    this.Response.EnsureSuccessStatusCode();

                    throw new UnexpectedHttpResponseException(
                        $"The HTTP request failed with status {(int)this.Response.StatusCode} {this.Response.StatusCode}.");
                }
                catch (HttpRequestException httpRequestException)
                {
                    throw new UnexpectedHttpResponseException(
                        $"The HTTP request failed with status {(int)this.Response.StatusCode} {this.Response.StatusCode}. See inner exception.", httpRequestException);
                }
            }

            this.Resource = await this.ResourceFormatter.ReadAsResourceAsync(this.Response);
        }

        private bool IsExpectedStatus(HttpStatusCode statusCode)
        {
            return this.JourneyContext.ExpectedStatusCodes.Contains((int)statusCode);
        }

        internal abstract Task<HttpResponseMessage> ExecuteStepRequestAsync(HttpStep previous, CancellationToken cancellationToken);
    }
}