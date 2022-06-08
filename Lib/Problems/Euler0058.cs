//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0058 : Euler
	{
		public Euler0058() : base()
		{
			title = "Spiral primes";
			problemNumber = 58;
		}
		protected override void Run()
		{
			double primesCount = 0;
			int totalCount = 1; // the center "1" piece is counted
			for(int sideLength = 3; true; sideLength += 2)
            {
				int sideLengthMinus1 = sideLength - 1;
				int lowerRight = sideLength * sideLength;
				int lowerLeft = lowerRight - sideLengthMinus1;
				int upperLeft = lowerLeft - sideLengthMinus1;
				int upperRight = upperLeft - sideLengthMinus1;

				totalCount += 4;
				// lowerRight will never be prime
				if(CommonAlgorithms.IsPrime(upperRight)) primesCount++;
				if (CommonAlgorithms.IsPrime(lowerLeft)) primesCount++;
				if (CommonAlgorithms.IsPrime(upperLeft)) primesCount++;

#if VERBOSEOUTPUT
                Console.WriteLine("{0}	{1}	{2}	{3}", lowerRight, lowerLeft, upperLeft, upperRight); 
#endif

				if (primesCount / totalCount < 0.1d)
                {
					PrintSolution(sideLength.ToString());
					return;
				}
			}
			
		}
	}
}
