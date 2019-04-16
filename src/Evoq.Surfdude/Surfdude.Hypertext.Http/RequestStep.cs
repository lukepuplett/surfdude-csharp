namespace Evoq.Surfdude.Hypertext.Http
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    internal class RequestStep : HttpStep
    {
        public RequestStep(string rel, HttpClient httpClient, JourneyContext journeyContext, IHypertextResourceFormatter resourceFormatter)
            : base(httpClient, journeyContext, resourceFormatter)
        {
            if (string.IsNullOrWhiteSpace(rel))
            {
                throw new ArgumentNullOrWhitespaceException(nameof(rel));
            }

            this.Rel = rel;
        }

        //

        public string Rel { get; }

        //

        internal override async Task<HttpResponseMessage> ExecuteStepRequestAsync(HttpStep previous, CancellationToken cancellationToken)
        {
            IHypertextControl control = previous.Resource.GetControl(this.Rel);

            return await this.HttpClient.GetAsync(control.HRef, cancellationToken);
        }
    }
}