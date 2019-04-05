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

        protected internal virtual IStep GetFollowLinkStep(string rel, JourneyContext context)
        {
            return new FollowLinkStep(rel, this.GetHttpClient(), context);
        }

        protected internal virtual IStep GetOpenItemStep(int index, JourneyContext context)
        {
            return new OpenItemStep(index, this.GetHttpClient(), context);
        }

        protected internal virtual IStep GetSubmitStep(string rel, JourneyContext context)
        {
            return new SubmitStep(rel, this.GetHttpClient(), context);
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