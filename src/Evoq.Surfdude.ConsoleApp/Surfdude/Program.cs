namespace Evoq.Surfdude
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            var report = await Journey.Start(args?.FirstOrDefault() ?? "https://private-ac89c-surfdude.apiary-mock.com/")
                .FromRoot()
                .Request("registrations")
                .RequestItem(0)
                .Submit("update-contact-details", new { email = "mike@beans.com" })
                .Read(out Func<ResourceModel> getContactDetails)
                .Request("registration")
                .Submit("add-processing-instruction", new { message = "Put NO-SPAM in the email subject." })
                .RunAsync();

            Console.WriteLine(getContactDetails().Email);

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
