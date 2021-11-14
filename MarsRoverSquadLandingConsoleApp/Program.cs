using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Diagnostics;

namespace MarsRoverSquadLandingConsoleApp
{
    class Program
    {
        static Task Main(string[] args)
        {
            try
            {
                Log.Information("Starting Host");

                var host = CreateHostBuilder(args).Build();
                host.RunAsync();

                Log.Logger.Information("Services Starting");
                return StartService(host.Services);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return Task.FromResult(1); ;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            if (!Debugger.IsAttached)
            {
                throw new Exception("Settings file not found (e.g. appsettings.json)");
            }

            var settingsFileName = Debugger.IsAttached && args.Length == 0 ? "appsettings.json" : args[0];

            return Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddJsonFile(settingsFileName, false, false);
                })
                .ConfigureLogging((hostContext, logging) =>
                {
                    Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .WriteTo.Console()
                   .Enrich.FromLogContext()
                   .CreateLogger();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IRoverLandingService, RoverLandingService>();
                });
        }

        private static async Task StartService(IServiceProvider services)
        {
            try
            {
                using var serviceScope = services.CreateScope();
                var provider = serviceScope.ServiceProvider;
                var workerService = provider.GetRequiredService<IRoverLandingService>();
                await workerService.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Starting Seervice Failed");
                throw;
            }
            finally
            {
                Console.WriteLine("App Closing");
                Environment.Exit(0);
            }
        }
    }
}