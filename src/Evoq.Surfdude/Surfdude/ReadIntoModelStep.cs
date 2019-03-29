using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    public class ReadIntoModelStep<TModel> : IStep where TModel : class
    {
        private HttpClient httpClient;
        private JourneyContext context;

        public ReadIntoModelStep(HttpClient httpClient, JourneyContext context)
        {
            this.httpClient = httpClient;
            this.context = context;
        }

        public TModel Model { get; internal set; }

        public object Result { get; private set; }

        public Task<object> RunAsync(IStep previous)
        {
            var response = (HttpResponseMessage)previous.Result;

            throw new System.NotImplementedException();

            // Make HTTP request.

            // this.Result = await this.HttpClient.GetAsync();
        }
    }
}