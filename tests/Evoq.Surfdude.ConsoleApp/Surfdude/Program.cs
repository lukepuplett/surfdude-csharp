namespace Evoq.Surfdude
{
    using Evoq.Instrumentation;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    class Program
    {
        static readonly InMemoryLoggerFactory loggerFactory = new InMemoryLoggerFactory();        

        static async Task Main(string[] args)
        {
            //var report = await Surf.Wave(args?.FirstOrDefault() ?? "https://private-ac89c-surfdude.apiary-mock.com/")
            //       .FromRoot()
            //       .ThenSubmit("registration-finder", new { status = "active", email = "mike@beans.com" })
            //       .GoAsync();

            var context = new SurfContext(args?.FirstOrDefault() ?? "https://private-ac89c-surfdude.apiary-mock.com/", loggerFactory);

            var report = await Surf.Wave(context)
                .FromRoot()
                .Then("registrations")
                .ThenItem(0)
                .ThenSubmit("update-contact-details", new { email = "mike@beans.com" })
                .ThenRead(out Func<ResourceModel> getContactDetails)
                .Then("registration")
                //.ThenSubmit("add-processing-instruction", new { message = "Put NO-SPAM in the email subject." })
                .GoAsync();

            Console.WriteLine(String.Join(Environment.NewLine, loggerFactory.Messages));

            Console.WriteLine(getContactDetails()?.Email);

            foreach (var line in report.Lines)
            {
                Console.WriteLine(line.Message);
            }

            report.EnsureSuccess();
        }
    }

    internal class ResourceModel
    {
        public string Email { get; set; }
    }
}
