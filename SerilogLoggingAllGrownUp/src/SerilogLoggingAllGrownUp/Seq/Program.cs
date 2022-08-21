using System;
using Serilog;

namespace Seq
{
    class Program
    {
        static void Main(string[] args)
        {
            Second();

            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        static void Base()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:5341")
                //.Enrich.WithProperty("Version", "1.0.0.0")
                //.Enrich.WithProperty("AppName", "SomeApp")
                .CreateLogger();

            Log.Logger.Information("Starting Service");

            Log.Logger.Information("Locating Customer: {CustomerId} - {CustomerName}", 123, "Brian Korzynski");

            Log.Logger.Warning("Customer {CustomerId} seems to be inactive");
            Log.Logger.Error("There was a problem deleting customer {CustomerId}", 123);
            Log.Logger.Information("Stopping Service");

            //Console.ReadKey();
            Log.CloseAndFlush();
        }

        static void Second()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:5341")
                //.Enrich.WithProperty("Version", "1.0.0.0")
                //.Enrich.WithProperty("AppName", "SomeApp")
                .CreateLogger();

            var contextualLogger = Log.Logger.ForContext("RequestId", Guid.NewGuid());

            contextualLogger.Information("Starting Service");

            contextualLogger.Information("Locating Customer: {CustomerId} - {CustomerName}", 123, "Brian Korzynski");

            contextualLogger.Warning("Customer {CustomerId} seems to be inactive");

            contextualLogger.Error("There was a problem deleting customer {CustomerId}", 123);

            try
            {
                throw new ArgumentException("Problem deleting customer");
            }
            catch (Exception e)
            {
                contextualLogger.Error(e, "There was a problem deleting customer {CustomerId}", 123);
            }
            
            contextualLogger.Information("Stopping Service");

            //Console.ReadKey();
            Log.CloseAndFlush();
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                Log.Logger.Fatal("Unhandled Exception! {Sender}", exception, sender);
            }
            else
            {
                Log.Logger.Fatal("Unhandled Exception! {Sender}", sender);
            }
        }
    }
}
