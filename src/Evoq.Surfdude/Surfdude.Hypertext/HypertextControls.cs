using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Evoq.Surfdude.Hypertext
{
    internal class HypertextControls : Collection<Form>, IHypertextControls
    {
        public HypertextControls()
            : base()
        {
        }

        public HypertextControl GetControl(string rel)
        {
            HypertextControl control = this.FirstOrDefault(c => rel.Equals(c.Rel, StringComparison.OrdinalIgnoreCase));

            return control ??
                throw new RelationNotFoundException(
                    $"Could not find a hyperlink with relation '{rel}'. The available relations are '{string.Join(", ", GetRelations())}'");
        }

        public IEnumerable<string> GetRelations()
        {
            return this.Select(control => control.Rel);
        }
    }
}