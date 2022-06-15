namespace EulerProblems.Lib.Problems
{
	public class Euler0047 : Euler
	{
		public Euler0047() : base()
		{
			title = "Distinct primes factors";
			problemNumber = 47;
			
		}
		protected override void Run()
        {
			//Run_slow();	// Elapsed time: 4111.7067 milliseconds
			Run_fast();		// Elapsed time: 13.0281 milliseconds

		}
		protected void Run_fast()
        {
			/*
			 * this uses a sieve method to avoid calculating factors for each 
			 * integer. the idea is to set up an array of zeros up to a pre-
			 * determined maximum (1MM here). Then, start an i loop at 2. For 
			 * each multiple of 2 increment your array of zeros in that 
			 * position by 1. So first iteration, you'll have
			 * 
			 *      0 0 1 0 1 0 1 0 1...
			 * 
			 * Once you've done all the factors of 2, move to 3. Note that 
			 * array[3] is not already a 1, so it wasn't a factor of 2, meaning
			 * that 3 is prime. So now increment all the multiples of 3. 3, 6, 
			 * 9, etc.
			 * 
			 *      0 0 1 1 1 0 2 0 1...
			 *      
			 * Notice the 6th position is a 2. That's because 6 has prime 
			 * factors of 2 and 3. So let's go to 4. 4 will be skipped because
			 * it's already a 1 in our array, meaning that it has a factor of a
			 * prime already (2) and is therefore not prime. So we don't want 
			 * to increment any multiples of 4 because 4 isn't a prime and 
			 * wouldn't be a prime factor. On to 5.
			 * 
			 *      0 0 1 1 1 1 2 0 1...
			 *      
			 * Keep doing this all the way up and you'll have an array of 
			 * numbers i => 0..max that represent the count of prime factors
			 * for i.
			 * 
			 * */
			int max = 1000000;
			// set up an array of 0s with length max
			int[] numFactors = new int[max];

			for (int i = 2; i < max; i++)
			{
				if (numFactors[i] != 0)
				{
					// if our i place in numFactors is != 0, that means that i
					// is a multiple of an earlier factor and therefore not
					// prime
					continue;
				}

				for(int a = i * 2; a < max; a += i)
                {
					numFactors[a]++;
                }
            }
			// now we have our counts of prime factors, let's find the first 4
			// consecutive values with at least 4 prime factors
			
			int startingNumber = 646; // assume it's higher than the first 3-digit answer from the problem
			for (int i = startingNumber; i < numFactors.Length - 4; i++)
            {
				if(numFactors[i] >= 4
					&& numFactors[i + 1] >= 4
					&& numFactors[i + 2] >= 4
					&& numFactors[i + 3] >= 4
					)
                {
					PrintSolution(i.ToString());
					return;
				}

			}
        }
		protected void Run_slow()
		{
			int targetPrimeCount = 4;
			// get y'all an easy to reference list of primes so the prime factors call
			// goes all fast and stuff
			bool[] primes = CommonAlgorithms.GetPrimesUpToNAsBoolArray(1000000);

			// create a starting point that mimics the end of the last iteration
			int startingNumber = 646;
			int[] primeFactorsOfI = new int[0];
			int[] primeFactorsOfIMinus1 = CommonAlgorithms.GetPrimeFactors(startingNumber - 1);
			int[] primeFactorsOfIMinus2 = CommonAlgorithms.GetPrimeFactors(startingNumber - 2);
			int[] primeFactorsOfIMinus3 = CommonAlgorithms.GetPrimeFactors(startingNumber - 3);

			for (int i = startingNumber; true; i++)
            {
				if (i > int.MaxValue) throw new OverflowException();

				// update factors of i
				primeFactorsOfI = CommonAlgorithms.GetPrimeFactors(i);
				if(
					primeFactorsOfI.Length >= targetPrimeCount
					&& primeFactorsOfIMinus1.Length >= targetPrimeCount
					&& primeFactorsOfIMinus2.Length >= targetPrimeCount
					&& primeFactorsOfIMinus3.Length >= targetPrimeCount
				)
                {
					PrintSolution((i - 3).ToString());
					return;
				}

				// move the factors arrays down
				primeFactorsOfIMinus3 = primeFactorsOfIMinus2;
				primeFactorsOfIMinus2 = primeFactorsOfIMinus1;
				primeFactorsOfIMinus1 = primeFactorsOfI;
			}
		}
	}
}
