namespace Evoq.Surfdude
{
    using System.Threading.Tasks;

    public interface ISurfSteps
    {
        ISurfSteps To(string relation);

        ISurfSteps ToItem(int index);

        ISurfSteps Submit(string relation, object transferObject);

        ISurfSteps Read<TModel>(TModel[] models) where TModel : class;

        Task<SurfReport> RideItAsync(System.Threading.CancellationToken cancellationToken = default);
    }
}