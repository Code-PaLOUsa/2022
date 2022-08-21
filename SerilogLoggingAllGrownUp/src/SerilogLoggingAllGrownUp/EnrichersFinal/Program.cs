using System;
using System.IO;
using Serilog;

namespace Enrichers
{
    class Program
    {
        static Program()
        {
            Directory.GetFiles(@"C:\temp\Logs").Foreach(File.Delete);
        }

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .WriteTo.Seq("http://localhost:5341")
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Version", "1.0.0")
                .CreateLogger();

            GlobalEnrichment();
            ContextualEnchriment();
        }

        static void GlobalEnrichment()
        {
            try
            {
                Log.Logger.Warning("Trying to do something");
                throw new InvalidOperationException("You can't do that.");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "I don't know something happed.");
            }

            Log.Logger.Information("Stopping Service");

            Console.ReadKey();
        }

        static void ContextualEnchriment()
        {
            var log = Log.Logger.ForContext("CustomerId", 12345);

            log.Information("Starting Service");

            try
            {
                log.Warning("Trying to do something");
                throw new InvalidOperationException("You can't do that.");
            }
            catch (Exception ex)
            {
                log.Error(ex, "I don't know something happed.");
            }

            log.Information("Stopping Service");

            Console.ReadKey();
        }
    }
}
