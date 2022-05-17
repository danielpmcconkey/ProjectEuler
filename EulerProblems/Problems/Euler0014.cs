using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
    internal class Euler0014 : Euler
    {
        public Euler0014() : base()
        {
            title = "Longest Collatz sequence";
            problemNumber = 14;
            PrintTitle();
        }
        public override void Run()
        {
            long longestChain = 0;
            int longestChainStartingValue = 0;
            int stoppingPoint = 1000000;
            int startingPoint = 10;

            for(int i = startingPoint; i <= stoppingPoint; i++)
            {
                long chainLength = HowLongIsTheCollatzChainForN(i);
                if(chainLength > longestChain)
                {
                    longestChain = chainLength;
                    longestChainStartingValue = i;
                }
            }



            PrintSolution(longestChainStartingValue.ToString());
            return;
        }
        private long HowLongIsTheCollatzChainForN(int n)
        {
            long currentVal = n;   // needs to be a long because some values will overrun an int
            long chainLength = 1;
            while (currentVal != 1)
            {
                // if currentVal is even, divide by 2
                // if currentVal is odd, muliply by 3 and add 1
                if(currentVal % 2 == 0)
                {
                    currentVal = currentVal / 2;
                }
                else
                {
                    currentVal = (currentVal * 3) + 1;
                }
                chainLength++;                
            }
            return chainLength;
        }
    }
}
