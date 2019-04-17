namespace Evoq.Surfdude
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            var report = await Surf.Wave(args?.FirstOrDefault() ?? "https://private-ac89c-surfdude.apiary-mock.com/")
                .FromRoot()
                .To("registrations")
                .ToItem(0)
                .Submit("update-contact-details", new { email = "mike@beans.com" })
                .Read(out Func<ResourceModel> getContactDetails)
                .To("registration")
                .Submit("add-processing-instruction", new { message = "Put NO-SPAM in the email subject." })
                .GoAsync();

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
