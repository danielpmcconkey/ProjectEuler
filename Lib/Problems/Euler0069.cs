#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	
	public class Euler0069 : Euler
	{
		public Euler0069() : base()
		{
			title = "Totient maximum";
			problemNumber = 69;
		}
		protected override void Run()
		{
			/*
			 * This one was both fun and weird for me. I immediately knew how 
			 * to solve it via brute force. Create a cached list of prime 
			 * factors of all digits 1 to 1MM. Then compare n to all digits
			 * from 1 to n - 1. Join their prime factors together in a linq
			 * query and, if the resulting count was 1, then n was relatively
			 * prime to m.
			 * 
			 * The problem is that this was a LOT of calculation. I had what I 
			 * thought was a relatively fast way to get prime factors that I 
			 * stole from an F# tutorial I was learning earlier this week. But
			 * even that took almost 4 minutes just to get the cache of prime 
			 * factors. I killed the program after it ran for another several 
			 * minutes and n was still only up to about 100k. (I later let the 
			 * brute force version run to completion and it took 00:00:00 to 
			 * get the correct answer.)
			 * 
			 * I knew I was doing it wrong, but it took me a bit to realize 
			 * that I was thinking about it backwards. The goal is to find the
			 * highest n divided by phi n. That means that I want to find the n
			 * that has the fewest relative primes (totients?). And that, in 
			 * turn means that I want to find numbers that have a LOT of 
			 * factors.
			 * 
			 * So I started thinking through numbers like 6, 12, 60, 360 but I 
			 * quickly realized that I had no way of knowing whether to use 360
			 * or a multiple of 360. I thought about running a modified version
			 * of my brute force algorithm that only checked multiples of 6. 
			 * But decided I needed more data. 
			 * 
			 * I modified the brute force algorithm to run only up to n <= 10k
			 * and I set it to write any n out to the console that represented
			 * a new maximum for n divided by phi of n. This gave me:
			 * 
			 *      2
			 *      6
			 *      30
			 *      210
			 *      2,310
			 *      
			 * Sure enough, they were all divisible by 6 (except 2, but that 
			 * shouldn't count for that particular analysis). But I also 
			 * decided to try out checking for a relationship between each of 
			 * these values. Was there anything special about them? I dropped 
			 * them into a spreadsheet and divided each number by the number 
			 * prior. This was interesting:
			 * 
			 *      n         relationship to prior n
			 *   ______________________________________
			 *      2         N/A
			 *      6         3
			 *      30        5
			 *      210       7
			 *      2,310     11
			 *      
			 *  They all go up by a factor of primes. Should be easy to 
			 *  predict. Take 2,310 and multiply it by the next prime in the 
			 *  sequence: 13. 2,310 * 13 is 30,030. So I adjusted my brute 
			 *  force method to run up to n = 32k and guess what answer it 
			 *  gave. Yeah, so then all I had to do was multiply 30,030 by the
			 *  next prime number (17) and that gave me the answer to the 
			 *  problem.
			 *  
			 *  So yeah...another one solved in Excel first. Now I'm gonna go
			 *  read the Euler thread and learn about all the math nerds who 
			 *  are shocked we didn't all already know this relationship 
			 *  intuitively.
			 * 
			 * */

			Run_bruteForce(); // never finishes
			Run_elegant();
		}
		private void Run_elegant()
		{
			var limit = 1000000;
			var primes = CommonAlgorithms.GetPrimesUpToN(100);
			int n = 1;
			for(int i = 0; true; i++)
			{
				var p = primes[i];
				var np = n * p;
				if (np > limit) break;
				n = np;
			}
			var answer = n;
			PrintSolution(answer.ToString());
			return;
		}
		private void Run_bruteForce()
		{ 
			var limit = 1000000;
			int[][] primeFactors = new int[limit + 1][];
			for(int n = 2; n <= limit; n++)
			{
				primeFactors[n] = CommonAlgorithms.GetPrimeFactors(n);
			}
			float maxValue = 0.0f;
			int nAtMax = 0;
			for (int n = 2; n <= limit; n++)
			{
				// every n is relatively prime to 1 but 1 isn't prime, so it
				// doesn't show up on the prime factors list. so start the m
				// list at 2 with a relative prime count already set to 1
				float relativePrimeCount = 1; 
				for (int m = 2; m < n; m++)
				{
					var f_n = primeFactors[n].Distinct();
					var f_m = primeFactors[m].Distinct();
					var factorsInCommon = 
						from f1 in f_n
						join f2 in f_m
						on f1 equals f2
						select new { f1, f2 };

					if(factorsInCommon.Count() == 0)
					{
						relativePrimeCount++;
					}
				}
				float nDivRPCount = (relativePrimeCount > 0)
					? n / relativePrimeCount
					: 0;
				if(nDivRPCount > maxValue)
				{
					maxValue = nDivRPCount;
					nAtMax = n;
#if VERBOSEOUTPUT
					Console.WriteLine("n at {0} is relatively prime to {1} numbers with a DivRPCount of {2}",
						n, relativePrimeCount, nDivRPCount); 
#endif
				}
			}

			var answer = nAtMax;
			PrintSolution(answer.ToString());
			return;
		}
	}
}
