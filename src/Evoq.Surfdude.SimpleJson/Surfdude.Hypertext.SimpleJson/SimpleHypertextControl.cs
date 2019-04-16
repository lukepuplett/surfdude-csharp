namespace Evoq.Surfdude.Hypertext.SimpleJson
{
    using Evoq.Surfdude.Hypertext.Http;
    using System.Collections.Generic;
    using System.Linq;
    
    internal class SimpleHypertextControl : IHypertextControl
    {
        public string HRef { get; set; }

        public string Rel { get; set; }

        public string Method { get; set; }

        public string IfMatch { get; set; }

        public IEnumerable<SimpleInputControl> Form { get; set; }

        public IEnumerable<IHypertextInputControl> Inputs => this.Form;

        public IEnumerable<KeyValuePair<string, string>> ControlData => new []
        {
            new KeyValuePair<string, string>(HttpControlData.MethodControlName, this.Method),
            new KeyValuePair<string, string>(HttpControlData.IfMatchControlName, this.IfMatch)
        };

        public bool RequiresInput => this.Inputs?.Any(i => !i.IsOptional) ?? false;
    }
}