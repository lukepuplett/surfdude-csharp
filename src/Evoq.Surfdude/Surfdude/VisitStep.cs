namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class VisitStep : HttpStep
    {
        public VisitStep(string rel, HttpClient httpClient, JourneyContext journeyContext, IHypertextResourceFormatter resourceFormatter)
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

        internal override async Task<HttpResponseMessage> InvokeRequestAsync(HttpStep previous)
        {
            IHypertextControl control = previous.Resource.GetControl(this.Rel);

            return await this.HttpClient.GetAsync(control.HRef);
        }
    }
}