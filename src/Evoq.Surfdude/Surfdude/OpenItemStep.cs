using Evoq.Surfdude.Hypertext;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class OpenItemStep : HttpRequestStep
    {
        public OpenItemStep(int index, HttpClient httpClient, JourneyContext journeyContext, Func<HttpContent, Task<IHypertextResource>> readResource)
            : base(httpClient, journeyContext, readResource)
        {
            this.Index = index;
        }

        //

        public int Index { get; }

        //

        internal override Task<HttpResponseMessage> InvokeRequestAsync(HttpRequestStep previous)
        {
            var item = previous.Resource.GetItem(this.Index).GetControl("item");

            return this.HttpClient.GetAsync(item.HRef);
        }
    }
}