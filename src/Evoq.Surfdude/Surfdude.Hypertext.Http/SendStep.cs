namespace Evoq.Surfdude.Hypertext.Http
{
    using Evoq.Surfdude.Hypertext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class SendStep : HttpStep
    {
        public SendStep(string rel, object sendModel, HttpClient httpClient, JourneyContext journeyContext, IHypertextResourceFormatter resourceFormatter)
            : base(httpClient, journeyContext, resourceFormatter)
        {
            this.Rel = rel ?? throw new ArgumentNullException(nameof(rel));
            this.SendModel = sendModel ?? throw new ArgumentNullException(nameof(sendModel));
        }

        //

        public string Rel { get; }

        public object SendModel { get; }

        //

        internal override Task<HttpResponseMessage> ExecuteStepRequestAsync(HttpStep previous)
        {
            var senderControl = previous.Resource.GetControl(this.Rel);
            var previousResponseMediaType = previous.Response.Content.Headers.ContentType?.MediaType ?? "application/json; charset=utf-8";
            var urlAndContent = this.PrepareUrlAndContent(new SendDictionary(this.SendModel), senderControl, previousResponseMediaType); // Refactor into this.ResourceFormatter.Write

            throw new NotImplementedException(nameof(SendStep));
        }

        private (string, HttpContent) PrepareUrlAndContent(SendDictionary sendBag, IHypertextControl hypertextControl, string mediaType)
        {
            if (sendBag == null)
            {
                throw new ArgumentNullException(nameof(sendBag));
            }

            if (hypertextControl == null)
            {
                throw new ArgumentNullException(nameof(hypertextControl));
            }

            if (mediaType == null)
            {
                throw new ArgumentNullException(nameof(mediaType));
            }

            if (hypertextControl.Inputs != null)
            {
                ThrowOnMissingControls(sendBag, hypertextControl.Inputs);
            }

            if (IsBodyRequired(hypertextControl))
            {
                if (hypertextControl.Inputs != null)
                {
                    var bodyBag = new SendDictionary();

                    foreach (var input in hypertextControl.Inputs)
                    {
                        bodyBag.Add(input.Name, sendBag[input.Name]);
                    }

                    // Serialize and put in 

                    var httpContent = new StringContent(null);
                }
                else
                {

                }
            }
            else
            {

            }

            // Prepare request.

            throw new NotImplementedException(nameof(PrepareUrlAndContent));
        }

        private void ThrowOnMissingControls(SendDictionary sendBag, IEnumerable<IHypertextInputControl> inputControls)
        {
            // This code has knowledge of our JSON processing instructions, i.e. our media-type. It needs abstracting from the step.

            var missingRequired = this.GetMissingRequired(sendBag.Keys, inputControls);
            if (missingRequired.Length > 0)
            {
                var missingInput = new MissingInputException(
                    $"Unable to prepare the representation to send. The control associated with " +
                    $"relation '{this.Rel}' requires the following inputs '{String.Join(", ", missingRequired)}'.");

                foreach (var m in missingRequired)
                {
                    missingInput.Data.Add(m, "missing");
                }

                throw missingInput;
            }
        }

        private static bool IsBodyRequired(IHypertextControl hypertextControl)
        {
            var methodName = hypertextControl.ControlData?.FirstOrDefault(cd => cd.Key == HttpControlData.MethodControlName).Value;

            if (methodName == null)
            {
                return false;
            }
            else if (methodName.Equals("get", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else if (methodName.Equals("delete", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                return true;
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