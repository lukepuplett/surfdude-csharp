using System;
using System.Collections.Generic;
using System.Text;

namespace Evoq.Surfdude
{
    public static class SurfdudeExtensions
    {
        public static IWaveSteps ThenSubmit(this IWaveSteps builder, string relation, IDictionary<string, string> transferValues)
        {
            return builder.ThenSubmit(relation, transferObject: transferValues);
        }

        public static IWaveSteps ThenRead<TModel>(this IWaveSteps builder, out Func<TModel> get) where TModel : class
        {
            TModel[] models = new TModel[1];

            get = () => models[0];

            return builder.ThenRead<TModel>(models);
        }
    }
}
