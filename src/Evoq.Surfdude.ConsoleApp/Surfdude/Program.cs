namespace Evoq.Surfdude
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            var report = await Journey.Start(args?.FirstOrDefault() ?? "https://private-0dcfd-usermanagementbackend.apiary-mock.com")
                .FromRoot()
                .FollowLink("quotes")
                .FollowLink("next")
                .OpenItem(0)
                .Submit("form", new { phrase = "beans" })
                .Read<Model>(out Model model)
                .RunAsync();

            foreach(var line in report.Lines)
            {
                Console.WriteLine(line.Message);
            }

            report.EnsureSuccess();
        }
    }

    internal class Model
    {
    }
}
