namespace Evoq.Surfdude.Hypertext
{
    using System.Collections.Generic;
    using System.Linq;

    public static class HypertextHttpExtensions
    {
        public static IEnumerable<IHypertextInputControl> GetRequiredHypertextControls(this IEnumerable<IHypertextInputControl> inputs)
        {
            return inputs.Where(i => !i.IsOptional).ToArray();
        }
    }
}
