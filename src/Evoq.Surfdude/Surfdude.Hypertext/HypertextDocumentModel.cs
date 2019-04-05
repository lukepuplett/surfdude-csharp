using System.Collections.Generic;

namespace Evoq.Surfdude.Hypertext
{
    internal class HypertextDocumentModel
    {
        public IEnumerable<HypertextDocumentModel> Items { get; set; } = new HypertextDocumentModel[] { };

        public HypertextControls Links { get; set; } = new HypertextControls();
    }
}