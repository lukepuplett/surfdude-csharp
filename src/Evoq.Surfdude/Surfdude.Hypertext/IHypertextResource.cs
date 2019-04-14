namespace Evoq.Surfdude.Hypertext
{
    public interface IHypertextResource : IHypertextControls
    {
        IHypertextControls GetItem(int index);
    }
}