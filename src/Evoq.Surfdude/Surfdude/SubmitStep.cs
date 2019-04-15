using Evoq.Surfdude.Hypertext;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    public class SubmitStep : HttpRequestStep
    {
        public SubmitStep(string rel, HttpClient httpClient, JourneyContext journeyContext, Func<HttpContent, Task<IHypertextResource>> readResource)
            : base(httpClient, journeyContext, readResource)
        {
        }

        internal override Task<HttpResponseMessage> InvokeRequestAsync(HttpRequestStep previous)
        {
            throw new System.NotImplementedException();
        }
    }
}