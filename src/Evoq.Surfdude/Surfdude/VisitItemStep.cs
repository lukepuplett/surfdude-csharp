using Evoq.Surfdude.Hypertext;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class VisitItemStep : HttpStep
    {
        public VisitItemStep(int index, HttpClient httpClient, JourneyContext journeyContext, IHypertextResourceFormatter resourceFormatter)
            : base(httpClient, journeyContext, resourceFormatter)
        {
            this.Index = index;
        }

        //

        public int Index { get; }

        //

        internal override Task<HttpResponseMessage> InvokeRequestAsync(HttpStep previous)
        {
            var itemControl = previous.Resource.GetItem(this.Index).GetControl("item");

            var firstRequiredInput = itemControl.Inputs?.FirstOrDefault(i => !i.IsOptional);
            if (firstRequiredInput == null)
            {
                return this.InvokeHttpMethodAsync(itemControl.HRef, itemControl.ControlData);
            }
            else
            {
                throw new UnexpectedInputsException(
                    $"Unable to invoke the HTTP request to get the item. The item's hypertext control" +
                    $" requires a value for '{firstRequiredInput.Name}'.");
            }
        }
    }
}