using Evoq.Surfdude.Hypertext;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    public interface IStep // Factor into IStepAction and IStepResult
    {
        string Name { get; }

        HypertextResource Resource { get; }

        Task<HypertextResource> RunAsync(IStep previous);
    }
}