#define VERBOSEOUTPUT
using System.Diagnostics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0070 : Euler
	{
		Dictionary<int, int> phi;
        const int limit = 10000000;

		public Euler0070() : base()
		{
			title = "Totient permutation";
			problemNumber = 70;
		}
		protected override void Run()
		{
			/*
			 * This one kicked my ass. I tried serveral versions of getting the
			 * brute force approach to a runtime lower than a minute. The best
			 * that did was get up to n = 1,956,103. That took about 11 minutes
			 * and each iteration of n was taking longer than the last.
			 * 
			 * So then I tried clever ways to pre-fill my phi list. I knew that
			 * if m and n were relative primes to each other, then the phi of 
			 * mn would equal phi of m * phi of n. This is referred to as 
			 * Euler's product formula. 
			 * 
			 * https://en.wikipedia.org/wiki/Euler%27s_totient_function#Euler's_product_formula
			 * 
			 * So I tried creating a list of phi values that, whenever I found 
			 * a phi, I would try to use it to generate more. But that was also 
			 * taking forever.
			 * 
			 * So I tried to see if I could get away with narrowing the number
			 * of n candidates. I started with just the prime numbers, but 
			 * their phi values would never be a permutation of them, since all
			 * primes' phi is n - 1. But, using Euler's product formula, and in 
			 * conjunction with the handy dandy ease of finding the phi of 
			 * primes, I very quickly put together a list of phi values 
			 * representing n of all primes and n of the product of any two 
			 * primes.
			 * 
			 * I didn't think this would produce the ultimate answer, though as
			 * the brute force method had previously produced the following 
			 * result log:
			 * 
			 *    n at 21 is relatively prime to 12 numbers with a DivRPCoun...
			 *    n at 291 is relatively prime to 192 numbers with a DivRPCo...
			 *    n at 2817 is relatively prime to 1872 numbers with a DivRP...
			 *    n at 2991 is relatively prime to 1992 numbers with a DivRP...
			 *    n at 4435 is relatively prime to 3544 numbers with a DivRP...
			 *    n at 20617 is relatively prime to 20176 numbers with a Div...
			 *    ...
			 *    
			 * The program logged n every time n / phi of n hit a new low. Not
			 * shown here, but I had logged the first 23 such n values. So, 
			 * after I'd narrowed down my phi list, I checked each of these 
			 * "local" minima to see if my phi list contained their n value. 
			 * And it did, except for 2817. That caused me to spend about 
			 * another hour trying to get a better phi list. In the end, I gave
			 * up and just ran with a phi list that only contained primes, and
			 * the products of 2 primes. 
			 * 
			 * It ran in 5 seconds and gave me a Euler-accepted answer. After 
			 * reading the Euler thread, I was able to optimize it a bit 
			 * further limiting my product of 2 primes to be 2 primes that 
			 * stick close to the square root of 10MM. I never found any good
			 * logic to suggest a mathematical rationale for how close, though.
			 * In the end, I used 2x and 1/2 of the square root of 10MM as my
			 * min and max.
			 * 
			 * It now runs in 2 seconds.
			 * 
			 * */

			PreFillPhi();
			
			float minValue = float.MaxValue;
			int nAtMin = 0;

#if VERBOSEOUTPUT
			Stopwatch sw = Stopwatch.StartNew();
#endif

			foreach (var pair in phi.OrderBy(x => x.Key))
			{
				int n = pair.Key;
				int phiOfN = pair.Value;
				// now check if n and phi of n are permutations of each other
				if (CommonAlgorithms.AreTwoIntegersPermutationsOfEachOther(n, phiOfN))
				{
					float nDivRPCount = (phiOfN > 0)
						? n / (float)phiOfN
						: 0;
					if (nDivRPCount < minValue)
					{
						minValue = nDivRPCount;
						nAtMin = n;
#if VERBOSEOUTPUT
						Console.WriteLine("{3} | n at {0} is relatively prime to {1} numbers with a DivRPCount of {2}",
							n, phiOfN, nDivRPCount, sw.ElapsedMilliseconds);
#endif
					}
				}
			}
			var answer = nAtMin;
			PrintSolution(answer.ToString());
			return;
		}		
		private void PreFillPhi()
		{
			phi = new Dictionary<int, int>();
			
			
			var primes = CommonAlgorithms.GetPrimesUpToN(limit);
			for (int n = 0; n < primes.Length; n++)
			{
				phi[primes[n]] = primes[n] - 1;
			}

			// now, according to wikipedia when 2 numbers, m and n, are
			// relatively prime to each other, then phi of (m*n) is equal to
			// phi of m * phi of n. So multiply all primes' phi together to get
			// many of the most likely candidate. A little cheating here to 
			// optimize. We know that the candidate that produces the min
			// answer should be the product of 2 high primes. So therefore, our
			// answer is very unlikely to be divisible by a prime that drifts
			// too far from the square root of 10MM.

			int maxPrimeAsProduct = (int)Math.Ceiling(Math.Sqrt(limit) * 2);
			int minPrimeAsPRoduct = (int)Math.Ceiling(Math.Sqrt(limit) * 0.5);
			for (int i = 0; true; i++)
			{
				int m = primes[i];
				if (m > maxPrimeAsProduct) break;
				if (m < minPrimeAsPRoduct) continue;
				for(int j = 0; true; j++)
				{
					int n = primes[j];
					if (n == m) continue;
					if (n > maxPrimeAsProduct) break;
					if (n < minPrimeAsPRoduct) continue;
					long mn = m * (long)n;
					if (mn > limit) break;
					phi[(int)mn] = phi[m] * phi[n];
				}
			}			
		}				
	}
}
