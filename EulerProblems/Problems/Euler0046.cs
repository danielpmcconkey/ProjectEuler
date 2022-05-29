using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0046 : Euler
	{
		public Euler0046() : base()
		{
			title = "Goldbach's other conjecture";
			problemNumber = 46;
			PrintTitle();
		}
		public override void Run()
		{
			int[] primes = PrimeHelper.GetPrimesUpToX(1000000).ToArray();
			long[] squares = WeirdAlgorithms.GetFirstNPerfectSquares(
				(int)(Math.Floor(Math.Pow(int.MaxValue, 0.5))));
			for (long i = 33; true; i += 2)	// only check odd numbers
            {
				if(WeirdAlgorithms.IsComposite(i))
                {
					bool canBeWritten = false;
					long[] squaresLessThan = squares.Where(x => x < (i * 0.5)).ToArray();
					int[] primesLessThan = primes.Where(x => x < i).ToArray();
					for(int j = 0; !canBeWritten && j < squaresLessThan.Length; j++)
                    {
						long square = (long)squaresLessThan[j];

						for (int k = 0; !canBeWritten && k < primesLessThan.Length; k++)
						{
							long prime = (long)primesLessThan[k];

							if(i == prime + (2 * square))
                            {
								canBeWritten = true;
                            }
						}
					}				
					if(!canBeWritten)
                    {
						// bob's your uncle
						PrintSolution(i.ToString());
						return;
					}
                }
            }
			
		}
		
	}
}
