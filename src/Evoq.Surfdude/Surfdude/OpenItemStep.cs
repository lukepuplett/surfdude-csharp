using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class OpenItemStep : HttpRequestStep
    {
        public OpenItemStep(HttpClient httpClient, JourneyContext journeyContext) : base(httpClient, journeyContext)
        {
        }

        internal override Task<object> RunStepAsync(HttpRequestStep previous)
        {
            throw new System.NotImplementedException();
        }
    }
}