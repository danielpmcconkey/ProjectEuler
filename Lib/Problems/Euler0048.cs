//#define VERBOSEOUTPUT
using System.Numerics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0048 : Euler
	{
		public Euler0048() : base()
		{
			title = "Self powers";
			problemNumber = 48;
		}
		protected override void Run()
        {
			// Run_bruteForce();
			Run_bruteForceish();

		}
		protected void Run_bruteForceish()
		{
			// this only takes 32 milliseconds. I guess my BigCalculator is bogus
			int limit = 999; // 1000^1000 is only going to add 0s to the last 10 digits
			BigInteger sum = 0;

			for (int i = 1; i <= limit; i++)
			{
				sum += BigInteger.Pow(i, i);
			}
			string sumAsString = sum.ToString();
			string answer = sumAsString.Substring(sumAsString.Length - 10);
			PrintSolution(answer);
			return;
		}
		protected void Run_bruteForce()
		{
			// this way take about 2 minutes to run. gots to be a better way
			int limit = 1000;
			BigNumber sum = new BigNumber(0);

			for(int i = 1; i <= limit; i++)
            {
				BigNumber exponentResult = BigNumberCalculator.Exponent(i, i);
				sum = BigNumberCalculator.Add(sum, exponentResult);
            }
			
			PrintSolution(sum.ToString());
			return;
		}
	}
}
