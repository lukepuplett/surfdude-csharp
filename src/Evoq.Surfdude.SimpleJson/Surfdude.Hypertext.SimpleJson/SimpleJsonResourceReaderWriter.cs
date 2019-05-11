namespace Evoq.Surfdude.Hypertext.SimpleJson
{
    using Evoq.Surfdude.Hypertext.Http;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    internal class SimpleJsonResourceReaderWriter : IHypertextResourceFormatter
    {
        private readonly ILogger logger;

        //

        public SimpleJsonResourceReaderWriter(SurfContext context)
        {
            this.JourneyContext = context ?? throw new ArgumentNullException(nameof(context));

            logger = this.JourneyContext.LoggerFactory?.CreateLogger<IHypertextResourceFormatter>();
        }

        //

        public SurfContext JourneyContext { get; }

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

            var httpRequest = this.CreateRequestWithOptionalBody(sendPairs, hypertextControl);

            this.AppendHeaders(hypertextControl, httpRequest);

            return httpRequest;
        }

        //

        private void AppendHeaders(IHypertextControl hypertextControl, HttpRequestMessage httpRequest)
        {
            if (this.DefaultMediaType != null)
            {
                logger?.LogDebug($"Append header Accept: {this.DefaultMediaType}");

                httpRequest.Headers.Accept.ParseAdd(this.DefaultMediaType);
            }

            if (this.DefaultCharSet != null)
            {
                logger?.LogDebug($"Append header Accept: {this.DefaultCharSet}");

                httpRequest.Headers.AcceptCharset.ParseAdd(this.DefaultCharSet);
            }

            if (hypertextControl.TryParseIfMatch(out string ifMatch))
            {
                logger?.LogDebug($"Append header Accept: {ifMatch}");

                httpRequest.Headers.IfMatch.ParseAdd(ifMatch);
            }
        }

        private HttpRequestMessage CreateRequestWithOptionalBody(IDictionary<string, string> sendPairs, IHypertextControl hypertextControl)
        {
            logger?.LogDebug("Creating HTTP request.");

            HttpRequestMessage httpRequest;
            if (hypertextControl.SupportsRequestBody())
            {
                httpRequest = new HttpRequestMessage(hypertextControl.DetermineHttpMethod(), hypertextControl.HRef)
                {
                    Content = this.PrepareHttpContent(sendPairs, hypertextControl)
                };
            }
            else
            {
                httpRequest = new HttpRequestMessage(hypertextControl.DetermineHttpMethod(), this.PrepareUri(sendPairs, hypertextControl));
            }

            return httpRequest;
        }

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
            var pairs = new Dictionary<string, string>(sendPairs);

            var uriTemplate = new UriTemplate.Core.UriTemplate(hypertextControl.HRef);
            var uri = uriTemplate.BindByName(pairs);
            var bindings = uriTemplate.Match(uri, pairs.Keys).Bindings;

            foreach (string bound in bindings.Keys)
            {
                pairs.Remove(bound);
            }

            if (pairs.Count > 0)
            {
                string queryString = String.Join("&", pairs.Select(v => $"{v.Key}={v.Value}"));
                string questionMark = uri.OriginalString.Contains("?") ? String.Empty : "?";

                return Flurl.Url.Combine(uri.OriginalString, questionMark, queryString);
            }
            else
            {
                return uri.OriginalString;
            }
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

            logger?.LogDebug($"HTTP request body has {jsonBodyString.Length} chars.");

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
                message =
                    $"Unable to prepare a request for the relation '{hypertextControl.Rel}'. These inputs were " +
                    $"supplied, '{String.Join(", ", sendKeys)}'. One or more of these inputs were missing, '{String.Join(", ", missing)}'.";
            }
            else
            {
                message =
                    $"Unable to prepare a request for the relation '{hypertextControl.Rel}'. No inputs were " +
                    $"supplied. The control has these inputs, '{String.Join(", ", missing)}'.";
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
