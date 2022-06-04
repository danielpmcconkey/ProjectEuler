using EulerProblems.Lib;
using System.Text.RegularExpressions;
using System.Numerics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0029 : Euler
	{


		public Euler0029() : base()
		{
			title = "Distinct powers";
			problemNumber = 29;
			
		}
		protected override void Run()
		{
			const int limit = 100;
			
			List<BigInteger> outputs = new List<BigInteger>();
			for (int a = 2; a <= limit; a++)
			{
				for (int b = 2; b <= limit; b++)
				{
					BigInteger n = BigInteger.Pow(a, b);
					if(!outputs.Contains(n)) outputs.Add(n);
				}
			}
			int answer = outputs.Count();
			PrintSolution(answer.ToString());
			return;
		}



	}
}
