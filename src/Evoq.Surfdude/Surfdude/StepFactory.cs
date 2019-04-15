namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using Evoq.Surfdude.Hypertext.Http;
    using Evoq.Surfdude.Hypertext.SimpleJson;
    using System;
    using System.Net.Http;

    public class StepFactory // Refactor into IServiceProvider.Get<FromRootStep>()
    {
        public StepFactory(JourneyContext context)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
            this.ResourceFormatter = new SimpleJsonResourceReaderWriter(context);
        }

        //

        public JourneyContext JourneyContext { get; }

        private IHypertextResourceFormatter ResourceFormatter { get; }

        //

        protected internal virtual IStep GetFromRootStep(JourneyContext context)
        {
            return new FromRootStep(this.GetHttpClient(), context, this.ResourceFormatter);
        }

        protected internal virtual IStep GetVisitStep(string rel, JourneyContext context)
        {
            return new VisitStep(rel, this.GetHttpClient(), context, this.ResourceFormatter);
        }

        protected internal virtual IStep GetVisitItemStep(int index, JourneyContext context)
        {
            return new VisitItemStep(index, this.GetHttpClient(), context, this.ResourceFormatter);
        }

        protected internal virtual IStep GetSendStep(string rel, object transferObject, JourneyContext context)
        {
            return new SendStep(rel, transferObject, this.GetHttpClient(), context, this.ResourceFormatter);
        }

        internal ReadIntoModelStep<TModel> GetReadIntoModelStep<TModel>(JourneyContext context) where TModel : class
        {
            return new ReadIntoModelStep<TModel>(this.GetHttpClient(), context);
        }

        private HttpClient GetHttpClient()
        {
            return new HttpClient();
        }
    }
}