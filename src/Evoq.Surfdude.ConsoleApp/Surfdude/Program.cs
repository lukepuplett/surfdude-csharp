namespace Evoq.Surfdude
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            var report = await Journey.Start(new JourneyContext())
                .FromRoot()
                .FollowLink("quotes")
                .FollowLink("next")
                .OpenItem(0)
                .Submit("form", new { phrase = "beans" })
                .Read<Model>(out Model model)
                .RunAsync();
        }
    }

    internal class Model
    {
    }
}
