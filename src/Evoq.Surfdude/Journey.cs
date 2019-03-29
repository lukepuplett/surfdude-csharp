namespace Evoq.Surfdude
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Journey : IJourneyStart, IJourneySteps
    {
        private List<Step> steps = new List<Step>(20);

        //

        internal Journey(JourneyContext context, StepFactory stepFactory = null)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.StepFactory = stepFactory ?? new StepFactory();
        }

        //

        public static IJourneyStart Start(JourneyContext options)
        {
            return new Journey(options);
        }

        //

        public JourneyContext Context { get; }

        public StepFactory StepFactory { get; }

        //

        Task<JourneyReport> IJourneySteps.RunAsync()
        {
            throw new NotImplementedException();
        }

        IJourneySteps IJourneyStart.FromRoot()
        {
            FromRootStep step = this.StepFactory.GetFromRoot(this.Context);
        }

        IJourneySteps IJourneySteps.FollowLink(string relation)
        {
            throw new NotImplementedException();
        }

        IJourneySteps IJourneySteps.OpenItem(int index)
        {
            throw new NotImplementedException();
        }

        IJourneySteps IJourneySteps.Submit(string relation, object form)
        {
            throw new NotImplementedException();
        }
    }
}