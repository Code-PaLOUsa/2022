using System;
using Serilog.Core;
using Serilog.Events;

namespace CreateYourOwnSink
{
    public class CustomSink : ILogEventSink
    {
        public void Emit(LogEvent logEvent)
        {
            string format = $"{DateTime.Now} {logEvent.Level} AppName:{logEvent.Properties["AppName"]} Version:{logEvent.Properties["Version"]}: {logEvent.RenderMessage()}";

            Console.WriteLine(format);
        }
    }
}
