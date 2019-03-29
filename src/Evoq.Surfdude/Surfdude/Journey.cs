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

        async Task<JourneyReport> IJourneySteps.RunAsync(System.Threading.CancellationToken cancellationToken)
        {
            var finalStep = this.StepFactory.GetFinalStep(this.Context);
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

        private IJourneySteps AppendStep<TStep>() where TStep : IStep
        {
            var stepType = typeof(TStep);
            IStep step;

            if (stepType == typeof(FromRootStep))
            {
                step = this.StepFactory.GetFromRootStep(this.Context);
            }
            else if (stepType == typeof(FollowLinkStep))
            {
                step = this.StepFactory.GetFollowLinkStep(this.Context);
            }
            else if (stepType == typeof(OpenItemStep))
            {
                step = this.StepFactory.GetOpenItemStep(this.Context);
            }
            else if (stepType == typeof(SubmitStep))
            {
                step = this.StepFactory.GetSubmitStep(this.Context);
            }
            else if (stepType == typeof(FinalStep))
            {
                step = this.StepFactory.GetFinalStep(this.Context);
            }
            else
            {
                throw new UnexpectedStepTypeException($"Unable to create step of type '{stepType}'.");
            }

            this.steps.Add(step);

            return this;
        }

        IJourneySteps IJourneyStart.FromRoot()
        {
            return AppendStep<FromRootStep>();
        }

        IJourneySteps IJourneySteps.FollowLink(string relation)
        {
            return AppendStep<FollowLinkStep>();
        }

        IJourneySteps IJourneySteps.OpenItem(int index)
        {
            return AppendStep<OpenItemStep>();
        }

        IJourneySteps IJourneySteps.Submit(string relation, object form)
        {
            return AppendStep<SubmitStep>();
        }

        IJourneySteps IJourneySteps.Read<TModel>(out TModel model)
        {
            var step = this.StepFactory.GetReadIntoModelStep<TModel>(this.Context);

            this.steps.Add(step);

            model = step.Model;

            return this;
        }
    }
}