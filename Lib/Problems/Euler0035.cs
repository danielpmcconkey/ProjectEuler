//#define VERBOSEOUTPUT

namespace EulerProblems.Lib.Problems
{
    public class Euler0035 : Euler
	{
		public Euler0035() : base()
		{
			title = "Circular primes";
			problemNumber = 35;
			
		}
        protected override void Run() 
		{
			// this is more efficient with a prime number sieve algorithm
			// but you need to know how many primes you want to create
			// with a sieve. so I first ran the below:

			//long[] primes = CommonAlgorithms.GetFirstNPrimes(1000000);
			//int howManny = primes.Where(x => x <= max).Count();

			// and that told me that there are 78498 prime numbers below
			// 1MM

			int howManyPrimes = 78498;
			long[] primes = CommonAlgorithms.GetFirstNPrimes(howManyPrimes);
			int answer = 0;
			for (int i = 0; i < primes.Length; i++)
			{
				if(CommonAlgorithms.IsCircularPrime(primes[i], primes))
                {
#if VERBOSEOUTPUT
					Console.WriteLine(primes[i]);
#endif
					answer++;					
                }
			}
			PrintSolution(answer.ToString());
			return;
		}
		
		
		
	}
}
