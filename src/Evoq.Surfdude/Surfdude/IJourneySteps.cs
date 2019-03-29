namespace Evoq.Surfdude
{
    using System.Threading.Tasks;

    public interface IJourneySteps
    {
        IJourneySteps FollowLink(string relation);

        IJourneySteps OpenItem(int index);

        IJourneySteps Submit(string relation, object form);

        IJourneySteps Read<TModel>(out TModel model) where TModel : class;

        Task<JourneyReport> RunAsync(System.Threading.CancellationToken cancellationToken = default);
    }
}