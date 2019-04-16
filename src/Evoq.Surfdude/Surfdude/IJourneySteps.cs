namespace Evoq.Surfdude
{
    using System.Threading.Tasks;

    public interface IJourneySteps
    {
        IJourneySteps Request(string relation);

        IJourneySteps RequestItem(int index);

        IJourneySteps Submit(string relation, object transferObject);

        IJourneySteps Read<TTransferModel>(out TTransferModel transferObject) where TTransferModel : class;

        Task<JourneyReport> RunAsync(System.Threading.CancellationToken cancellationToken = default);
    }
}