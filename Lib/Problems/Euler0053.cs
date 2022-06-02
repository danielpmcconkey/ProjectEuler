//#define VERBOSEOUTPUT
using System.Numerics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0053 : Euler
	{
		public Euler0053() : base()
		{
			title = "Template";
			problemNumber = 53;
		}
		protected override void Run()
		{
			int min = 1;
			int max = 100;
			BigInteger target = 1000000;
			BigInteger answer = 0;
			for (int n = min; n <= max; n++)
            {
				for(int r = 1; r <= n; r++)
                {
					BigInteger combinatrics = CommonAlgorithms.GetCombinatoricRFromN(n, r);
					if (combinatrics > target)
					{
						answer++;
					}
				}
            }
			
			PrintSolution(answer.ToString());
			return;
		}
	}
}
