namespace Evoq.Surfdude.Hypertext.SimpleJson
{
    using Evoq.Surfdude.Hypertext.Http;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    internal class SimpleJsonResourceReaderWriter : IHypertextResourceFormatter
    {
        public SimpleJsonResourceReaderWriter(JourneyContext context)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        //

        public JourneyContext JourneyContext { get; }

        public string DefaultMediaType { get; } = "application/simple-hypertext+json";

        public string DefaultCharSet { get; } = "utf-8";

        public Encoding DefaultEncoding { get; } = Encoding.UTF8;

        //

        public Task<TModel> ReadAsModelAsync<TModel>(HttpResponseMessage httpResponse)
        {
            return this.Deserialize<TModel>(httpResponse);
        }

        public async Task<IHypertextResource> ReadAsResourceAsync(HttpResponseMessage httpResponse)
        {
            if (httpResponse == null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            var documentModel = await this.Deserialize<SimpleDocumentModel>(httpResponse);

            return new SimpleHypertextResource(documentModel);
        }

        public HttpRequestMessage BuildRequest(IDictionary<string, string> sendPairs, IHypertextControl hypertextControl)
        {
            if (sendPairs == null)
            {
                throw new ArgumentNullException(nameof(sendPairs));
            }

            if (hypertextControl == null)
            {
                throw new ArgumentNullException(nameof(hypertextControl));
            }

            if (hypertextControl.Inputs != null)
            {
                this.ThrowOnMissingInputs(sendPairs, hypertextControl);
            }

            HttpRequestMessage httpRequest;
            if (hypertextControl.IsMutation())
            {
                httpRequest = new HttpRequestMessage(hypertextControl.GetHttpMethod(), hypertextControl.HRef)
                {
                    Content = this.PrepareHttpContent(sendPairs, hypertextControl)
                };
            }
            else
            {
                httpRequest = new HttpRequestMessage(hypertextControl.GetHttpMethod(), this.PrepareUri(sendPairs, hypertextControl));                
            }
            
            httpRequest.Headers.Accept.ParseAdd(this.DefaultMediaType);
            httpRequest.Headers.AcceptCharset.ParseAdd(this.DefaultCharSet);

            return httpRequest;
        }

        //

        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse)
        {
            var httpContent = httpResponse.Content;
            byte[] contentBytes = await httpContent.ReadAsByteArrayAsync();

            var encodingResolver = new EncodingResolver();
            string responseMediaType = httpContent.Headers.ContentType?.MediaType ?? this.DefaultMediaType;
            var encoding = encodingResolver.ResolveEncoding(responseMediaType, this.DefaultEncoding);

            var textReader = new StreamReader(new MemoryStream(contentBytes), encoding);

            var jsonTextReader = new Newtonsoft.Json.JsonTextReader(textReader);
            var serializer = new Newtonsoft.Json.JsonSerializer();

            return serializer.Deserialize<T>(jsonTextReader);
        }

        private string PrepareUri(IDictionary<string, string> sendPairs, IHypertextControl hypertextControl)
        {
            throw new NotImplementedException(nameof(PrepareUri));
        }

        private HttpContent PrepareHttpContent(IDictionary<string, string> sendPairs, IHypertextControl hypertextControl)
        {
            string jsonBodyString;

            if (hypertextControl.Inputs != null)
            {
                var explicitlyDefinedPairs = new Dictionary<string, string>();

                foreach (var input in hypertextControl.Inputs)
                {
                    explicitlyDefinedPairs.Add(input.Name, sendPairs[input.Name]);
                }

                jsonBodyString = Newtonsoft.Json.JsonConvert.SerializeObject(explicitlyDefinedPairs);
            }
            else
            {
                jsonBodyString = Newtonsoft.Json.JsonConvert.SerializeObject(sendPairs);
            }

            var httpContent = new StringContent(jsonBodyString);

            httpContent.Headers.ContentType.MediaType = this.DefaultMediaType;
            httpContent.Headers.ContentType.CharSet = this.DefaultCharSet;

            return httpContent;
        }

        private void ThrowOnMissingInputs(IDictionary<string, string> sendPairs, IHypertextControl hypertextControl)
        {
            var inputControls = hypertextControl.Inputs;

            string[] missingRequired = this.GetMissingRequired(sendPairs.Keys, inputControls);
            if (missingRequired.Length > 0)
            {
                var missingInputException = new MissingInputException(
                    $"Unable to prepare the representation to send. The control associated with " +
                    $"relation '{hypertextControl.Rel}' requires the following inputs '{String.Join(", ", missingRequired)}'.");

                foreach (string m in missingRequired)
                {
                    missingInputException.Data.Add(m, "missing");
                }

                throw missingInputException;
            }
        }

        private string[] GetMissingRequired(IEnumerable<string> sendKeys, IEnumerable<IHypertextInputControl> inputs)
        {
            return inputs.GetRequiredHypertextControls()
                .Select(i => i.Name)
                .Except(sendKeys)
                .ToArray();
        }        
    }
}
