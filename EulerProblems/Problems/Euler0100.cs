using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0100 : Euler
	{
		public Euler0100() : base()
		{
			title = "Template";
			problemNumber = 100;
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
