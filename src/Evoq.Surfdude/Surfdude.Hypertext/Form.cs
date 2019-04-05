using System.Collections.Generic;
using System.Linq;

namespace Evoq.Surfdude.Hypertext
{
    internal class Form : HypertextControl
    {
        public IEnumerable<Input> Inputs { get; }

        public IDictionary<string, ControlData> ControlData { get; }

        public override bool RequiresInput()
        {
            return this.Inputs != null && this.Inputs.Any();
        }
    }
}