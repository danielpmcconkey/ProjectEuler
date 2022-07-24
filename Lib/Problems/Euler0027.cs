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

			// this is more efficient with a prime number sieve algorithm
			// but you need to know your maximum value to check up to. so
			// I ran this with a standard prime check for each set of a and
			// b values and 12989 is the largest numbeer this algorith
			// checks. So I first grabbed the first 12989 primes and learned
			// that there are 1547 prime numbers <= 12989. so now my prime
			// sieve only looks for the first 1547 prime numbesr
			primes = CommonAlgorithms.GetFirstNPrimes(1548);



            var values = Enumerable.Range((limit * -1), limit * 2 + 1);
            var joinedList = from a in values
                             from b in values
                             select new { countPrimes = HowManyPrimes(a, b), product = a * b };
            var max = joinedList.MaxBy(i => i.countPrimes);
            var answer = max.product;


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
