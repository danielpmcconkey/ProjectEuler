using System.Diagnostics;

namespace EulerProblems.Lib
{
    internal static class Timing
    {
        internal static T_Out TimeFunction<T_In, T_Out>(Func<T_In, T_Out> func, T_In parameter)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            T_Out result = func.Invoke(parameter);
            sw.Stop();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(func.Method);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(" with parameter ");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(parameter);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(" completed in ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0:n0}", sw.ElapsedTicks);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(" ticks with result ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result);
            Console.ForegroundColor = ConsoleColor.White;

            return result;
        }
    }
}
