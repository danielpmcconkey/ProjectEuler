using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
	internal class Euler0030 : Euler
	{


		public Euler0030() : base()
		{
			title = "Digit fifth powers";
			problemNumber = 30;
			PrintTitle();
		}
		public override void Run()
		{
			int exponent = 5;
			/*
			 * what's the upper bound? I first guessed at 1MM and it gave me the
			 * right answer. But how do I know that 1MM is an appropriate guess?
			 * well, the largest 5-digit number is 99,999. f(99999) = 5 * (9^5)
			 * which = 295,245. As 295,245 is less than 99,999, I have reason to
			 * believe that the answer will have 6 digits. The largest 6 digit
			 * number is 999,999. f(999999) would be 6 * (9^5), which = 354,294. 
			 * 354,294 is way smaller than 999,999. I *think* that, as numers get
			 * bigger, there will be no way for the sum of the digits raised to
			 * the 5th to be as large as the numbers themselves (which are going
			 * up by powers of 10 after all). So the real max should be somewhere
			 * between 99,999 and 999,999. I chose one million because it's 
			 * rounder.
			 * */
			long max = 1000000;
			List<long> fancyNumbers = new List<long>();

			for (long i = 10; i < max; i++)
			{
				BigNumber n = new BigNumber(i); // just a quick way to get all the digits into an arrayn
				long sum = (long)n.digits.Sum(x => Math.Pow(x, 5));
				if(sum == i)
                {
					fancyNumbers.Add(i);

				}
			}
			long answer = fancyNumbers.Sum();
			PrintSolution(answer.ToString());
			return;
		}



	}
}
