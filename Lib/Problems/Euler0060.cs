//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0060 : Euler
	{
		int[] primes;
		Dictionary<int, bool> primesTable;
		public Euler0060() : base()
		{
			title = "Prime pair sets";
			problemNumber = 60;
		}
		protected override void Run()
		{
			// Run_slow(); // used to take 6 seconds, but after I optimized prime handling and changed DoAllCombinationsMakeAPrime, it doesn't finish in under a min
			Run_fast(); // 327.4366 ms, but it's ugly AF
		}
		private void Run_fast()
		{
			int maxPrimeToTry = 9000;
			InitPrimes(maxPrimeToTry);

			for (int i = 0; i < primes.Length; i++)
            {
				for (int j = i+1; j < primes.Length; j++)
                {
					int[] thesePrimes = new int[] { primes[i], primes[j] };

					if (DoAllCombinationsMakeAPrime(thesePrimes))
					{
						for (int k = j + 1; k < primes.Length; k++)
						{
							thesePrimes = new int[] { primes[i], primes[j], primes[k] };

							if (DoAllCombinationsMakeAPrime(thesePrimes))
							{
								for (int l = k + 1; l < primes.Length; l++)
								{
									thesePrimes = new int[] { primes[i], primes[j], primes[k], primes[l] };

									if (DoAllCombinationsMakeAPrime(thesePrimes))
									{
										for (int m = l + 1; m < primes.Length; m++)
										{
											thesePrimes = new int[] { 
												primes[i], primes[j], primes[k], primes[l], primes[m] };

											if (DoAllCombinationsMakeAPrime(thesePrimes))
											{
												int answer = thesePrimes.Sum();
												PrintSolution(answer.ToString());
												return;
											}
										}
									}									
								}
							}							
						}
					}					
				}
			}
		}
		private void Run_slow()
		{
			int maxPrimeToTry = 9000;
			InitPrimes(maxPrimeToTry);
			int limit = int.Parse(maxPrimeToTry.ToString() + maxPrimeToTry.ToString());
			int[] primes = CommonAlgorithms.GetPrimesUpToN(limit);
			bool[] primesAsBool = CommonAlgorithms.GetPrimesUpToNAsBoolArray(limit);

			// first get all the 2-prime combos
			
			List<(int a, int b, int bIndex)> combosOf2 = new List<(int, int, int)>();

			for (int i = 0; i < primes.Length; i++)
			{
				for (int j = i + 1; j < primes.Length; j++)
				{
					int[] thesePrimes = new int[] { primes[i], primes[j] };
					
					if (DoAllCombinationsMakeAPrime(thesePrimes))
					{
						combosOf2.Add((primes[i], primes[j], j));
					}
				}
			}

			// now use the 2-prime combos to come up with the 3-prime combos
			List<(int a, int b, int c, int cIndex)> combosOf3 = new List<(int, int, int, int)>();
			for (int i = 0; i < combosOf2.Count; i++)
			{
				int a = combosOf2[i].a;
				int b = combosOf2[i].b;
				int bIndex = combosOf2[i].bIndex;

				for (int j = bIndex + 1; j < primes.Length; j++)
				{
					int[] thesePrimes = new int[] { a, b, primes[j] };

					if (DoAllCombinationsMakeAPrime(thesePrimes))
					{
						combosOf3.Add((a, b, primes[j], j));
					}
				}
			}

			// now use the 3-prime combos to come up with the 4-prime combos
			List<(int a, int b, int c, int d, int dIndex)> combosOf4 = new List<(int, int, int, int, int)>();
			for (int i = 0; i < combosOf3.Count; i++)
			{
				int a = combosOf3[i].a;
				int b = combosOf3[i].b;
				int c = combosOf3[i].c;
				int cIndex = combosOf3[i].cIndex;

				for (int j = cIndex + 1; j < primes.Length; j++)
				{
					int[] thesePrimes = new int[] { a, b, c, primes[j] };

					if (DoAllCombinationsMakeAPrime(thesePrimes))
					{
						combosOf4.Add((a, b, c, primes[j], j));
					}
				}
			}

			// now use the 4-prime combos to find the answer
			for (int i = 0; i < combosOf4.Count; i++)
			{
				int a = combosOf4[i].a;
				int b = combosOf4[i].b;
				int c = combosOf4[i].c;
				int d = combosOf4[i].d;
				int dIndex = combosOf4[i].dIndex;

				for (int j = dIndex + 1; j < primes.Length; j++)
				{
					int[] thesePrimes = new int[] { a, b, c, d, primes[j] };

					if (DoAllCombinationsMakeAPrime(thesePrimes))
					{
						int answer = thesePrimes.Sum();
						PrintSolution(answer.ToString());
						return;
					}
				}
			}
		}
		private bool DoAllCombinationsMakeAPrime(int[] thesePrimes)
        {
			
			// we only need to check the last to the others. assume they've already been 
			// checked against themselves in a prior round
			var thisPrime = thesePrimes[thesePrimes.Length - 1];
			for (int i = 0; i < thesePrimes.Length - 1; i++)
			{
				var otherPrime = thesePrimes[i];
				int arrangement1 = thisPrime + (int)(otherPrime * Math.Pow(10, CommonAlgorithms.GetOrderOfMagnitude(thisPrime) + 1));
				int arrangement2 = otherPrime + (int)(thisPrime * Math.Pow(10, CommonAlgorithms.GetOrderOfMagnitude(otherPrime) + 1));
				if (!IsPrime(arrangement1) || !IsPrime(arrangement2))
                {
                    return false;
                }
			}
			return true;			
		}
		private void InitPrimes(int maxPrimeToTry)
        {
			primes = CommonAlgorithms.GetPrimesUpToN(maxPrimeToTry);
			primesTable = new Dictionary<int, bool>();
			bool[] pBools = new bool[maxPrimeToTry + 1];
			foreach (var p in primes)
			{
				pBools[p] = true;
			}
			for (int i = 0; i < maxPrimeToTry + 1; i++)
			{
				primesTable.Add(i, pBools[i]);
			}
		}
		private bool IsPrime(int n)
        {
			if (!primesTable.ContainsKey(n))
				primesTable.Add(n, CommonAlgorithms.IsPrime(n));
			return primesTable[n];
		}
	}
}
