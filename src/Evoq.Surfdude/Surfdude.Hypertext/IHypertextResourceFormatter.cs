namespace Evoq.Surfdude.Hypertext
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IHypertextResourceFormatter
    {
        Task<TModel> ReadAsModelAsync<TModel>(HttpResponseMessage httpResponse);

        Task<IHypertextResource> ReadAsResourceAsync(HttpResponseMessage httpResponse);

        HttpRequestMessage BuildRequest(IDictionary<string, string> sendPairs, IHypertextControl hypertextControl);
    }
}
