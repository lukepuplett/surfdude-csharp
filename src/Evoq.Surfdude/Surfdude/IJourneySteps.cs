namespace Evoq.Surfdude
{
    using System.Threading.Tasks;

    public interface IJourneySteps
    {
        IJourneySteps Visit(string relation);

        IJourneySteps VisitItem(int index);

        IJourneySteps Send(string relation, object form);

        IJourneySteps CopyInto<TModel>(out TModel model) where TModel : class;

        Task<JourneyReport> RunAsync(System.Threading.CancellationToken cancellationToken = default);
    }
}