using System;
using System.Collections.Generic;
using System.Text;

namespace Evoq.Surfdude
{
    public static class SurfdudeExtensions
    {
        public static IJourneySteps Submit(this IJourneySteps journey, string relation, IDictionary<string, string> transferValues)
        {
            return journey.Submit(relation, transferObject: transferValues);
        }

        public static IJourneySteps Read<TModel>(this IJourneySteps journey, out Func<TModel> get) where TModel : class
        {
            TModel[] models = new TModel[1];

            get = () => models[0];

            return journey.Read<TModel>(models);
        }
    }
}
