namespace Evoq.Surfdude.Hypertext.SimpleJson
{
    using System.Collections.Generic;
    using System.Linq;
    
    internal class SimpleHypertextControl : IHypertextControl
    {
        public string HRef { get; set; }

        public string Rel { get; set; }

        public string Method { get; set; }

        public IEnumerable<Input> Inputs { get; }

        public IDictionary<string, RequestData> RequestData { get; }

        public bool RequiresInput => this.Inputs?.Any(i => !i.IsOptional) ?? false;
    }
}