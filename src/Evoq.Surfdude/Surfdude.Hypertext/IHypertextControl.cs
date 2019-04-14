namespace Evoq.Surfdude.Hypertext
{
    using System.Collections.Generic;

    public interface IHypertextControl : IHyperlink
    {
        string Method { get; set; }

        IEnumerable<Input> Inputs { get; }

        IDictionary<string, RequestData> RequestData { get; }
    }
}