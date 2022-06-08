//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0059 : Euler
	{
		Dictionary<char, double> expectedLetterFrequencies;
#if VERBOSEOUTPUT
        List<double> variances; 
#endif
		public Euler0059() : base()
		{
			title = "XOR decryption";
			problemNumber = 59;
		}
		protected override void Run()
		{
			/*
			 * This problem was tough for me to understand at first. It's a 
			 * huge departure from the formula thus far with these problems
			 * and it took me a while to understand what problem I was solving.
			 * Once I understood though, it was easy to break down.
			 * 
			 * Part 1 was figuring out how to generate all the possible 
			 * passwords and apply them to the input chars. That wasn't tough. 
			 * 
			 * Part 2 was figuring out which password was right. I knew that a 
			 * lot of code breaking was figuring out word distribution or 
			 * letter distribution and comparing it to a known set. I wasn't 
			 * sure if we had a large enough sample size, so I decided to go 
			 * with letter distributions as there are more letters than words.
			 * 
			 * Maybe that doesn't make sense. I don't know. I went with it and 
			 * it worked. Wikipedia lists letter frequencies so it wasn't hard
			 * to implement. All I had to do was determine my letter 
			 * frequencies and compare them to the known average frequencies, 
			 * and any passwords that yielded "plausible" frequencies had a 
			 * high probability of being correct.
			 * 
			 * But how to determine a "plausible" result. I created a variance
			 * that tracked all letters actual frequency vs expected. I plotted 
			 * absolute values here as a wildly low frequency was equally bad 
			 * to a wildly high one. But I still need to have a cut-off 
			 * threshold, under which any varience would be deemed "plausible".
			 * 
			 * I first started with 1, but that let too much stuff through that
			 * was gobbledy gook. So I started lowering my threshold. But that 
			 * took long. So I decided to just let statistics tell me. I let 
			 * the app run without caring about the "plausibility". I just 
			 * tracked total variance. Here are the stats:
			 * 
			 *      avg: 0.79
			 *      min: 0.24
			 *      10%ile: 0.46
			 * 
			 * So I just set my threshold to 0.25. Turns out the answer was the
			 * only one with a total variance below 0.25.
			 * 
			 * Bob is, indeed, your uncle.
			 * 
			 * */


			expectedLetterFrequencies = new Dictionary<char, double>()
			{
				// https://en.wikipedia.org/wiki/Letter_frequency
				{ 'A', 0.082 },
				{ 'B', 0.015 },
				{ 'C', 0.028 },
				{ 'D', 0.043 },
				{ 'E', 0.13 },
				{ 'F', 0.022 },
				{ 'G', 0.02 },
				{ 'H', 0.061 },
				{ 'I', 0.07 },
				{ 'J', 0.002 },
				{ 'K', 0.008 },
				{ 'L', 0.04 },
				{ 'M', 0.024 },
				{ 'N', 0.067 },
				{ 'O', 0.075 },
				{ 'P', 0.019 },
				{ 'Q', 0.001 },
				{ 'R', 0.06 },
				{ 'S', 0.063 },
				{ 'T', 0.091 },
				{ 'U', 0.028 },
				{ 'V', 0.01 },
				{ 'W', 0.024 },
				{ 'X', 0.002 },
				{ 'Y', 0.02 },
				{ 'Z', 0.001 },
			};
			const string filePath = @"E:\ProjectEuler\ExternalFiles\p059_cipher.txt";
			string fileContents = File.ReadAllText(filePath);
			string[] stringsOfNums = fileContents.Split(",");
            int[] inputInts = new int[stringsOfNums.Length];
			for(int i = 0; i < stringsOfNums.Length; i++)
            {
				inputInts[i] = int.Parse(stringsOfNums[i]);
            }

			for (int char1 = ASCIIHelper.lowerA; char1 < ASCIIHelper.lowerZ; char1++)
			{
				for (int char2 = ASCIIHelper.lowerA; char2 < ASCIIHelper.lowerZ; char2++)
				{
					for (int char3 = ASCIIHelper.lowerA; char3 < ASCIIHelper.lowerZ; char3++)
					{
                        int[] passwordAttempt = new int[] { char1, char2, char3 };

						// decrypt using the passkey and check for letter distribution
						Dictionary<char, int> letterCounts = new Dictionary<char, int>();
						string message = "";
						bool isStillLegal = true;

						int answer = 0;

						for (int i = 0; isStillLegal && i < inputInts.Length; i++)
						{
							var inputInt = inputInts[i];
							var passwordInt = passwordAttempt[i % 3];
							var intXor = inputInt ^ passwordInt;

							answer += intXor;

							// if the XOR operation resulted in an unprintable
							// char, move on to the next password
							if (intXor < 32 || intXor > 126)
                            {
								isStillLegal = false;
								continue;
                            }

							char decryptedChar = ASCIIHelper.asciiPrintable[intXor];
							message += decryptedChar;

							char charToCount = (char)0;
							// add it to the letter counts if it's a letter
							if(intXor >= ASCIIHelper.lowerA && intXor <= ASCIIHelper.lowerZ)
                            {
								charToCount = ASCIIHelper.asciiPrintable[intXor - 32];
                            }
							else if (intXor >= ASCIIHelper.upperA && intXor <= ASCIIHelper.upperZ)
                            {
								charToCount = decryptedChar;
                            }
							if(charToCount != 0)
                            {
								if (letterCounts.ContainsKey(charToCount)) letterCounts[charToCount]++;
								else letterCounts.Add(charToCount, 1);

                            }
						}
						if (isStillLegal)
						{
							// message decryption attempt complete; see if we have a plausible
							// distribution of letters
							if (IsLetterFrequencyPlausible(letterCounts))
							{
								// winner, winner
#if VERBOSEOUTPUT
								Console.WriteLine(message);
#endif
								PrintSolution(answer.ToString());
								return;
							}
						}
					}
				}
			}
#if VERBOSEOUTPUT
			double avgV = variances.Average();
			double minV = variances.Min();
			List<double> sortedV = variances.OrderBy(x => x).ToList();
			double v10Percentile = sortedV[(int)Math.Round(variances.Count * 0.1, 0)];
#endif
			
			
		}
		private bool IsLetterFrequencyPlausible(Dictionary<char, int> letterCounts)
        {
			const double threshold = 0.25;
#if VERBOSEOUTPUT
			if (variances == null) variances = new List<double>();
#endif
			double totalLetters = letterCounts.Sum(x => x.Value);
			double totalVariance = 0d;
			foreach(KeyValuePair<char, int> lc in letterCounts)
            {
				double expected = expectedLetterFrequencies[lc.Key];
				double actual = lc.Value / totalLetters;
				totalVariance += Math.Abs(actual - expected);
            }
#if VERBOSEOUTPUT
			variances.Add(totalVariance);
#endif
			if (totalVariance <= threshold)
			{
				return true;
			}
			return false;
        }
	}
}
