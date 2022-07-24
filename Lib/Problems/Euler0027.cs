//#define VERBOSEOUTPUT
using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Lib.Problems
{
	public class Euler0027 : Euler
	{
		// long maxPrimeCheck = 0;
		long[] primes;

		public Euler0027() : base()
		{
			title = "Quadratic primes";
			problemNumber = 27;
			
		}
		protected override void Run()
		{
			const int limit = 1000;
			long largestNumberOfPrimes = 0;
			int aAtLargest = 0;
			int bAtLargest = 0;
			int counter = 0;

			// this is more efficient with a prime number sieve algorithm
			// but you need to know your maximum value to check up to. so
			// I ran this with a standard prime check for each set of a and
			// b values and 12989 is the largest numbeer this algorith
			// checks. So I first grabbed the first 12989 primes and learned
			// that there are 1547 prime numbers <= 12989. so now my prime
			// sieve only looks for the first 1547 prime numbesr
			primes = CommonAlgorithms.GetFirstNPrimes(1548);

 
#if VERBOSEOUTPUT
            long testAnswer = HowManyPrimes(1, 41);
            testAnswer = HowManyPrimes(-79, 1601); 
#endif

			// what's 4MM possibilities amongst friends?
			// BRUTE FORCE!!!


			for (int a = limit * -1; a < limit; a++)
            {
				for (int b = limit * -1; b <= limit; b++)
				{
					long numberOfPrimes = HowManyPrimes(a, b);
					if (numberOfPrimes > largestNumberOfPrimes)
					{
						largestNumberOfPrimes = numberOfPrimes;
						aAtLargest = a;
						bAtLargest = b;
					}
					counter++;
				}
			}
#if VERBOSEOUTPUT
            Console.WriteLine(string.Format("Number of primes: {0}", largestNumberOfPrimes));
            Console.WriteLine(string.Format("a: {0}", aAtLargest));
            Console.WriteLine(string.Format("b: {0}", bAtLargest));
            //Console.WriteLine(string.Format("maxPrimeCheck: {0}", maxPrimeCheck));  
#endif

			int answer = aAtLargest * bAtLargest;
			PrintSolution(answer.ToString());
			return;
		}
		
		private long HowManyPrimes(int a, int b)
        {
			long n = 0;
			
			while (true)
            {
				long quadraticResult = (long)(Math.Round(Math.Pow(n, 2) + (a * n) + b, 0));
				
				
				// maxPrimeCheck = Math.Max(maxPrimeCheck, quadraticResult);
				if (primes.Contains(quadraticResult))
				{
					n++;
				}
				else
				{
					return n;
				}
			}
        }

	}
}
