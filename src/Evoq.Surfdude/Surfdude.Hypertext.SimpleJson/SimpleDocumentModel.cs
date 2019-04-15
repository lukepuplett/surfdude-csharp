namespace Evoq.Surfdude.Hypertext.SimpleJson
{
    using System.Collections.Generic;
    using System.Linq;

    internal class SimpleDocumentModel : IHypertextResource
    {
        public IEnumerable<SimpleDocumentModel> Items { get; set; } = new SimpleDocumentModel[] { };

        public SimpleControlCollection Links { get; set; } = new SimpleControlCollection();

        //

        public IHypertextControl GetControl(string rel)
        {
            return this.Links.GetControl(rel);
        }

        public IHypertextControls GetItem(int index)
        {
            return this.Items.ToArray()[index];
        }
    }
}