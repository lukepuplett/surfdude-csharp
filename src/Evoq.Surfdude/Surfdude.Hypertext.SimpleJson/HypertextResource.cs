namespace Evoq.Surfdude.Hypertext.SimpleJson
{
    using Evoq.Surfdude.Hypertext.SimpleJson;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal class SimpleHypertextResource : IHypertextResource
    {
        public SimpleHypertextResource(SimpleDocumentModel documentModel)
        {
            this.DocumentModel = documentModel ?? throw new ArgumentNullException(nameof(documentModel));
        }

        public SimpleDocumentModel DocumentModel { get; }

        //

        public IHypertextControl GetControl(string rel)
        {
            return this.DocumentModel.GetControl(rel);
        }

        public IHypertextControls GetItem(int index)
        {
            return this.DocumentModel.GetItem(index);
        }
    }
}
