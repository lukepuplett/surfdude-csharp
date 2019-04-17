using System;
using System.Collections.Generic;
using System.Text;

namespace Evoq.Surfdude
{
    public static class SurfdudeExtensions
    {
        public static ISurfSteps Submit(this ISurfSteps journey, string relation, IDictionary<string, string> transferValues)
        {
            return journey.Submit(relation, transferObject: transferValues);
        }

        public static ISurfSteps Read<TModel>(this ISurfSteps journey, out Func<TModel> get) where TModel : class
        {
            TModel[] models = new TModel[1];

            get = () => models[0];

            return journey.Read<TModel>(models);
        }
    }
}
