namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext.SimpleJson;
    using System;
    using System.Net.Http;

    public class StepFactory
    {
        public StepFactory(JourneyContext context)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
            this.ResourceReader = new SimpleJsonResourceReader(context);
        }

        //

        public JourneyContext JourneyContext { get; }

        private SimpleJsonResourceReader ResourceReader { get; }

        //

        protected internal virtual IStep GetFromRootStep(JourneyContext context)
        {
            return new FromRootStep(this.GetHttpClient(), context, this.ResourceReader.ReadResourceAsync);
        }

        protected internal virtual IStep GetFollowLinkStep(string rel, JourneyContext context)
        {
            return new FollowLinkStep(rel, this.GetHttpClient(), context, this.ResourceReader.ReadResourceAsync);
        }

        protected internal virtual IStep GetOpenItemStep(int index, JourneyContext context)
        {
            return new OpenItemStep(index, this.GetHttpClient(), context, this.ResourceReader.ReadResourceAsync);
        }

        protected internal virtual IStep GetSubmitStep(string rel, JourneyContext context)
        {
            return new SubmitStep(rel, this.GetHttpClient(), context, this.ResourceReader.ReadResourceAsync);
        }

        protected internal virtual IStep GetFinalStep(JourneyContext context)
        {
            return new FinalStep(this.GetHttpClient(), context, this.ResourceReader.ReadResourceAsync);
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