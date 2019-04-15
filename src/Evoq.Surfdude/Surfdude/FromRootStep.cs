using Evoq.Surfdude.Hypertext;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class FromRootStep : HttpStep
    {
        public FromRootStep(HttpClient httpClient, JourneyContext journeyContext, IHypertextResourceFormatter resourceFormatter)
            : base(httpClient, journeyContext, resourceFormatter)
        {
        }

        internal async override Task<HttpResponseMessage> InvokeRequestAsync(HttpStep previous)
        {
            return await this.HttpClient.GetAsync(this.JourneyContext.StartingLocation);
        }
    }
}