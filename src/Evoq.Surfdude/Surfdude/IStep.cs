using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    public interface IStep
    {
        Task RunAsync(IStep previous);
    }
}