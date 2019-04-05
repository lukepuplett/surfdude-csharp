using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class FollowLinkStep : HttpRequestStep
    {
        public FollowLinkStep(string rel, HttpClient httpClient, JourneyContext journeyContext) : base(httpClient, journeyContext)
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

        internal async override Task<HttpResponseMessage> InvokeRequestAsync(HttpRequestStep previous)
        {
            var control = previous.Resource.GetControl(this.Rel);

            return await this.HttpClient.GetAsync(control.HRef);
        }
    }
}