//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0100 : Euler
	{
		public Euler0100() : base()
		{
			title = "Template";
			problemNumber = 100;
		}
		protected override void Run()
		{
			int answer = 0;
			PrintSolution(answer.ToString());
			return;
		}
	}
}
