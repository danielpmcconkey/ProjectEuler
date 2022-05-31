//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0050 : Euler
	{
		public Euler0050() : base()
		{
			title = "Consecutive prime sum";
			problemNumber = 50;
		}
		protected override void Run()
		{
			//Run_bruteForce(); // takes about 60 seconds to run
			Run_elegant(); // runs in 223 ms
		}
		protected void Run_elegant()
		{
			/*
			 * you don't need to check every possible sum. instead check all the sums 
			 * that start with the first prime and go all the way up until the the sum
			 * of primes[2..N] is greater than the largest prime under 1MM. Then move 
			 * your starting i up one in the prime array and do the same. Except this 
			 * time you don't need to check primes[i..i+1] you can start with the max
			 * from the previous checks. So, if the biggest consecutive streak that 
			 * started with 2 was 11 long, you can start by only checking 
			 * primes[i..i+12] because anything shorter wouldn't give you a new high 
			 * water mark
			 * */
			 
			int answer = 0;
			int mostNumberOfConsecutivePrimes = 1;
			int limit = 1000000;
			int[] primes = CommonAlgorithms.GetPrimesUpToN(limit);
            bool[] primesAsBool = CommonAlgorithms.GetPrimesUpToNAsBoolArray(limit);
			int biggestPrime = primes[primes.Length - 1];

			for (int i = 0; i < primes.Length; i++)
			{
				// start with 2 and go all the way up
				int rangeMin = i + mostNumberOfConsecutivePrimes; // no sense checking anything fewer than the current max

				for (int j = rangeMin; j <= primes.Length;  j++)
                {
					int sum = primes[i..j].Sum();
					if (sum > biggestPrime)
					{
						break;
					}
                    if (primesAsBool[sum])
                    {
						int numConsecutivePrimes = j - i;
						if(numConsecutivePrimes > mostNumberOfConsecutivePrimes)
                        {
							answer = sum;
							mostNumberOfConsecutivePrimes = numConsecutivePrimes;
                        }
                    }
                }
			}
			PrintSolution(answer.ToString());
			return;
		}
		protected void Run_bruteForce()
		{
			int answer = 0;
			int mostNumberOfConsecutivePrimes = 0;
			int limit = 1000000;
			int[] primes = CommonAlgorithms.GetPrimesUpToN(limit);
			for (int i = primes.Length - 1; i >= 0; i--)
			{
				int p_i = primes[i];

				for (int j = 0; j < i; j++)
				{
					int p_j = primes[j];
					int j_sum = p_j;
					int numConsecutivePrimes = 1;
					for (int k = j + 1; j_sum < p_i; k++)
					{
						int p_k = primes[k];
						j_sum += p_k;
						numConsecutivePrimes++;
					}
					if(j_sum == p_i)
                    {
						if(numConsecutivePrimes > mostNumberOfConsecutivePrimes)
                        {
							mostNumberOfConsecutivePrimes = numConsecutivePrimes;
							answer = p_i;
                        }						
					}
				}
			}
			PrintSolution(answer.ToString());
			return;

		}
	}
}
