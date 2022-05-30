using System.Diagnostics;

namespace EulerProblems.Lib
{
    public abstract class Euler
    {
        public string solution { get; set; }
        protected string title { get; set; }
        protected int problemNumber { get; set; }
        protected bool shouldRunSilent;
        
        public (string solution, TimeSpan runTime) Solve(bool shouldRunSilent = false)
        {
            this.shouldRunSilent = shouldRunSilent;

            if (!shouldRunSilent)
            {
                Console.WriteLine(string.Format("Running Euler {0}: {1}",
                    problemNumber.ToString("0000"), title));
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            Run();
            stopwatch.Stop();
            if (!shouldRunSilent)
            {
                Console.WriteLine(String.Format("Elapsed time: {0} milliseconds",
                    stopwatch.Elapsed.TotalMilliseconds));
            }

            return (solution, stopwatch.Elapsed);
        }
        
        protected abstract void Run();

        protected void PrintSolution(string solution)
        {
            this.solution = solution;
            if (!shouldRunSilent) Console.WriteLine(String.Format("Solution found {0}", solution));
        }
    }
}
