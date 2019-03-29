using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    public interface IStep
    {
        object Result { get; }

        Task<object> RunAsync(IStep previous);
    }
}