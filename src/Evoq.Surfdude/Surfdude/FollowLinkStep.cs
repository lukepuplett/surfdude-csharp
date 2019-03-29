using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class FollowLinkStep : HttpRequestStep
    {
        public FollowLinkStep(HttpClient httpClient, JourneyContext journeyContext) : base(httpClient, journeyContext)
        {
        }

        internal override Task<object> RunInternalAsync(IStep previous)
        {
            throw new System.NotImplementedException();
        }
    }
}