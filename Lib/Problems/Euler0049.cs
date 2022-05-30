//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0049 : Euler
	{
		public Euler0049() : base()
		{
			title = "Prime permutations";
			problemNumber = 49;
		}
		protected override void Run()
		{
			// fetch all the primes between 1000 and 9999, ordered descending
			// I figured the primes go down faster from the top, so we're
			// likely to get our answer sooner if we go greatest to least
			int[] primes = CommonAlgorithms.GetPrimesUpToN(9999).Where(x => x >= 1000)
				.OrderByDescending(y => y)
				.ToArray();

			// the bools are faster for checking if a number is prime
			bool[] primesAsBools = CommonAlgorithms.GetPrimesUpToNAsBoolArray(9999);

			foreach(int prime in primes)
            {
				int[][] permutations = CommonAlgorithms.GetAllPermutationsOfArray(
					CommonAlgorithms.ConvertIntToIntArray(prime));
				int totalNumberOfPrimes = 0;
				List<int> primePermutations = new List<int>();
				foreach(var p in permutations)
                {
					int pAsNumber = CommonAlgorithms.ConvertIntArrayToInt(p);					
					if (pAsNumber < primesAsBools.Length && primesAsBools[pAsNumber] 
						&& !primePermutations.Contains(pAsNumber))	 // don't want duplicates
					{
						totalNumberOfPrimes++;
						primePermutations.Add(pAsNumber);
					}
                }
				if(totalNumberOfPrimes >= 3)
                {
					if (totalNumberOfPrimes == 3)
					{
						// if only 3, then the diff between p1 and p2 must be the same as the
						// diff between p2 and p3
						if (primePermutations[2] - primePermutations[1]
							== primePermutations[1] - primePermutations[0])
						{
							// winner, winner, muskrat dinner
							string answer = primePermutations[0].ToString();
							answer += primePermutations[1].ToString();
							answer += primePermutations[2].ToString();
							PrintSolution(answer);
							return;
						}
					}
					else
					{
						List<(int p1, int p2, int diff)> differences = new List<(int p1, int p2, int diff)>();
						foreach (int p1 in primePermutations)
						{
							foreach (int p2 in primePermutations)
							{
								if(p1 != p2) differences.Add((p1, p2, p2 - p1));
							}
						}
						// now group by the diffs
						var groups = from d in differences
									 group d by d.diff into g
									 select new
									 {
										 difference = g.Key,
										 diffCount = g.Count(),
										 perpmutations = g.ToList()
									 };
						var groupsOf2OrMore = groups.Where(x => x.diffCount >= 2);
						if(groupsOf2OrMore.Count() > 0)
                        {
							// winner, winner?
							var permsInCandidate = groupsOf2OrMore.First().perpmutations.ToArray();
                            if (permsInCandidate[0].p2 == permsInCandidate[1].p1)
                            {
								// winner winner, but now we gotta order the strings right
								int[] permsOfTheWinner = new int[]
								{
									permsInCandidate[0].p1,
									permsInCandidate[0].p2,
									permsInCandidate[1].p2,
								};
								Array.Sort(permsOfTheWinner);
                                
                                string answer = permsOfTheWinner[0].ToString();
								answer += permsOfTheWinner[1].ToString();
								answer += permsOfTheWinner[2].ToString();
								PrintSolution(answer);
								return;
                                
							}
                        }
					}
                }
			}
		}
	}
}
