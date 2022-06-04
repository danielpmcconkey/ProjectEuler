//#define VERBOSEOUTPUT
using System.Numerics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0056 : Euler
	{
		public Euler0056() : base()
		{
			title = "Powerful digit sum";
			problemNumber = 56;
		}
		protected override void Run()
		{
			const int limit = 100;
			int maxDigitSum = 0;
			for(int a = 0; a < limit; a++)
            {
				for (int b = 0; b < limit; b++)
				{
					BigInteger exponentialResult = BigInteger.Pow(a, b);
					char[] resultAsChars = exponentialResult.ToString().ToCharArray();
					int sum = 0;
					foreach(char c in resultAsChars)
                    {
						sum += int.Parse(c.ToString());
                    }
					if(sum > maxDigitSum)
                    {
						maxDigitSum = sum;
                    }
				}
			}
			PrintSolution(maxDigitSum.ToString());
			return;
		}
	}
}
