namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public abstract class HttpStep : IStep
    {
        internal static readonly string MethodControlName = "method";
        internal static readonly string IfMatchControlName = "if-match";

        //

        public HttpStep(HttpClient httpClient, JourneyContext journeyContext, IHypertextResourceFormatter resourceFormatter)
        {
            this.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.JourneyContext = journeyContext ?? throw new ArgumentNullException(nameof(journeyContext));
            this.ResourceFormatter = resourceFormatter ?? throw new ArgumentNullException(nameof(resourceFormatter));
        }

        //

        public IHypertextResource Resource { get; private set; }

        public string Name => this.GetType().Name;

        //

        protected HttpResponseMessage Response { get; set; }

        protected IHypertextResourceFormatter ResourceFormatter { get; }

        protected JourneyContext JourneyContext { get; }

        protected HttpClient HttpClient { get; }

        //

        public async Task RunAsync(IStep previous)
        {
            this.Response = await this.InvokeRequestAsync((HttpStep)previous);

            if (!this.JourneyContext.IgnoreBadResults)
            {
                try
                {
                    this.Response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException httpRequestException)
                {
                    throw new StepFailedException("The step failed. See inner exception.", httpRequestException);
                }
            }

            this.Resource = await this.ResourceFormatter.ReadResourceAsync(this.Response.Content);
        }

        internal abstract Task<HttpResponseMessage> InvokeRequestAsync(HttpStep previous);

        protected Task<HttpResponseMessage> InvokeHttpMethodAsync(string url, IEnumerable<KeyValuePair<string, string>> controlData, HttpContent httpContent = null)
        {
            string methodValue = controlData.FirstOrDefault(cd => cd.Key == MethodControlName).Value ?? HttpMethod.Get.Method;
                        
            if (HttpMethod.Get.Method == methodValue)
            {
                return this.HttpClient.GetAsync(url);
            }
            else if (HttpMethod.Put.Method == methodValue)
            {
                return this.HttpClient.PutAsync(url, httpContent);
            }
            else if (HttpMethod.Post.Method == methodValue)
            {
                return this.HttpClient.PostAsync(url, httpContent);
            }
            else if (HttpMethod.Delete.Method == methodValue)
            {
                return this.HttpClient.DeleteAsync(url);
            }
            else
            {
                throw new UnexpectedMethodException(
                    $"Could not determine the HTTP method to use in the request. The method '{methodValue}' in the control data was not recognised or is unsupported.");
            }
        }
    }
}