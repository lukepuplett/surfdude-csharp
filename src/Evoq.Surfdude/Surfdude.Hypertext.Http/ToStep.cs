namespace Evoq.Surfdude.Hypertext.Http
{
    using Evoq.Surfdude.Hypertext;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ToStep : HttpStep
    {
        public ToStep(HttpStepContext stepContext, string rel)
            : base(stepContext)
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

        internal override async Task<HttpResponseMessage> ExecuteStepRequestAsync(HttpStep previous, CancellationToken cancellationToken)
        {
            IHypertextControl control = previous.Resource.GetControl(this.Rel);

            return await this.StepContext.HttpClient.GetAsync(control.HRef, cancellationToken);
        }
    }
}