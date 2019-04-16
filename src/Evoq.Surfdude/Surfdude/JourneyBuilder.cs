namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class JourneyBuilder : IJourneyStart, IJourneySteps
    {
        private readonly List<IStep> steps = new List<IStep>(20);

        //
        public JourneyBuilder() { }

        protected JourneyBuilder(JourneyContext context, IHypertextResourceFormatter resourceFormatter)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
            this.StepFactory = new StepFactory(context, resourceFormatter);
        }

        protected JourneyBuilder(JourneyContext context, StepFactory stepFactory)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
            this.StepFactory = stepFactory ?? throw new ArgumentNullException(nameof(stepFactory));
        }

        //

        protected JourneyContext JourneyContext { get; }

        protected StepFactory StepFactory { get; }

        //

        public IJourneySteps FromRoot()
        {
            this.steps.Add(this.StepFactory.GetFromRootStep(this.JourneyContext));

            return this;
        }

        //

        async Task<JourneyReport> IJourneySteps.RunAsync(System.Threading.CancellationToken cancellationToken)
        {            
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

        IJourneySteps IJourneySteps.Request(string rel)
        {
            this.steps.Add(this.StepFactory.GetVisitStep(rel, this.JourneyContext));

            return this;
        }

        IJourneySteps IJourneySteps.RequestItem(int index)
        {
            this.steps.Add(this.StepFactory.GetVisitItemStep(index, this.JourneyContext));

            return this;
        }

        IJourneySteps IJourneySteps.Submit(string rel, object transferObject)
        {
            this.steps.Add(this.StepFactory.GetSendStep(rel, transferObject, this.JourneyContext));

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