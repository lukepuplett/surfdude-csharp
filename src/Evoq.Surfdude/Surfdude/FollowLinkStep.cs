namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class FollowLinkStep : HttpRequestStep
    {
        public FollowLinkStep(string rel, HttpClient httpClient, JourneyContext journeyContext, Func<HttpContent, Task<IHypertextResource>> readResource)
            : base(httpClient, journeyContext, readResource)
        {
            if (string.IsNullOrWhiteSpace(rel))
            {
                throw new ArgumentNullOrWhitespaceException(nameof(rel));
            }

            this.Rel = rel;
        }

        //

        public string Rel { get; }

        //

        internal override async Task<HttpResponseMessage> InvokeRequestAsync(HttpRequestStep previous)
        {
            IHypertextControl control = previous.Resource.GetControl(this.Rel);

            return await this.HttpClient.GetAsync(control.HRef);
        }
    }
}