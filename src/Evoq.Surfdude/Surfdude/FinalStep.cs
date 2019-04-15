using Evoq.Surfdude.Hypertext;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class FinalStep : HttpRequestStep
    {
        public FinalStep(HttpClient httpClient, JourneyContext journeyContext, Func<HttpContent, Task<IHypertextResource>> readResource)
            : base(httpClient, journeyContext, readResource)
        {
        }

        internal override Task<HttpResponseMessage> InvokeRequestAsync(HttpRequestStep previous)
        {
            throw new System.NotImplementedException();
        }
    }
}