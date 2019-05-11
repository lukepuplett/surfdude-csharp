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
        public HttpStep(HttpStepContext stepContext)
        {
            this.StepContext = stepContext ?? throw new ArgumentNullException(nameof(stepContext));
        }

        //

        public IHypertextResource Resource { get; private set; }

        public string Name => this.GetType().Name;

        //

        public HttpStepContext StepContext { get; }

        protected internal HttpResponseMessage Response { get; set; }

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

            this.Resource = await this.StepContext.ResourceFormatter.ReadAsResourceAsync(this.Response);
        }

        private bool IsExpectedStatus(HttpStatusCode statusCode)
        {
            return this.StepContext.SurfContext.ExpectedStatusCodes?.Contains((int)statusCode) ?? false;
        }

        internal abstract Task<HttpResponseMessage> ExecuteStepRequestAsync(HttpStep previous, CancellationToken cancellationToken);
    }
}