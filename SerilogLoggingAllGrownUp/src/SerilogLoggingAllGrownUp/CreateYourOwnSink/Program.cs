using System;
using Serilog;

namespace CreateYourOwnSink
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()  
                .WriteTo.CustomSink()
                .Enrich.WithProperty("Version", "1.0.0.0")
                .Enrich.WithProperty("AppName", "SomeApp")
                .CreateLogger();

            Log.Logger.Information("Some message");
            Log.Logger.Information("Some message 2");

            Console.ReadKey();
        }
    }
}
