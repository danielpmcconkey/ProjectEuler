//#define VERBOSEOUTPUT
using System.Diagnostics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0092 : Euler
	{
		public Euler0092() : base()
		{
			title = "Square digit chains";
			problemNumber = 92;
		}
		protected override void Run()
		{
            /*
             * This was another simple solve, but I was quite proud of how I 
             * optimized it. The recurrsive getChainResult function combined 
             * with the memo really make it fly, going through all 10MM in just
             * barely over a second. 
             * 
             * Funilly enough, this showed me that my old ConvertIntToIntArray
             * function that I was so fond of was slow. The old function went
             * through powers of 10 and "stripped" away the remainder. I was 
             * certain that keeping everything in the domain of numbers was the
             * right way to go. I was wrong. A straight 
             * ToString().ToCharArray() with a little ASCII math turns out to 
             * be about 10x faster. 
             * 
             * I did a quick regression test to ensure I didn't screw 
             * everything up and whammo. Fastness.
             * 
             * */

            const int limit = 10000000;
            int[] squares = new int[] { 0, 1, 4, 9, 16, 25, 36, 49, 64, 81 };
            int[] memo = new int[limit];
            memo[1] = 1;
            memo[89] = 89;
            Func<int, int> squareDigits = (n) =>
            {
                var nums = CommonAlgorithms.ConvertIntToIntArray(n);
                int total = 0;
                for(int i = 0; i < nums.Length; i++)
                {
                    total += squares[nums[i]];
                }
                return total;
            };
            Func<int, int> getChainResult = null;
            getChainResult = (n) =>
            {
                if (memo[n] > 0) return memo[n];
                var thisLink = squareDigits(n);
                var result = getChainResult(thisLink);
                memo[n] = result;
                return result;
            };

            int answer = 0;
            for (int n = 1; n < limit; n++)
            {
                if(getChainResult(n) == 89) answer++;
            }
			PrintSolution(answer.ToString());
			return;
		}
    }
}
