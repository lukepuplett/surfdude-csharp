using System;
using System.Net.Http;

namespace Evoq.Surfdude
{
    public class StepFactory
    {
        private HttpClient GetHttpClient()
        {
            return new HttpClient();
        }

        //

        protected internal virtual IStep GetFromRootStep(JourneyContext context)
        {
            return new FromRootStep(this.GetHttpClient(), context);
        }

        protected internal virtual IStep GetFollowLinkStep(JourneyContext context)
        {
            return new FollowLinkStep(this.GetHttpClient(), context);
        }

        protected internal virtual IStep GetOpenItemStep(JourneyContext context)
        {
            return new OpenItemStep(this.GetHttpClient(), context);
        }

        protected internal virtual IStep GetSubmitStep(JourneyContext context)
        {
            return new SubmitStep(this.GetHttpClient(), context);
        }

        protected internal virtual IStep GetFinalStep(JourneyContext context)
        {
            return new FinalStep(this.GetHttpClient(), context);
        }

        internal ReadIntoModelStep<TModel> GetReadIntoModelStep<TModel>(JourneyContext context) where TModel : class
        {
            return new ReadIntoModelStep<TModel>(this.GetHttpClient(), context);
        }
    }
}