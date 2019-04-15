using Evoq.Surfdude.Hypertext;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class FromRootStep : HttpRequestStep
    {
        public FromRootStep(HttpClient httpClient, JourneyContext journeyContext, Func<HttpContent, Task<IHypertextResource>> readResource)
            : base(httpClient, journeyContext, readResource)
        {
        }

        internal async override Task<HttpResponseMessage> InvokeRequestAsync(HttpRequestStep previous)
        {
            return await this.HttpClient.GetAsync(this.JourneyContext.StartingLocation);
        }
    }
}