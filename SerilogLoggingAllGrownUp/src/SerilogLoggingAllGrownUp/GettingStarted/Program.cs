using System;
using Serilog;
using Serilog.Events;

namespace GettingStarted
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstLook();
            MultipleLoggers();
            StaticInstance();
            CertainLevel();
        }

        static void DoSomething()
        {
            Log.Logger.Information("Starting Service");

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
        }

        static void FirstLook()
        {
            //Create the logger
            ILogger logger = new LoggerConfiguration()
                .WriteTo.RollingFile(@"C:\temp\logs\log-FirstLook.txt", LogEventLevel.Information)
                .CreateLogger();

            //Log something
            logger.Information("Did something");
        }

        static void MultipleLoggers()
        {
            //In general this is an ANTI-PATTERN

            //Create the logger
            ILogger logger = new LoggerConfiguration()
                .WriteTo.RollingFile(pathFormat: @"C:\temp\logs\log-MultipleLoggers.txt")
                .CreateLogger();

            ILogger logger2 = new LoggerConfiguration()
                .WriteTo.LiterateConsole()
                .CreateLogger();

            //Log something
            logger.Information("Did something else");
            logger2.Information("Log something to the console");
        }

        static void StaticInstance()
        {
            //Use the static logger instead of having to inject ILogger into every class you want to log

            //Create the logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(@"C:\Temp\logs\log-StaticInstance.txt").CreateLogger();

            DoSomething();
        }

        static void CertainLevel()
        {
            //Create the logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(@"C:\Temp\logs\log-CertainLevel.txt"
                , LogEventLevel.Warning).CreateLogger();

            DoSomething();
        }
    }
}
