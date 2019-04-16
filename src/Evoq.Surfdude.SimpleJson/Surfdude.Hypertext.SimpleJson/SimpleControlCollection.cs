namespace Evoq.Surfdude.Hypertext.SimpleJson
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    internal class SimpleControlCollection : Collection<SimpleHypertextControl>, IHypertextControls
    {
        public SimpleControlCollection()
            : base()
        {
        }

        //

        public IHypertextControl GetControl(string rel)
        {
            if (rel == null)
            {
                throw new ArgumentNullException(nameof(rel));
            }

            if (this.Count == 0)
            {
                throw new RelationNotFoundException($"Could not find a hyperlink with relation '{rel}'. There are no hyperlinks on the resource.");
            }
            else
            {
                IHypertextControl control = this.FirstOrDefault(c => rel.Equals(c.Rel, StringComparison.OrdinalIgnoreCase));

                return control ??
                    throw new RelationNotFoundException(
                        $"Could not find a hyperlink with relation '{rel}'. The available relations are '{string.Join(", ", GetRelations())}'");
            }
        }

        public IEnumerable<string> GetRelations()
        {
            return this.Select(control => control.Rel);
        }
    }
}