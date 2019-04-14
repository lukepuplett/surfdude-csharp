namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using System.Threading.Tasks;

    public interface IStep // Factor into IStepAction and IStepResult
    {
        string Name { get; }

        IHypertextResource Resource { get; }

        Task RunAsync(IStep previous);
    }
}