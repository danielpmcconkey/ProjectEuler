using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0047 : Euler
	{
		public Euler0047() : base()
		{
			title = "Distinct primes factors";
			problemNumber = 47;
			PrintTitle();
		}
		public override void Run()
		{
			int[] primes = CommonAlgorithms.GetPrimesUpToN(100000);
			int startingNumber = 646;
			int[] factorsOfI = new int[0];
			int[] factorsOfIMinus1 = CommonAlgorithms.GetFactors(startingNumber - 1);
			int[] factorsOfIMinus2 = CommonAlgorithms.GetFactors(startingNumber - 2);
			int[] factorsOfIMinus3 = CommonAlgorithms.GetFactors(startingNumber - 3);

			for (int i = startingNumber; true; i++)
            {
				if (i > int.MaxValue) throw new OverflowException();

				// update factors of i
				factorsOfI = CommonAlgorithms.GetFactors(i);


				// move the factors arrays down
				factorsOfIMinus3 = factorsOfIMinus2;
				factorsOfIMinus2 = factorsOfIMinus1;
				factorsOfIMinus1 = factorsOfI;
			}
			int answer = 0;
			PrintSolution(answer.ToString());
			return;
		}
	}
}
