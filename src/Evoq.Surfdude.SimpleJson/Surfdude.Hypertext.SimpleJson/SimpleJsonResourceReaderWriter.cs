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
        public SimpleJsonResourceReaderWriter(RideContext context)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        //

        public RideContext JourneyContext { get; }

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
                this.CheckInputs(sendPairs, hypertextControl);
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
                    if (sendPairs.TryGetValue(input.Name, out string sendValue))
                    {
                        explicitlyDefinedPairs.Add(input.Name, sendValue);
                    }
                    else if (!input.IsOptional)
                    {
                        throw new InvalidOperationException(
                            "Cannot prepare the HTTP content. A missing input was unexpected. Ensure " +
                            "all required fields are present before calling this method.");
                    }
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

        private void CheckInputs(IDictionary<string, string> sendPairs, IHypertextControl hypertextControl)
        {
            string[] overlapping = this.GetOverlapping(sendPairs.Keys, hypertextControl.Inputs);
            if (overlapping.Length == 0)
            {
                string[] allInputs = hypertextControl.Inputs.Select(i => i.Name).ToArray();

                throw CreateMissingInputException(hypertextControl, sendPairs.Keys, allInputs);
            }

            //

            string[] missingRequired = this.GetMissingRequired(sendPairs.Keys, hypertextControl.Inputs);
            if (missingRequired.Length > 0)
            {
                throw CreateMissingInputException(hypertextControl, sendPairs.Keys, missingRequired);
            }
        }

        private static MissingInputException CreateMissingInputException(IHypertextControl hypertextControl, IEnumerable<string> sendKeys, string[] missing)
        {
            string message;

            if (sendKeys.Any())
            {
                message = $"Unable to prepare the representation to send. The control associated with " +
                    $"relation '{hypertextControl.Rel}' defines the input(s) '{String.Join(", ", missing)}'" +
                    $" not found in the data being sent, '{String.Join(", ", sendKeys)}'.";
            }
            else
            {
                message = $"Unable to prepare the representation to send. The control associated with " +
                    $"relation '{hypertextControl.Rel}' defines the input(s) '{String.Join(", ", missing)}'" +
                    $" but there is no data being sent.";
            }

            var missingInputException = new MissingInputException(message);

            foreach (string m in missing)
            {
                missingInputException.Data.Add(m, "missing");
            }

            return missingInputException;
        }

        private string[] GetOverlapping(IEnumerable<string> sendKeys, IEnumerable<IHypertextInputControl> inputs)
        {
            return inputs
                .Select(i => i.Name)
                .Intersect(sendKeys, StringComparer.OrdinalIgnoreCase)
                .ToArray();
        }

        private string[] GetMissingRequired(IEnumerable<string> sendKeys, IEnumerable<IHypertextInputControl> inputs)
        {
            return inputs.GetRequiredHypertextControls()
                .Select(i => i.Name)
                .Except(sendKeys, StringComparer.OrdinalIgnoreCase)
                .ToArray();
        }
    }
}
