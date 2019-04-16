namespace Evoq.Surfdude.Hypertext
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IHypertextResourceFormatter
    {
        Task<IHypertextResource> FromResponseAsync(HttpResponseMessage httpResponse);

        HttpRequestMessage ToRequest(IDictionary<string, string> sendPairs, IHypertextControl hypertextControl);
    }
}
