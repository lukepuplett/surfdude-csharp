using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class OpenItemStep : HttpRequestStep
    {
        public OpenItemStep(int index, HttpClient httpClient, JourneyContext journeyContext) : base(httpClient, journeyContext)
        {
            this.Index = index;
        }

        //

        public int Index { get; }

        //

        internal override Task<HttpResponseMessage> InvokeRequestAsync(HttpRequestStep previous)
        {
            throw new System.NotImplementedException();
        }
    }
}