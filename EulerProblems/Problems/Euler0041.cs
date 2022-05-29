using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0041 : Euler
	{
		public Euler0041() : base()
		{
			title = "Pandigital prime";
			problemNumber = 41;
			PrintTitle();
		}
		public override void Run()
		{
            int[] numerals = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			for (int i = numerals.Length - 1; i > 0; i--)
			{
				int[][] permutations = CommonAlgorithms
					.GetAllLexicographicPermutationsOfIntArray(numerals[0..(i + 1)]);

				// reverse the order to start with largest first
				Array.Reverse(permutations);

				for(int j = 0; j < permutations.Length; j++)
                {
					// throw out any numbers that end in 2, 4, 6, 8, or 5
					// because they cannot be prime. yes, 2 and 5 are prime by
					// themselves, but we'll find the largest before then and
					// we'll never have to deal with that.
					int lastDigit = permutations[j][i];
					if(lastDigit == 1 || lastDigit == 3 || lastDigit == 7  || lastDigit == 9)
                    {
						int candidate = CommonAlgorithms.ConvertIntArrayToInt(permutations[j]);
						if(CommonAlgorithms.IsPrime(candidate))
                        {
							PrintSolution(candidate.ToString());
							return;
						}
                    }
				}


			}


			
		}


	}
}
