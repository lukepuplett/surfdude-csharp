using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class FromRootStep : HttpRequestStep
    {
        public FromRootStep(HttpClient httpClient, JourneyContext journeyContext) : base(httpClient, journeyContext)
        {
        }

        internal override Task<object> RunInternalAsync(IStep previous)
        {
            throw new System.NotImplementedException();
        }
    }
}