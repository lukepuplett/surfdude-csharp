namespace Evoq.Surfdude
{
    using System.Threading.Tasks;

    public interface IJourneySteps
    {
        IJourneySteps FollowLink(string relation);

        IJourneySteps OpenItem(int index);

        IJourneySteps Submit(string relation, object form);

        Task<JourneyReport> RunAsync(System.Threading.CancellationToken cancellationToken = default);
    }
}