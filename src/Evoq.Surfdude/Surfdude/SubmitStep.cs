using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    public class SubmitStep : HttpRequestStep
    {
        public SubmitStep(string rel, HttpClient httpClient, JourneyContext journeyContext) : base(httpClient, journeyContext)
        {
        }

        internal override Task<HttpResponseMessage> InvokeRequestAsync(HttpRequestStep previous)
        {
            throw new System.NotImplementedException();
        }
    }
}