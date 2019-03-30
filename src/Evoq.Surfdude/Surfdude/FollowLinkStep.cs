using Evoq.Surfdude.Hypermedia;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class FollowLinkStep : HttpRequestStep
    {
        public FollowLinkStep(HttpClient httpClient, JourneyContext journeyContext) : base(httpClient, journeyContext)
        {
        }

        internal override Task<object> RunStepAsync(HttpRequestStep previous)
        {
            var linkReader = new HyperlinkReader();

            throw new NotImplementedException(nameof(FollowLinkStep));
        }
    }
}