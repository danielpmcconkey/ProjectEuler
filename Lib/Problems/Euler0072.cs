//#define VERBOSEOUTPUT
using System.Diagnostics;
using System.Text;

namespace EulerProblems.Lib.Problems
{
	public class Euler0072 : Euler
	{
		public Euler0072() : base()
		{
			title = "Counting fractions";
			problemNumber = 72;
		}
		protected override void Run() 
		{
			/*
			 * This one was tough, but fun for me. I've since learned that I 
			 * could've just summed up the Euler totients phi of 2 to phi of 
			 * 1MM. But I didn't know that then and I have no idea how I'd have
			 * learned that other than through reading the problem thread.
			 * 
			 * So instead I was left to my own meager wits. The brute force 
			 * method proved accurate, but after 30 minutes, it had only gotten
			 * to a denominator of about 50k. Not gonna cut it. So I took to my
			 * old habbits and tried to find relationships between rows. This 
			 * was very time consuming and I went through many iterations of 
			 * reasoning. Finally, I discovered said relationship using the 
			 * text in the verboseOutput string in the brute force mechanism. 
			 * Here it is for the first few denominators:
			 * 
			 *      [2]                 *
			 *      [3]                 **
			 *      [2, 2]              *-*
			 *      [5]                 ****
			 *      [2, 3]              *---*
			 *      [7]                 ******
			 *      [2, 2, 2]           *-*-*-*
			 *      [3, 3]              **-**-**
			 *      [2, 5]              *-*---*-*
			 *      [11]                **********
			 *      [2, 2, 3]           *---*-*---*
			 *      [13]                ************
			 *      [2, 7]              *-*-*---*-*-*
			 *      [3, 5]              **-*--**--*-**
			 *      [2, 2, 2, 2]        *-*-*-*-*-*-*-*
			 *      [17]                ****************
			 *      [2, 3, 3]           *---*-*---*-*---*
			 *      [19]                ******************
			 *      [2, 2, 5]           *-*---*-*-*-*---*-*
			 *      [3, 7]              **-**--*-**-*--**-**
			 *      [2, 11]             *-*-*-*-*---*-*-*-*-*
			 *      [23]                **********************
			 *      [2, 2, 2, 3]        *---*-*---*-*---*-*---*
			 *      [5, 5]              ****-****-****-****-****
			 *      [2, 13]             *-*-*-*-*-*---*-*-*-*-*-*
			 *      [3, 3, 3]           **-**-**-**-**-**-**-**-**
			 *      [2, 2, 7]           *-*-*---*-*-*-*-*-*---*-*-*
			 *      [29]                ****************************
			 *      [2, 3, 5]           *-----*---*-*---*-*---*-----*
			 *      
			 * The brackets show the prime factors of the denominator. The 
			 * asterisks represent a numerator that would produce a reduced 
			 * fraction and the dashes are for numerators that produce a 
			 * fraction that should be reduce (and wouldn't count for my 
			 * answer).
			 * 
			 * I could see patterns. One of the first that jumped out was that
			 * every multiple of 6 had the same *---*-*---* pattern. Except I 
			 * learned that 30 broke that pattern. Eventually, after playing 
			 * with numbers long enough, I found the connection. It was unique
			 * primes. If, I denominator D had unique prime factors of 2, 3, 
			 * and 5, then the number of non-reducible fractions with that 
			 * denominator would be D * 1/2 * 2/3 * 4/5. 
			 * 
			 * Thus the logic you see in the Run_elegant method. However, while
			 * the logic runs likety split, creating a list of prime factors 
			 * was my long pole in the tent. My 
			 * CommonAlogorithms.GetPrimeFactors took about 3.5 minutes to run 
			 * from n = 1 to n = 1MM. So I had to re-jigger that. I basically 
			 * built a method that takes every prime number P form 1 to 1MM and
			 * crawls up the array, adding P to every list that is a multiple 
			 * of P.
			 * 
			 * It works. And it runs in about a second.
			 * 
			 * */

			//Run_bruteForce(); // never finishes
			Run_elegant(); // 1132.0028 milliseconds
		}
		private void Run_elegant()
		{
			int dMax = 1000000;
			
			var primeFactors = CommonAlgorithms.GetUniquePrimeFactorsUpToN(dMax);

			/*
			 * Da rules
			 * 
			 * For any given denominator D:
			 *   Find the unique prime factors
			 *   Subtract 1 from each. Call that pfMinus
			 *   Find the product of all pfMinus values call that product_pfMinus
			 *   Find the product of all prime factors and call that product_pf
			 *   The number of new fractions for D is (product_pfMinus / product_pf) * D
			 * 
			 * */

			
			Func<int, int> subtract1 = (n) => n - 1;

			long answer = 0;
			for (int d = 2; d <= dMax; d++)
			{
				var distinctPrimes = primeFactors[d].Distinct();
				long product_pf = distinctPrimes.Aggregate(1, (n, m) => n * m);
				long product_pfMinus = distinctPrimes.Aggregate(1, (n, m) => n * subtract1(m));
				long newCount_calc = (long)Math.Round(d * (product_pfMinus / (float)product_pf));
				answer += newCount_calc;
			}
			
			PrintSolution(answer.ToString());
			return;
		}
		private void Run_bruteForce()
		{
			int dMax = 1000000;
			int[][] primeFactors = new int[dMax + 1][];
			for (int n = 1; n <= dMax; n++)
			{
				primeFactors[n] = CommonAlgorithms.GetPrimeFactors(n);
			}
			Stopwatch sw = Stopwatch.StartNew();
			StringBuilder sb = new StringBuilder();
									
			long answer = 0;
			int previousSecs = 0;
			for(int d = 2; d <= dMax; d++)
			{
				if (d % 1000 == 0)
				{
					var totalSecs = (int)sw.Elapsed.TotalSeconds;
					var secsThisThou = totalSecs - previousSecs;
					previousSecs = totalSecs;
					Console.WriteLine("{0}	{1}", secsThisThou, d);
				}
				string factorsStr = "[";
				for(int i = 0; i < primeFactors[d].Length; i++)
				{
					if (i > 0) factorsStr += ", ";
					factorsStr += primeFactors[d][i];
				}
				factorsStr += "]";
				sb.Append(string.Format("{0}", factorsStr.PadRight(20)));
				long newCount = 0;
				for(int n = 1; n < d; n++)
				{
					if(CommonAlgorithms.AreTwoNumbersRelativelyPrime(n, d, primeFactors))
					{
						answer++;
						newCount++;
						sb.Append("*");
					}
					else sb.Append("-");
				}
#if VERBOSEOUTPUT
				sb.AppendLine();
#endif
			}
			string verboseOutput = sb.ToString();
			PrintSolution(answer.ToString());
			return;
		}
	}
}
