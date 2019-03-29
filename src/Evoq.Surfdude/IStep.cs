using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal interface IStep
    {
        Task RunAsync(IStep previous);
    }
}