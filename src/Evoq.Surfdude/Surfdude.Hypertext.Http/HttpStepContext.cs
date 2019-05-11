namespace Evoq.Surfdude.Hypertext.Http
{
    using System.Net.Http;

    public class HttpStepContext
    {
        public HttpStepContext(HttpClient httpClient, SurfContext surfContext, IHypertextResourceFormatter resourceFormatter)
        {
            this.HttpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            this.SurfContext = surfContext ?? throw new System.ArgumentNullException(nameof(surfContext));
            this.ResourceFormatter = resourceFormatter ?? throw new System.ArgumentNullException(nameof(resourceFormatter));
        }

        //

        public IHypertextResourceFormatter ResourceFormatter { get; }

        public SurfContext SurfContext { get; }

        public HttpClient HttpClient { get; }
    }
}