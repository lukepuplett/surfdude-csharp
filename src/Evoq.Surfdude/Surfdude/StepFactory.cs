namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using Evoq.Surfdude.Hypertext.Http;
    using System;
    using System.Net.Http;

    public class StepFactory // Refactor into IServiceProvider.Get<FromRootStep>()
    {
        public StepFactory(JourneyContext context, IHypertextResourceFormatter resourceFormatter)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
            this.ResourceFormatter = resourceFormatter ?? throw new ArgumentNullException(nameof(resourceFormatter));
        }

        //

        public JourneyContext JourneyContext { get; }

        private IHypertextResourceFormatter ResourceFormatter { get; }

        //

        protected internal virtual IStep GetFromRootStep(JourneyContext context)
        {
            return new FromRootStep(this.GetHttpClient(), context, this.ResourceFormatter);
        }

        protected internal virtual IStep GetRequestStep(string rel, JourneyContext context)
        {
            return new RequestStep(rel, this.GetHttpClient(), context, this.ResourceFormatter);
        }

        protected internal virtual IStep GetRequestItemStep(int index, JourneyContext context)
        {
            return new RequestItemStep(index, this.GetHttpClient(), context, this.ResourceFormatter);
        }

        protected internal virtual IStep GetSubmitStep(string rel, object transferObject, JourneyContext context)
        {
            return new SubmitStep(rel, transferObject, this.GetHttpClient(), context, this.ResourceFormatter);
        }

        internal ReadStep<TModel> GetReadStep<TModel>(JourneyContext context, TModel[] models) where TModel : class
        {
            return new ReadStep<TModel>(models, this.GetHttpClient(), context, this.ResourceFormatter);
        }

        private HttpClient GetHttpClient()
        {
            return new HttpClient();
        }
    }
}