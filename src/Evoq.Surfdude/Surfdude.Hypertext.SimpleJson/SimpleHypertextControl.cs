namespace Evoq.Surfdude.Hypertext.SimpleJson
{
    using System.Collections.Generic;
    using System.Linq;
    
    internal class SimpleHypertextControl : IHypertextControl
    {
        public string HRef { get; set; }

        public string Rel { get; set; }

        public string Method { get; set; }

        public string IfMatch { get; set; }

        public IEnumerable<InputControl> Inputs { get; }

        public IEnumerable<KeyValuePair<string, string>> ControlData => new []
        {
            new KeyValuePair<string, string>(HttpRequestStep.MethodControlName, this.Method),
            new KeyValuePair<string, string>(HttpRequestStep.IfMatchControlName, this.IfMatch)
        };

        public bool RequiresInput => this.Inputs?.Any(i => !i.IsOptional) ?? false;
    }
}