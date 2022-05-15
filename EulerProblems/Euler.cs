using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems
{
    internal abstract class Euler
    {
        protected string title { get; set; }
        protected int problemNumber { get; set; }
        private Stopwatch stopwatch;
        public Euler()
        {            
            stopwatch = Stopwatch.StartNew();
        }
        public void Deconstructor()
        {
            Console.WriteLine(String.Format("Elapsed time: {0} milliseconds",
                stopwatch.Elapsed.TotalMilliseconds));
        }
        public abstract void Run();

        protected void PrintSolution(string solution)
        {
            Console.WriteLine(String.Format("Solution found {0}", solution));
        }
        protected void PrintTitle()
        {
            Console.WriteLine(string.Format("Running Euler {0}: {1}", problemNumber.ToString("0000"), title));
        }
    }
}
