using System;

namespace Evoq.Surfdude
{
    public class StepFactory
    {
        internal TStep Get<TStep>(JourneyContext context) where TStep : IStep
        {
            return (TStep)this.Get(typeof(TStep), context);
        }

        protected virtual IStep Get(Type stepType, JourneyContext context)
        {
            // Potentially constrain Get<T> with new() and instantiate and setup and return.

            if (stepType == typeof(FromRootStep))
            {
                return new FromRootStep();
            }

            if (stepType == typeof(FinalStep))
            {
                return new FinalStep();
            }

            throw new UnexpectedStepTypeException($"Unable to create the step. A step of type '{stepType}' was unexpected.");
        }
    }
}