using EulerProblems.Lib;
using System.Numerics;
using System.Text.RegularExpressions;

namespace EulerProblems.Lib.Problems
{
	public class Euler0026 : Euler
	{
		public Euler0026() : base()
		{
			title = "Reciprocal cycles";
			problemNumber = 26;
			
		}
		protected override void Run()
		{
			int maximumRepeat = 0;
			int answer = 0;
			int limit = 1000;
			for(int i = 2; i <= limit; i++)
            {
				int localAnswer = unitFractionDivisor(i);
				if (localAnswer > maximumRepeat)
				{
					answer = i;
					maximumRepeat = localAnswer;
				}
			}
			
			PrintSolution(answer.ToString());
		}
		private int unitFractionDivisor(int n)
        {
			if (n == 1) return (0);

			/*
			 * let n = 11
			 * 
			 *             0.09
			 *           ___________
			 *      11   | 1.0000000
			 *             1 00 
			 *               99
			 *            -----
			 *                1 <-- you've identified a repeat because 1 here is the same as when you started
			 *             
			 *  */
			

			int currentTrialNumerator = 10;	// 100
			string longDivisionAnswer = "0.";
			List<int> trialNumerators = new List<int>();
			trialNumerators.Add(currentTrialNumerator);

			while (true)
            {
				// go until you either have a clean answer or have identified a repetition
				if (n > currentTrialNumerator)
				{
					currentTrialNumerator *= 10;
					longDivisionAnswer += "0";
					trialNumerators.Add(currentTrialNumerator);
				}
				if (n == currentTrialNumerator)
				{
#if DEBUG
                    Console.WriteLine(string.Format("{0}{1}{2}", n.ToString().PadRight(10), longDivisionAnswer.PadRight(10), "          ")); 
#endif
                    return 0;
				}
				if (n < currentTrialNumerator)
                {
					// how many times will n go into currentTrialDenominator
					int numberOfTimes = currentTrialNumerator / n;
					longDivisionAnswer += numberOfTimes.ToString();
					int product = n * numberOfTimes;
					int remainder = currentTrialNumerator - product;
					if (remainder == 0)
					{
#if DEBUG
                        Console.WriteLine(string.Format("{0}{1}{2}", n.ToString().PadRight(10), longDivisionAnswer.PadRight(10), "          ")); 
#endif
                        return 0;   // even division, no repeat
					}
					currentTrialNumerator = remainder * 10;
					// is this new new trial numerator already in the list? if so, we've found our repeat
					if (trialNumerators.Contains(currentTrialNumerator))
					{
						// we have a repeat
						// find the place where it was in the stack (from the right)
						// and that's how large our repeater is
						int lastPosition = trialNumerators.IndexOf(currentTrialNumerator);
						int answer = trialNumerators.Count - lastPosition;
#if DEBUG
                        Console.WriteLine(string.Format("{0}{1}{2}", n.ToString().PadRight(10),
                                            longDivisionAnswer.PadRight(10),
                                            answer.ToString().PadRight(10)
                                            )); 
#endif

                        return answer;
                    }
					trialNumerators.Add(currentTrialNumerator);
				}

			}
				
        }
	}
}
