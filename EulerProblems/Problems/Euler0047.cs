using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0047 : Euler
	{
		public Euler0047() : base()
		{
			title = "Distinct primes factors";
			problemNumber = 47;
			PrintTitle();
		}
		public override void Run()
		{
			int targetPrimeCount = 4;
			// get y'all an easy to reference list of primes so the prime factors call
			// goes all fast and stuff
			bool[] primes = CommonAlgorithms.GetPrimesUpToNAsBoolArray(1000000);

			// create a starting point that mimics the end of the last iteration
			int startingNumber = 646;
			int[] primeFactorsOfI = new int[0];
			int[] primeFactorsOfIMinus1 = CommonAlgorithms.GetPrimeFactors(startingNumber - 1, primes);
			int[] primeFactorsOfIMinus2 = CommonAlgorithms.GetPrimeFactors(startingNumber - 2, primes);
			int[] primeFactorsOfIMinus3 = CommonAlgorithms.GetPrimeFactors(startingNumber - 3, primes);

			for (int i = startingNumber; true; i++)
            {
				if (i > int.MaxValue) throw new OverflowException();

				// update factors of i
				primeFactorsOfI = CommonAlgorithms.GetPrimeFactors(i, primes);
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
