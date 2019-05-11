namespace Evoq.Surfdude
{
    using System.Threading.Tasks;

    public interface IWaveSteps
    {
        IWaveSteps Then(string relation);

        IWaveSteps ThenItem(int index);

        IWaveSteps ThenSubmit(string relation, object transferObject);

        IWaveSteps ThenRead<TModel>(TModel[] models) where TModel : class;

        Task<SurfReport> GoAsync(System.Threading.CancellationToken cancellationToken = default);
    }
}