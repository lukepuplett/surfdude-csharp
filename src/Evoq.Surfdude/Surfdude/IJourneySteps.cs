namespace Evoq.Surfdude
{
    using System.Threading.Tasks;

    public interface IJourneySteps
    {
        IJourneySteps Visit(string relation);

        IJourneySteps VisitItem(int index);

        IJourneySteps Send(string relation, object transferObject);

        IJourneySteps Receive<TTransferModel>(out TTransferModel transferObject) where TTransferModel : class;

        Task<JourneyReport> RunAsync(System.Threading.CancellationToken cancellationToken = default);
    }
}