namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class SendStep : HttpStep
    {
        public SendStep(string rel, object form, HttpClient httpClient, JourneyContext journeyContext, IHypertextResourceFormatter resourceFormatter)
            : base(httpClient, journeyContext, resourceFormatter)
        {
            this.Rel = rel ?? throw new ArgumentNullException(nameof(rel));
            this.Form = form ?? throw new ArgumentNullException(nameof(form));
        }

        //

        public string Rel { get; }

        public object Form { get; }

        //

        internal override Task<HttpResponseMessage> InvokeRequestAsync(HttpStep previous)
        {
            IHypertextControl senderControl = previous.Resource.GetControl(this.Rel);

            SendFormatter formatter = new SendFormatter(this.ResourceFormatter);

            var urlAndContent = formatter.Make(this.Form, senderControl);

            throw new NotImplementedException(nameof(SendStep));
        }
    }
}