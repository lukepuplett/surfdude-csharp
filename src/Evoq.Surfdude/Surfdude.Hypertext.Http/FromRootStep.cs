using Evoq.Surfdude.Hypertext;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude.Hypertext.Http
{
    internal class FromRootStep : HttpStep
    {
        public FromRootStep(HttpClient httpClient, JourneyContext journeyContext, IHypertextResourceFormatter resourceFormatter)
            : base(httpClient, journeyContext, resourceFormatter)
        {
        }

        internal async override Task<HttpResponseMessage> ExecuteStepRequestAsync(HttpStep previous)
        {
            return await this.HttpClient.GetAsync(this.JourneyContext.RootUri);
        }
    }
}