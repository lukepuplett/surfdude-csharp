namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using Evoq.Surfdude.Hypertext.Http;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class WaveBuilder : IWaveStart, IWaveSteps
    {
        private readonly List<IStep> steps = new List<IStep>(20);

        //
        public WaveBuilder() { }

        protected WaveBuilder(SurfContext context, IHypertextResourceFormatter resourceFormatter)
        {
            this.SurfContext = context ?? throw new ArgumentNullException(nameof(context));
            this.StepFactory = new StepFactory(context, resourceFormatter);
        }

        protected WaveBuilder(SurfContext context, StepFactory stepFactory)
        {
            this.SurfContext = context ?? throw new ArgumentNullException(nameof(context));
            this.StepFactory = stepFactory ?? throw new ArgumentNullException(nameof(stepFactory));
        }

        //

        protected SurfContext SurfContext { get; }

        protected StepFactory StepFactory { get; }

        //

        public IWaveSteps FromRoot()
        {
            return this.From(this.SurfContext.RootUri);
        }

        public IWaveSteps From(string uri)
        {
            steps.Add(this.StepFactory.GetFromStep(uri, this.SurfContext));

            return this;
        }

        //

        async Task<SurfReport> IWaveSteps.GoAsync(CancellationToken cancellationToken)
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
                catch (UnexpectedHttpResponseException stepFailed)
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

        IWaveSteps IWaveSteps.Then(string rel)
        {
            steps.Add(this.StepFactory.GetToStep(rel, this.SurfContext));

            return this;
        }

        IWaveSteps IWaveSteps.ThenItem(int index)
        {
            steps.Add(this.StepFactory.GetToItemStep(index, this.SurfContext));

            return this;
        }

        IWaveSteps IWaveSteps.ThenSubmit(string rel, object transferObject)
        {
            steps.Add(this.StepFactory.GetSubmitStep(rel, transferObject, this.SurfContext));

            return this;
        }

        IWaveSteps IWaveSteps.ThenRead<TModel>(TModel[] models)
        {
            var step = this.StepFactory.GetReadStep<TModel>(this.SurfContext, models);

            steps.Add(step);

            return this;
        }
    }
}