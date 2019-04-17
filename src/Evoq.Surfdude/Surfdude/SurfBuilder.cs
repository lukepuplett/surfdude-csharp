namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class SurfBuilder : ISurfStart, ISurfSteps
    {
        private readonly List<IStep> steps = new List<IStep>(20);

        //
        public SurfBuilder() { }

        protected SurfBuilder(RideContext context, IHypertextResourceFormatter resourceFormatter)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
            this.StepFactory = new StepFactory(context, resourceFormatter);
        }

        protected SurfBuilder(RideContext context, StepFactory stepFactory)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
            this.StepFactory = stepFactory ?? throw new ArgumentNullException(nameof(stepFactory));
        }

        //

        protected RideContext JourneyContext { get; }

        protected StepFactory StepFactory { get; }

        //

        public ISurfSteps FromRoot()
        {
            this.steps.Add(this.StepFactory.GetFromRootStep(this.JourneyContext));

            return this;
        }

        //

        async Task<SurfReport> ISurfSteps.RideItAsync(CancellationToken cancellationToken)
        {            
            IStep previous = null;
            int stepCount = 1;

            var report = new SurfReport();
            
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
                    await step.ExecuteAsync(previous, cancellationToken);
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

        ISurfSteps ISurfSteps.To(string rel)
        {
            this.steps.Add(this.StepFactory.GetToStep(rel, this.JourneyContext));

            return this;
        }

        ISurfSteps ISurfSteps.ToItem(int index)
        {
            this.steps.Add(this.StepFactory.GetToItemStep(index, this.JourneyContext));

            return this;
        }

        ISurfSteps ISurfSteps.Submit(string rel, object transferObject)
        {
            this.steps.Add(this.StepFactory.GetSubmitStep(rel, transferObject, this.JourneyContext));

            return this;
        }

        ISurfSteps ISurfSteps.Read<TModel>(TModel[] models) 
        {
            var step = this.StepFactory.GetReadStep<TModel>(this.JourneyContext, models);

            this.steps.Add(step);

            return this;
        }
    }
}