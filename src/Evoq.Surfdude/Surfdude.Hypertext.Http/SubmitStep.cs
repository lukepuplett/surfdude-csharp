namespace Evoq.Surfdude.Hypertext.Http
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class SubmitStep : HttpStep
    {
        public SubmitStep(string rel, object sendModel, HttpClient httpClient, JourneyContext journeyContext, IHypertextResourceFormatter resourceFormatter)
            : base(httpClient, journeyContext, resourceFormatter)
        {
            this.Rel = rel ?? throw new ArgumentNullException(nameof(rel));
            this.SendModel = sendModel ?? throw new ArgumentNullException(nameof(sendModel));
        }

        //

        public string Rel { get; }

        public object SendModel { get; }

        //

        internal override Task<HttpResponseMessage> ExecuteStepRequestAsync(HttpStep previous)
        {
            var senderControl = previous.Resource.GetControl(this.Rel);
            
            var httpRequest = this.ResourceFormatter.ToRequest(new SendDictionary(this.SendModel), senderControl);

            return this.HttpClient.SendAsync(httpRequest);
        }
    }
}