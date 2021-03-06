﻿namespace Evoq.Surfdude.Hypertext.Http
{
    using Evoq.Surfdude.Hypertext;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ToItemStep : HttpStep
    {
        private const string ItemRelation = "item";

        //

        public ToItemStep(HttpStepContext stepContext, int index)
            : base(stepContext)
        {
            this.Index = index;
        }

        //

        public int Index { get; }

        //

        internal override Task<HttpResponseMessage> ExecuteStepRequestAsync(HttpStep previous, CancellationToken cancellationToken)
        {
            var itemControl = previous.Resource.GetItem(this.Index).GetControl(ItemRelation);

            if (itemControl.SupportsRequestBody())
            {
                throw new UnexpectedInputsException(
                    $"Unable to invoke the HTTP request to get the item. The item's '{ItemRelation}' hypertext control" +
                    $" is a mutating method.");
            }
            else
            {
                var firstRequiredInput = itemControl.Inputs?.FirstOrDefault(i => !i.IsOptional);
                if (firstRequiredInput == null)
                {
                    return this.StepContext.HttpClient.GetAsync(itemControl.HRef, cancellationToken);
                }
                else
                {
                    throw new UnexpectedInputsException(
                        $"Unable to invoke the HTTP request to get the item. The item's '{ItemRelation}' hypertext control" +
                        $" requires a value for '{firstRequiredInput.Name}'.");
                }
            }
        }
    }
}