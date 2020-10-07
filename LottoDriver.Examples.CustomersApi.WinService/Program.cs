using System;
using System.ServiceProcess;

namespace LottoDriver.Examples.CustomersApi.WinService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var svc = new Service1();

            if (Environment.UserInteractive)
            {
                Console.WriteLine("Starting example customers api client service");
                svc.ConsoleStart(args);
                Console.WriteLine("Started! Press Enter to stop.");

                Console.ReadLine();

                Console.WriteLine("Stopping example customers api client service");
                svc.Stop();
                Console.WriteLine("Stopped.");
            }
            else
            {
                ServiceBase.Run(svc);
            }
        }
    }
}
