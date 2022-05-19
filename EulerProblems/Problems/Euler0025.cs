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
			Run_bruteForce();
		}
		private void Run_bruteForce()
        {
			BigNumber penultimate = new BigNumber(0);
			BigNumber ultimate = new BigNumber(1);
			int position = 2;
			while(true)
            {
				BigNumber currentAnswer = BigNumberCalculator.Add(penultimate, ultimate);
				if(currentAnswer.digits.Length > 999)
                {
					PrintSolution(position.ToString());
					return;
				}
				// Console.WriteLine(string.Format("{0}{1}", position.ToString().PadRight(10), currentAnswer.ToString()));
				penultimate = ultimate;
				ultimate = currentAnswer;
				position++;
            }			
		}
		

	}
}
