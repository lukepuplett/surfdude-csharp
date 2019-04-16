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
            var report = await Journey.Start(args?.FirstOrDefault() ?? "https://private-ac89c-surfdude.apiary-mock.com/")
                .FromRoot()
                .Request("registrations")                
                .RequestItem(0)
                .Submit("update-contact-details", new { phrase = "beans" })
                .Read(out ResourceModel model)
                .RunAsync();

            foreach(var line in report.Lines)
            {
                Console.WriteLine(line.Message);
            }

            report.EnsureSuccess();
        }
    }

    internal class ResourceModel
    {
    }
}
