using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    public interface IStep // Factor into IStepAction and IStepResult
    {
        string Name { get; }

        object Result { get; }

        Task<object> RunAsync(IStep previous);
    }
}