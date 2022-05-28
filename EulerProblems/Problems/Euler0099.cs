using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0099 : Euler
	{
		public Euler0099() : base()
		{
			title = "Template";
			problemNumber = 99;
			PrintTitle();
		}
		public override void Run()
		{
			int answer = 0;
			PrintSolution(answer.ToString());
			return;
		}
	}
}
