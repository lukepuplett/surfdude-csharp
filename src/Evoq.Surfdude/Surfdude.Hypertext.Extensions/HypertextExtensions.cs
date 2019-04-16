namespace Evoq.Surfdude.Hypertext
{
    using Evoq.Surfdude.Hypertext.Http;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    public static class HypertextExtensions
    {
        public static IEnumerable<IHypertextInputControl> GetRequiredHypertextControls(this IEnumerable<IHypertextInputControl> inputs)
        {
            return inputs.Where(i => !i.IsOptional).ToArray();
        }

        public static HttpMethod GetHttpMethod(this IHypertextControl hypertextControl)
        {
            var firstMethodData = hypertextControl.ControlData.FirstOrDefault(cd => cd.Key == HttpControlData.MethodControlName).Value?.ToLowerInvariant();

            if (firstMethodData == null)
            {
                return HttpMethod.Get;
            }
            else
            {
                return new HttpMethod(firstMethodData);
            }
        }

        public static bool IsMutation(this IHypertextControl hypertextControl)
        {
            var method = hypertextControl.GetHttpMethod();

            if (method == HttpMethod.Post)
            {
                return true;
            }
            else if (method == HttpMethod.Put)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
