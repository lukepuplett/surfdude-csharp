using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    public class SubmitStep : HttpRequestStep
    {
        public SubmitStep(HttpClient httpClient, JourneyContext journeyContext) : base(httpClient, journeyContext)
        {
        }

        internal override Task<object> RunStepAsync(HttpRequestStep previous)
        {
            throw new System.NotImplementedException();
        }
    }
}