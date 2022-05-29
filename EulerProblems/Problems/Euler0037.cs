using EulerProblems.Lib;

namespace EulerProblems.Problems
{
    internal class Euler0037 : Euler
	{
		public Euler0037() : base()
		{
			title = "Truncatable primes";
			problemNumber = 37;
			PrintTitle();
		}
		public override void Run()
		{
			//var test = CommonAlgorithms.IsTruncatablePrime(3797, CommonAlgorithms.GetFirstNPrimes(3797));
			int goal = 11;
			long answer = 0;
			int max = 100000;	// just a guess; I can't figure out how to confirm an upper bound
			long[] primes = CommonAlgorithms.GetFirstNPrimes(max);
			for(int i = 0; i < primes.Length && goal > 0; i++)
            {
				if(CommonAlgorithms.IsTruncatablePrime(primes[i], primes))
                {
					answer += primes[i];
					Console.WriteLine(primes[i]);
					goal--;
				}
            }	
            
			PrintSolution(answer.ToString());
			return;
		}

	}
}
