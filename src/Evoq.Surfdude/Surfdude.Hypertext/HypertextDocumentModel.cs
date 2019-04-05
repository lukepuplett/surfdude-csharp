using System.Collections.Generic;

namespace Evoq.Surfdude.Hypertext
{
    internal class HypertextDocumentModel
    {
        public IEnumerable<Form> Links { get; set; } = new Form[] { };
    }
}