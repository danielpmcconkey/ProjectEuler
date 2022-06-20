//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0074 : Euler
	{
		public Euler0074() : base()
		{
			title = "Digit factorial chains";
			problemNumber = 74;
		}
		protected override void Run()
		{
			/*
			 * This one wasn't too tough for me. I more or less thought up the 
			 * below solution right away. I knew that I wanted to create a 
			 * cache of the chain length so that, once you got to a known 
			 * number, you could stop. From there, it was just a matter of 
			 * implementation. 
			 * 
			 * I decided to take this opportunity to expand my horizons. I've 
			 * been trying to learn F# lately and functional programming 
			 * techniques are rather appealing to me. Part of this learning
			 * journey has also been understanding C#'s FP support and, so I
			 * decided to build this as one function that composes a couple of
			 * lambda functions. This one probably won't be that hard to turn
			 * into F#.
			 * 
			 * There were a couple of "gotchas". 1 and 2 caused a stack 
			 * overflow exception right away. This is because my 
			 * howManyNonRepeaters function is recursive and it just kept going 
			 * forever. So my first reaction was to just start at 3. I didn't 
			 * even think to add them to my cache because the sum of factors 
			 * for any numbers not 1 or 2 would never resolve to 1 or 2. So I 
			 * could just forget about 1 or 2 for the remainder of this 
			 * problem.
			 * 
			 * Unfortunately, I needed to. The problem statement told me why. 
			 * 145 is a number that resolves to itself when you sum the 
			 * factors. This would also have caused the same stack overflow had
			 * I not pre-seeded my repeaters dictionary with the numbers in the 
			 * problem statement. What about 40585? 
			 * 
			 * That one also resolves to itself. How'd I know? A stack overflow
			 * exception somewhere between 10,000 and 20,000. And debugging a 
			 * stack overflow is tough. I tried setting up a conditional break
			 * point that looked for:
			 * 
			 *      (new StackTrace()).GetFrames().Length > 60
			 *      
			 * But slowed processing down so slow. I basically had to move my i
			 * from the main iteration loop to a global scope and set an if 
			 * statement up that first checked if i were in range x to y before
			 * then counting my stacktrace count. It was un-fun. 
			 * 
			 * Once I finished frolicking through my stack trace debuggary, I 
			 * added 40585 to my pre-seeded dictionary and got my answer. After
			 * submitting it to Euler, I decided it was cheating and quickly 
			 * ran a loop to find all numbers that did this. Turns out it's 
			 * just 1, 2, 145, and 40585. Huh.
			 * 
			 * Double embarrassment points when someone in the problem thread 
			 * pointed out that problem 34 was exactly about finding such fun
			 * numbers.
			 * 
			 * */

			int[] factorials = new int[] { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };
			// set up a dictionary of repeaters. Once you hit one of these
			// terms, you know how many non-repeating terms you'll wind up with
			Dictionary<int, int> repeaters = new Dictionary<int, int>();
			repeaters.Add(
				145,
				1   // how many repeats assigned to 145
				);
			repeaters.Add(169, 3);
			repeaters.Add(363601, 3);
			repeaters.Add(1454, 3);
			repeaters.Add(871, 2);
			repeaters.Add(45361, 2);
			repeaters.Add(872, 2);
			repeaters.Add(45362, 2);

			Func<int, int> sumOfDigitFactorials = (n) =>
			{

				var digits = CommonAlgorithms.ConvertIntToIntArray(n);
				var aggregate = digits.Aggregate(0, (total, next) => total + factorials[next]);
				return aggregate;
			};

			// recursive lambda for calculating how many non-repeaters from a given starting pos
			Func<int, int, List<int>, (int, List<int>)> howManyNonRepeaters = null;
			howManyNonRepeaters = (n, countSoFar, newRepeaters) =>
			{
				if (repeaters.ContainsKey(n))
				{
					return (countSoFar + repeaters[n], newRepeaters);
				}
				newRepeaters.Add(n);
				return howManyNonRepeaters(sumOfDigitFactorials(n), countSoFar + 1, newRepeaters);
			};

            int limit = 1000000;
			int start = 1;

			// add any special cases of numbers that are their own sum of factorials,
			// like 145. any number like this will send the howManyNonRepeaters
			// function into a stake overflow as we recurse forever. 
			for (int i = start; i < limit; i++)
			{
				if (repeaters.ContainsKey(i) == false && sumOfDigitFactorials(i) == i) 
					repeaters.Add(i, 1);
			}

			for (int i = start; i < limit; i++)
			{
				if (repeaters.ContainsKey(i)) continue;				

				var nonRepeaters = howManyNonRepeaters(i, 0, new List<int>());
				int numRepeaters = nonRepeaters.Item1;
				var newRepeaters = nonRepeaters.Item2;
				for (int j = 0; j < newRepeaters.Count; j++)
				{
					var r = newRepeaters[j];
					int howManyAtR = numRepeaters - j;
					if (!repeaters.ContainsKey(r)) repeaters.Add(r, howManyAtR);
				}
			}
			var answer = repeaters
				.Where(y => y.Value == 60)
				.Count();
			PrintSolution(answer.ToString());
			return;
		}
	}
}
