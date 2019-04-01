using Evoq.Surfdude.Hypermedia;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class FollowLinkStep : HttpRequestStep
    {
        public FollowLinkStep(HttpClient httpClient, JourneyContext journeyContext) : base(httpClient, journeyContext)
        {
        }

        internal async override Task<object> RunStepAsync(HttpRequestStep previous)
        {
            var reader = new HypermediaReader();
            var responseMediaType = previous.Response.Content.Headers.ContentType?.MediaType;

            var hyperlinks = await reader.ReadLinksAsync(new MemoryStream(previous.ContentBytes), responseMediaType);

            throw new NotImplementedException(nameof(FollowLinkStep));
        }
    }
}