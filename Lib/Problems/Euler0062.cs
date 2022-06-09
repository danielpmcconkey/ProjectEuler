//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public struct NumAndSortedDigits
    {
		public long originalNum;
		public string sortedDigits;
	}
	public class Euler0062 : Euler
	{
		public Euler0062() : base()
		{
			title = "Cubic permutations";
			problemNumber = 62;
		}
		protected override void Run()
		{
			/*
			 * I originally tried brute force, but that never finished or even 
			 * gave the right answer when I tried it on smaller numbers. So I
			 * quickly adapted to the below approach. Which is...
			 * 
			 * I didn't want to waste time with creating permutations and 
			 * checking whether they were cube. Instead, I created a list of 
			 * all the perfect cubes I'd ever need. And then I took each one 
			 * and split out their digits into an array and I sorted it.
			 * 
			 * Any permutation of a number would have the same sorted digits. 
			 * Take the example given in the problem. 
			 * 
			 *      41063625
			 *      56623104
			 *      66430125 
			 *      
			 * Sort each by their digits and they all come out to
			 * 
			 *      01234566
			 *      
			 * So, if I did that for every one of the cubes in my list, I would 
			 * find three of them who, when their digits were sorted, resolved 
			 * to 01234566.
			 * 
			 * From there, all I had to do was make my list big enough and 
			 * search for a value that showed up 5 times. From there, I just had
			 * to find which of those represented the smallest original number 
			 * (meaning pre-sorted). That's why I created the 
			 * NumAndSortedDigits struct, so I could keep track of the sorted
			 * digits string and the number that generated it.
			 * 
			 * All that was left was some linq grouping and joining and violin!
			 * 
			 * */

			int target = 5;
			long minValToCube = 345;
			long maxValToCube = 10000;// just a guess 10000 ^ 3 is a really big number
			List<long> cubesOver1MM = new List<long>();
			for (long i = minValToCube; i <= maxValToCube; i++)
			{
				cubesOver1MM.Add(i * i * i);
			}
			NumAndSortedDigits[] digitsSortedArray = new NumAndSortedDigits[cubesOver1MM.Count];
			for(int i = 0; i < cubesOver1MM.Count; i++)
            {
				int[] digits = CommonAlgorithms.ConvertLongToIntArray(cubesOver1MM[i]);
				Array.Sort(digits);
				string sortedDigitsString = string.Concat(digits);
				digitsSortedArray[i] = new NumAndSortedDigits()
				{
					originalNum = cubesOver1MM[i], 
					sortedDigits = sortedDigitsString
				};					
            }
			var groupByCount =
				from digits in digitsSortedArray
				group digits by digits.sortedDigits
				into digitGroups
				select new { sortedDigits = digitGroups.Key, numCubes = digitGroups.Count() };

			var rightNumOfCubes = groupByCount
				.Where(y => y.numCubes == target);

			var joinedSet =
				from r in rightNumOfCubes
				join d in digitsSortedArray on r.sortedDigits equals d.sortedDigits
				orderby d.originalNum
				select new { realNum = d.originalNum };

			long answer = joinedSet.First().realNum;
			PrintSolution(answer.ToString());
			return;		
		}
	}
}
