namespace Evoq.Surfdude
{
    using System;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            var report = await Journey.Start(new Options())
                .FromRoot()
                .FollowLink("quotes")
                .FollowLink("next")
                .OpenItem(0)
                .Submit("form", new { phrase = "beans" })
                .RunAsync();
        }
    }
}
