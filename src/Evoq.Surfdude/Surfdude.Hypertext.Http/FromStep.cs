using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Evoq.Surfdude.Hypertext.Http
{
    internal class FromStep : HttpStep
    {
        public FromStep(HttpStepContext stepContext, string uri)
            : base(stepContext)
        {
            this.Uri = uri ?? throw new System.ArgumentNullException(nameof(uri));
        }

        public string Uri { get; private set; }

        internal override async Task<HttpResponseMessage> ExecuteStepRequestAsync(HttpStep previous, CancellationToken cancellationToken)
        {
            return await this.StepContext.HttpClient.GetAsync(this.Uri, cancellationToken);
        }
    }
}