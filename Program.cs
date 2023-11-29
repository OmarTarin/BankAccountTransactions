using Microsoft.Extensions.Logging;

namespace BankingEvaluation
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            var service = new Service();
            service.Initialize();

            service.LoggerFactory<Service>()
                .LogInformation(@"Starting Service {Application.ProductName} Version {Application.ProductVersion}");
            
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(service));
        }
    }
}