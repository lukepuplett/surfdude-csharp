using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class FromRootStep : HttpRequestStep
    {
        public FromRootStep(HttpClient httpClient, JourneyContext journeyContext) : base(httpClient, journeyContext)
        {
        }

        internal async override Task<object> RunStepAsync(HttpRequestStep previous)
        {
            return await this.HttpClient.GetAsync(this.JourneyContext.StartingLocation);
        }
    }
}