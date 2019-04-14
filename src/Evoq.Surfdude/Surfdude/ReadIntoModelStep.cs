using System.Net.Http;
using System.Threading.Tasks;
using Evoq.Surfdude.Hypertext;

namespace Evoq.Surfdude
{
    public class ReadIntoModelStep<TModel> : IStep where TModel : class
    {
        private readonly HttpClient httpClient;
        private readonly JourneyContext context;

        public ReadIntoModelStep(HttpClient httpClient, JourneyContext context)
        {
            this.httpClient = httpClient;
            this.context = context;
        }

        public TModel Model { get; internal set; }

        public string Name => "ReadModelIntoStep<TModel>";

        public IHypertextResource Resource => throw new System.NotImplementedException();

        public Task RunAsync(IStep previous)
        {
            throw new System.NotImplementedException();

            // Make HTTP request.

            // this.Result = await this.HttpClient.GetAsync();
        }
    }
}