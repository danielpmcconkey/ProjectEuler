//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	/*
	 * Man, it feels good to get back to good old fashioned programming without 
	 * needing to read through math I don't understand with the hope of someone
	 * just telling me the steps to solve a problem. I don't know. Maybe there is
	 * some math-y way to solve this, but I didn't need it here.
	 * 
	 * Right away, I saw that this was an exercise in testing all the 
	 * permutations of 1 - 10. Well, except that I didn't need to test *all* of 
	 * them. The problem very explicitly tells you that there are 17-digit 
	 * strings and 16-digit strings that make "magic" 5-gon rings and that they
	 * only want the max 16-digit string. What this means is that there can only
	 * be 1 "10" in the string. And the only way to do that was to make sure that
	 * a 10 was an outer node. All the inner nodes are used twice.
	 * 
	 * Even better, since the problem statement requires you to start your string
	 * at the lowest outer node, it didn't really matter where the 10 is in 
	 * relation to the other outer nodes. So I chose to make 10 the first digit
	 * of all of my input sequences. This meant that I only had to test the 
	 * permutations of 1 - 9 and stick a 10 in front of each.
	 * 
	 * Now I just had to map a 5-gon ring to the index positions in my input 
	 * array. This was totally arbitrary. I could've put these in any order as 
	 * long as the 10 (at index zero) was one of the outer nodes. Below you'll
	 * see the map I chose.
	 *               
	 *         0                      3   
	 *              \                / 
	 *                  1           /
	 *              /         \    /  
	 *          8                 2   
	 *       /   \              /   
	 *   9        \            /    
	 *  	       \          /    
	 *  	       6---------4---------5
	 *  	       \        
	 *  	        \        
	 *  	         \        
	 *  	          7   
	 * 
	 * Having this map in place, it was super easy to test a permutation by 
	 * summing the values in each node set and ensuring they are the same. if
	 * yes, then go about the easy-but-time-consuming task of putting them into
	 * one giant concatenated int64. Returning that int64, all I had to do was
	 * keep track of the largest of those and I was in Yay city.
	 * 
	 * Population: me.
	 * 
	 * */

	public struct MagicRing
	{


		public int[] digits;
		private int[] node0set { get { return new int[] { digits[0], digits[1], digits[2] }; } }
		private int[] node3set { get { return new int[] { digits[3], digits[2], digits[4] }; } }
		private int[] node5set { get { return new int[] { digits[5], digits[4], digits[6] }; } }
		private int[] node7set { get { return new int[] { digits[7], digits[6], digits[8] }; } }
		private int[] node9set { get { return new int[] { digits[9], digits[8], digits[1] }; } }

		public MagicRing(int[] digits)
		{
			this.digits = digits;
		}
		public bool IsValid()
		{
			int node0Sum = node0set.Sum();
			if (node3set.Sum() != node0Sum) return false;
			if (node5set.Sum() != node0Sum) return false;
			if (node7set.Sum() != node0Sum) return false;
			if (node9set.Sum() != node0Sum) return false;
			return true;
		}
		public long GetConcatenatedDigits()
		{
			int lowestValue = GetLowestNodeValue();
			int lowestNode = WhichNodeIsLowest(lowestValue);
			var concatenatedDigits = GetConcatenatedDigitsArray(lowestNode);
			concatenatedDigits = Replace10InConcatenatedDigits(concatenatedDigits);
			return CommonAlgorithms.ConvertIntArrayToLong(concatenatedDigits);
		}
		private int GetLowestNodeValue()
		{
			// node digits are 0, 3, 5, 7, and 9. but the 0th position is
			// always the 10 so it'll never be lowest
			int currentMin = Math.Min(digits[3], digits[5]);
			currentMin = Math.Min(currentMin, digits[7]);
			return Math.Min(currentMin, digits[9]);
		}
		private int WhichNodeIsLowest(int lowestValue)
		{
			// node digits are 0, 3, 5, 7, and 9. but the 0th position is
			// always the 10 so it'll never be lowest
			if (node3set[0] == lowestValue) return 3;
			if (node5set[0] == lowestValue) return 5;
			if (node7set[0] == lowestValue) return 7;
			if (node9set[0] == lowestValue) return 9;
			throw new Exception("10 is the lowest value?");
		}
		private int[] GetConcatenatedDigitsArray(int lowestNode)
		{
			List<int> concatenatedDigits = new List<int>();

			if (lowestNode == 3)
			{
				concatenatedDigits.AddRange(node3set);
				concatenatedDigits.AddRange(node5set);
				concatenatedDigits.AddRange(node7set);
				concatenatedDigits.AddRange(node9set);
				concatenatedDigits.AddRange(node0set);
			}
			else if (lowestNode == 5)
			{
				concatenatedDigits.AddRange(node5set);
				concatenatedDigits.AddRange(node7set);
				concatenatedDigits.AddRange(node9set);
				concatenatedDigits.AddRange(node0set);
				concatenatedDigits.AddRange(node3set);
			}
			else if (lowestNode == 7)
			{
				concatenatedDigits.AddRange(node7set);
				concatenatedDigits.AddRange(node9set);
				concatenatedDigits.AddRange(node0set);
				concatenatedDigits.AddRange(node3set);
				concatenatedDigits.AddRange(node5set);
			}
			else if (lowestNode == 9)
			{
				concatenatedDigits.AddRange(node9set);
				concatenatedDigits.AddRange(node0set);
				concatenatedDigits.AddRange(node3set);
				concatenatedDigits.AddRange(node5set);
				concatenatedDigits.AddRange(node7set);
			}
			else
			{
				throw new Exception("10 is the lowest value?");
			}
			
			return concatenatedDigits.ToArray();
		}
		private int[] Replace10InConcatenatedDigits(int[] concatenatedDigits)
		{
			// this function is needed because my common algorithms function
			// for turning an int[] into a long only handles single digits. so
			// we need to turn { 1, 2, 10, 4 } into { 1, 2, 1, 0, 4 }

			int length = concatenatedDigits.Length;
			int[] catWith10Handled = new int[length + 1];
			bool hasHit10 = false;
			for (int i = 0; i < length; i++)
			{
				int placeToPutIt = i;
				if (hasHit10) placeToPutIt++;
				var d = concatenatedDigits[i];
				if (d == 10)
				{
					hasHit10 = true;
					catWith10Handled[i] = 1;
					catWith10Handled[i + 1] = 0;
				}
				else
				{
					catWith10Handled[placeToPutIt] = d;
				}
			}
			return catWith10Handled;
		}
	}
	public class Euler0068 : Euler
	{
		public Euler0068() : base()
		{
			title = "Magic 5-gon ring";
			problemNumber = 68;
		}
		protected override void Run()
		{
			long answer = 0;
			
			int[] digits1through9 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			int[][] permutations = CommonAlgorithms.GetAllPermutationsOfArray(digits1through9);
			foreach (var p in permutations)
			{
				// pop 10 to the front
				int[] all10 = new int[10];
				all10[0] = 10;
				for (int i = 0; i < p.Length; i++)
				{
					all10[i + 1] = p[i];
				}
				// create a magic ring and check it for validity
				MagicRing mr = new MagicRing(all10);
				if (mr.IsValid())
				{
					long thisVal = mr.GetConcatenatedDigits();
					answer = Math.Max(answer, thisVal);
				}
			}
			PrintSolution(answer.ToString());
			return;
		}
	}
}
