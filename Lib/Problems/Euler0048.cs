//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0048 : Euler
	{
		public Euler0048() : base()
		{
			title = "Self powers";
			problemNumber = 48;
		}
		protected override void Run()
		{
			int limit = 1000;
			BigNumber sum = new BigNumber(0);

			for(int i = 1; i <= limit; i++)
            {
				BigNumber exponentResult = BigNumberCalculator.Exponent(i, i);
				sum = BigNumberCalculator.Add(sum, exponentResult);
            }
			
			PrintSolution(sum.ToString());
			return;
		}
	}
}
