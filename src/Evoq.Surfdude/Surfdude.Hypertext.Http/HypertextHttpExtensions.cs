namespace Evoq.Surfdude.Hypertext.Http
{
    using System.Linq;
    using System.Net.Http;

    public static class HypertextHttpExtensions
    {
        public static bool TryParseIfMatch(this IHypertextControl hypertextControl, out string ifMatch)
        {
            ifMatch = hypertextControl.ControlData.FirstOrDefault(cd => cd.Key == HttpControlData.IfMatchControlName).Value;

            return ifMatch != null;
        }

        public static HttpMethod DetermineHttpMethod(this IHypertextControl hypertextControl)
        {
            var firstMethodValue = hypertextControl.ControlData.FirstOrDefault(cd => cd.Key == HttpControlData.MethodControlName).Value?.ToLowerInvariant();

            if (firstMethodValue == null)
            {
                return HttpMethod.Get;
            }
            else
            {
                return new HttpMethod(firstMethodValue);
            }
        }

        public static bool SupportsRequestBody(this IHypertextControl hypertextControl)
        {
            var method = hypertextControl.DetermineHttpMethod();

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
