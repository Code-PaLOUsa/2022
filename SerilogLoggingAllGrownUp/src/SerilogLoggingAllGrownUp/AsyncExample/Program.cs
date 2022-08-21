using Serilog;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncExample
{
    class Program
    {
        static void Main(string[] args)
        {
            TheProblem();
            TheSolution();
        }

        private static void TheProblem()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var numbers = Enumerable.Range(1, 20_000);

            Parallel.ForEach(numbers, new ParallelOptions { MaxDegreeOfParallelism = 6 }, number =>
             {
                 var square = number * number;
                 Console.WriteLine($"{number}:{square}");
             });

            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Took {elapsed} ms");
        }

        private static void TheSolution()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Async(x => x.Console())
                .CreateLogger();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var numbers = Enumerable.Range(1, 20_000);

            Parallel.ForEach(numbers, new ParallelOptions { MaxDegreeOfParallelism = 6 }, number =>
            {
                var square = number * number;
                Log.Logger.Information($"{number}:{square}");
            });

            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Took {elapsed} ms");
        }
    }
}
