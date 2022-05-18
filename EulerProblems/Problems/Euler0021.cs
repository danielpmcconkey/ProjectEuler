using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
    internal class Euler0021 : Euler
    {
        public Euler0021() : base()
        {
            title = "Amicable numbers";
            problemNumber = 21;
            PrintTitle();
        }
        public override void Run()
        {
            long lowestNum = 2; // 0 would screw with things and 1 doesn't have a number lower than it
            long highestNum = 10000 - 1; // the problem says numbers under 10000;
            List<long> amicableNumbers = new List<long>();

            for (long i = lowestNum; i <= highestNum; i++)
            {
                if(WeirdAlgorithms.IsAmicableNumber(i))
                {
                    amicableNumbers.Add(i);
#if DEBUG
                    Console.WriteLine(i); 
#endif
                }
            }

            // now sum them
            long answer = amicableNumbers.Sum();

            PrintSolution(answer.ToString());
            return;
        }
    }
}
