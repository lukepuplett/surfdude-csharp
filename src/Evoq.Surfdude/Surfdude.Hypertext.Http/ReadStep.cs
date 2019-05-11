namespace Evoq.Surfdude.Hypertext.Http
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Evoq.Surfdude.Hypertext;

    internal class ReadStep<TModel> : HttpStep where TModel : class
    {
        private readonly TModel[] models;

        public ReadStep(HttpStepContext stepContext, TModel[] models)
            : base(stepContext)
        {
            this.models = models ?? throw new System.ArgumentNullException(nameof(models));
        }

        //

        internal override async Task<HttpResponseMessage> ExecuteStepRequestAsync(HttpStep previous, CancellationToken cancellationToken)
        {
            var m = await this.StepContext.ResourceFormatter.ReadAsModelAsync<TModel>(previous.Response);

            models[0] = m;

            return previous.Response;
        }
    }
}