namespace Evoq.Surfdude.Hypertext
{
    using System.Collections.Generic;

    public interface IHypertextControl : IHyperlink
    {
        IEnumerable<InputControl> Inputs { get; }

        IEnumerable<KeyValuePair<string, string>> ControlData { get; }
    }
}