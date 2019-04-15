using Evoq.Surfdude.Hypertext;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class FinalStep : HttpStep
    {
        public FinalStep(HttpClient httpClient, JourneyContext journeyContext, IHypertextResourceFormatter resourceFormatter)
            : base(httpClient, journeyContext, resourceFormatter)
        {
        }

        internal override Task<HttpResponseMessage> InvokeRequestAsync(HttpStep previous)
        {
            throw new System.NotImplementedException();
        }
    }
}