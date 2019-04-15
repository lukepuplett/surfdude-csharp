namespace Evoq.Surfdude
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Journey : IJourneyStart, IJourneySteps
    {
        private readonly List<IStep> steps = new List<IStep>(20);

        //

        internal Journey(JourneyContext context, StepFactory stepFactory = null)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
            this.StepFactory = stepFactory ?? new StepFactory(context);
        }

        //

        public static IJourneyStart Start(string location)
        {
            return Start(new JourneyContext(location));
        }

        public static IJourneyStart Start(JourneyContext context)
        {
            return new Journey(context);
        }

        //

        public JourneyContext JourneyContext { get; }

        public StepFactory StepFactory { get; }

        //

        async Task<JourneyReport> IJourneySteps.RunAsync(System.Threading.CancellationToken cancellationToken)
        {
            var finalStep = this.StepFactory.GetFinalStep(this.JourneyContext);
            this.steps.Add(finalStep);
            
            IStep previous = null;
            int stepCount = 1;

            var report = new JourneyReport();
            
            report.AppendStarted();

            foreach (var step in steps)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    report.AppendCancelled();
                    return report;
                }

                report.AppendStep(step.Name);

                try
                {
                    await step.RunAsync(previous);
                }
                catch (StepFailedException stepFailed)
                {
                    report.AppendException(stepFailed);
                    return report;
                }
                finally
                {
                    report.AppendStopped();
                }

                stepCount++;
                previous = step;
            }

            return report;
        }

        IJourneySteps IJourneyStart.FromRoot()
        {
            this.steps.Add(this.StepFactory.GetFromRootStep(this.JourneyContext));

            return this;
        }

        IJourneySteps IJourneySteps.FollowLink(string rel)
        {
            this.steps.Add(this.StepFactory.GetFollowLinkStep(rel, this.JourneyContext));

            return this;
        }

        IJourneySteps IJourneySteps.OpenItem(int index)
        {
            this.steps.Add(this.StepFactory.GetOpenItemStep(index, this.JourneyContext));

            return this;
        }

        IJourneySteps IJourneySteps.Submit(string rel, object form)
        {
            this.steps.Add(this.StepFactory.GetSubmitStep(rel, this.JourneyContext));

            return this;
        }

        IJourneySteps IJourneySteps.Read<TModel>(out TModel model)
        {
            var step = this.StepFactory.GetReadIntoModelStep<TModel>(this.JourneyContext);

            this.steps.Add(step);

            model = step.Model;

            return this;
        }
    }
}