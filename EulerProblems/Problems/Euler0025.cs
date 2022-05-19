using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
	internal class Euler0025 : Euler
	{
		public Euler0025() : base()
		{
			title = "1000-digit Fibonacci number";
			problemNumber = 25;
			PrintTitle();
		}
		public override void Run()
		{
			for(int i = 0; i < problemNumber; i++)
            {
				Console.WriteLine(WeirdAlgorithms.GetFinoacciSequenceValueAtPositionN(i));

			}
			string answer = string.Empty;
			

			PrintSolution(answer.ToString());
			return;
		}
		

	}
}
