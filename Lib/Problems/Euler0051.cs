//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0051 : Euler
	{
		public Euler0051() : base()
		{
			title = "Prime digit replacements";
			problemNumber = 51;
		}
		protected override void Run()
		{
			int limit = 1000000;
			int[] primes = CommonAlgorithms.GetPrimesUpToN(limit);
			bool[] primesAsBool = CommonAlgorithms.GetPrimesUpToNAsBoolArray(limit);

			int target = 8;

			// count the primes <= 56003
			//var countPrimes = primesAsBool[0..56003].Where(x => x == true).Count();
			// 5683

			int iStart = 5683; // 56003 is the 5683rd prime

			
			for(int i = iStart; i < primes.Length; i++)
            {
				int p_i = primes[i];
				int[] p_i_digits = CommonAlgorithms.ConvertIntToIntArray(p_i);
				int howManyDigits = CommonAlgorithms.GetOrderOfMagnitude(p_i) + 1;
				if (howManyDigits > 6) break;
				// how many permutations of {digit, *} are there?
				// 2^ (howManyDigits - 1)
				int howManyPermutations = (int)Math.Pow(2, howManyDigits);
				// create that many binary strings
				List<char[]> binaryCombos = new List<char[]>();
				for(int j = 0; j < howManyPermutations; j++)
                {
					binaryCombos.Add(Convert.ToString(j, 2).PadLeft(howManyDigits,'0').ToCharArray());
                }
				// for each binary combo, replace 1s with the digit of the
				// number and 0s with a replacement
				foreach(var combo in binaryCombos)
                {
#if VERBOSEOUTPUT
					Console.WriteLine(string.Format("p_i: {0}; combo: {1};", p_i, string.Join(" ", combo)));
#endif
					Dictionary<int, bool> checkedVals = new Dictionary<int, bool>();
					for(int digitReplacement = 0; digitReplacement < 10; digitReplacement++)
                    {
#if VERBOSEOUTPUT
						Console.WriteLine(string.Format("     digit replacement: {0}", digitReplacement));
#endif
						int[] newArray = new int[howManyDigits];
						Array.Copy(p_i_digits, newArray, howManyDigits);
						for(int k = 0; k < howManyDigits; k++)
                        {
							if (combo[k] == '0') newArray[k] = digitReplacement;
                        }
						// evaluate newArray for prime
						int newArrayAsInt = CommonAlgorithms.ConvertIntArrayToInt(newArray);
						// make sure the new number has as many digits as the orig. they don't
						// count 03 in their example for *3
						int newNumDigits = CommonAlgorithms.GetOrderOfMagnitude(newArrayAsInt) + 1;
						if (newNumDigits != howManyDigits) continue;
#if VERBOSEOUTPUT
						Console.WriteLine(string.Format("     new Array: {0}", string.Join(" ", newArray)));
#endif
						if (!checkedVals.ContainsKey(newArrayAsInt))
						{
							if (newArrayAsInt < primesAsBool.Length && primesAsBool[newArrayAsInt])
							{
#if VERBOSEOUTPUT
								Console.WriteLine(string.Format("     prime! {0}{1}{2}",
									p_i.ToString().PadLeft(10),
									digitReplacement.ToString().PadLeft(10),
									newArrayAsInt.ToString().PadLeft(10)));
#endif
								checkedVals.Add(newArrayAsInt, true);
							}
							else checkedVals.Add(newArrayAsInt, false);
						}
                    }
					int numPrimes = checkedVals.Where(x => x.Value == true).Count();
#if VERBOSEOUTPUT
					Console.WriteLine(string.Format("p_i: {1}; combo: {2}; num primes: {0}", numPrimes, p_i, string.Join(" ", combo))); 
#endif
                    if(numPrimes >= target)
                    {
						// winner, winner, hamburger helper dinner
						int answer = checkedVals.Min(x => x.Key);
                        PrintSolution(answer.ToString());
                        return;
                    }
                }
            }
        }
	}
}
