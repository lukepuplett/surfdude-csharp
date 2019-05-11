namespace Evoq.Surfdude
{
    public interface IWaveStart
    {
        IWaveSteps FromRoot();

        IWaveSteps From(string uri);
    }
}