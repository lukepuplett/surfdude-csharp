namespace Evoq.Surfdude.Hypertext
{
    public abstract class HypertextControl: IHyperlink
    {
        public string HRef { get; set; }

        public string Rel { get; set; }

        public string Method { get; set; }

        public abstract bool RequiresInput();
    }
}