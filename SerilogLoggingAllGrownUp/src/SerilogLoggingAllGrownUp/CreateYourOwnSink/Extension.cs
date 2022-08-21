using Serilog;
using Serilog.Configuration;

namespace CreateYourOwnSink
{
   public static class Extension
    {
       public static LoggerConfiguration CustomSink(this 
           LoggerSinkConfiguration loggerSinkConfiguration)
       {
           return loggerSinkConfiguration.Sink(new CustomSink());
       }
    }
}
