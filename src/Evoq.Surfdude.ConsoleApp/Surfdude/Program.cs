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
                .Visit("registrations")                
                .VisitItem(0)
                .Send("update-contact-details", new { phrase = "beans" })
                .CopyInto<Model>(out Model model)
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
