using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Evoq.Surfdude.Hypertext;

namespace Evoq.Surfdude
{
    public class SendFormatter
    {
        public SendFormatter(IHypertextResourceFormatter resourceFormatter)
        {
            this.ResourceFormatter = resourceFormatter ?? throw new ArgumentNullException(nameof(resourceFormatter));
        }

        //

        public IHypertextResourceFormatter ResourceFormatter { get; }

        //

        public (string, HttpContent) Make(object sendObject, IHypertextControl hypertextControl)
        {
            if (sendObject == null)
            {
                throw new ArgumentNullException(nameof(sendObject));
            }

            if (hypertextControl == null)
            {
                throw new ArgumentNullException(nameof(hypertextControl));
            }


            var formPropertyNames = this.GetFormPropertyNames(sendObject);

            if (hypertextControl.Inputs != null)
            {
                object[] missingRequired = this.GetMissingRequired(formPropertyNames, hypertextControl);

                if (missingRequired.Length > 0)
                {
                    var missingInput = new MissingInputException(
                        $"Unable to prepare the representation to send. The control requires the following " +
                        $"missing inputs '{String.Join(", ", missingRequired)}'.");
                }

                // Which go in body and which go in URL?

                
            }

            // Prepare request.

            throw new NotImplementedException(nameof(Make));
        }

        private object[] GetMissingRequired(string[] formPropertyNames, IHypertextControl hypertextControl)
        {
            return hypertextControl.Inputs
                .Where(i => !i.IsOptional)
                .Select(i => i.Name)
                .Except(formPropertyNames)
                .ToArray();
        }

        private string[] GetFormPropertyNames(object sendObject)
        {
            return sendObject.GetType()
                .GetProperties()
                .Select(p => p.Name)
                .ToArray();
        }
    }
}