using System;
using LottoDriver.CustomersApi.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

using LottoDriver.Examples.CustomersApi.Common.DataAccess;

namespace LottoDriver.Examples.CustomersApi.WorkerService
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (!CreateLogger()) return 1;
            
            try
            {
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Host terminated unexpectedly!");
                return 2;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;

                    services.AddTransient<ICustomersApiClient>(serviceProvider =>
                    {
                        var logger = serviceProvider.GetRequiredService<ILogger<CustomersApiClient>>();

                        var client = new CustomersApiClient(
                            configuration.GetValue<string>("AppSettings:LottoDriverApiUrl"),
                            configuration.GetValue<string>("AppSettings:LottoDriverClientId"),
                            configuration.GetValue<string>("AppSettings:LottoDriverSecret")
                        );

                        client.Error += (source, exception) =>
                            logger.LogError(exception, "Error in lotto driver api client");

                        client.CallbackError += (source, exception) =>
                            logger.LogError(exception, "Error in lotto driver api client event handlers");

                        return client;
                    });

                    services.AddTransient<IDatabase>(_ => new SQLiteDatabase(configuration.GetValue<string>("AppSettings:DatabasePath")));

                    services.AddHostedService<Worker>();
                });
        }

        private static bool CreateLogger()
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                return true;
            }
            catch (Exception ex)
            {
                if (Environment.UserInteractive)
                {
                    Console.WriteLine("Logger creation failed!");
                    Console.WriteLine(ex.Message);
                }

                return false;
            }
        }
    }


}
