using EulerProblems.Lib;
using System.Text;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
	internal class Euler0035 : Euler
	{
		public Euler0035() : base()
		{
			title = "Digit factorials";
			problemNumber = 35;
			PrintTitle();
		}
		public override void Run()
		{
			// this is more efficient with a prime number sieve algorithm
			// but you need to know how many primes you want to create
			// with a sieve. so I first ran the below:

			//long[] primes = PrimeHelper.GetFirstNPrimes(1000000);
			//int howManny = primes.Where(x => x <= max).Count();

			// and that told me that there are 78498 prime numbers below
			// 1MM

			int howManyPrimes = 78498;
			long[] primes = PrimeHelper.GetFirstNPrimes(howManyPrimes);
			int answer = 0;
			for (int i = 0; i < primes.Length; i++)
			{
				if(PrimeHelper.IsCircularPrime(primes[i], primes))
                {
					Console.WriteLine(primes[i]);
					answer++;					
                }
			}
			PrintSolution(answer.ToString());
			return;
		}
		
		
		
	}
}
