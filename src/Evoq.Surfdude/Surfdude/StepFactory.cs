namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using Evoq.Surfdude.Hypertext.Http;
    using System;
    using System.Net.Http;

    public class StepFactory // Refactor into IServiceProvider.Get<FromRootStep>()
    {
        public StepFactory(RideContext context, IHypertextResourceFormatter resourceFormatter)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
            this.ResourceFormatter = resourceFormatter ?? throw new ArgumentNullException(nameof(resourceFormatter));
        }

        //

        public RideContext JourneyContext { get; }

        private IHypertextResourceFormatter ResourceFormatter { get; }

        //

        protected internal virtual IStep GetFromRootStep(RideContext context)
        {
            return new FromRootStep(this.GetHttpClient(), context, this.ResourceFormatter);
        }

        protected internal virtual IStep GetToStep(string rel, RideContext context)
        {
            return new ToStep(rel, this.GetHttpClient(), context, this.ResourceFormatter);
        }

        protected internal virtual IStep GetToItemStep(int index, RideContext context)
        {
            return new ToItemStep(index, this.GetHttpClient(), context, this.ResourceFormatter);
        }

        protected internal virtual IStep GetSubmitStep(string rel, object transferObject, RideContext context)
        {
            return new SubmitStep(rel, transferObject, this.GetHttpClient(), context, this.ResourceFormatter);
        }

        internal ReadStep<TModel> GetReadStep<TModel>(RideContext context, TModel[] models) where TModel : class
        {
            return new ReadStep<TModel>(models, this.GetHttpClient(), context, this.ResourceFormatter);
        }

        private HttpClient GetHttpClient()
        {
            return new HttpClient();
        }
    }
}