using System;
using System.Collections.Generic;
using System.Text;

namespace Evoq.Surfdude
{
    public static class SurfdudeExtensions
    {
        public static IWaveSteps ThenSubmit(this IWaveSteps journey, string relation, IDictionary<string, string> transferValues)
        {
            return journey.ThenSubmit(relation, transferObject: transferValues);
        }

        public static IWaveSteps ThenRead<TModel>(this IWaveSteps journey, out Func<TModel> get) where TModel : class
        {
            TModel[] models = new TModel[1];

            get = () => models[0];

            return journey.ThenRead<TModel>(models);
        }
    }
}
