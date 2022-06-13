//#define VERBOSEOUTPUT
using System.Numerics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0066 : Euler
	{
		public Euler0066() : base()
		{
			title = "Diophantine equation";
			problemNumber = 66;
		}
		protected override void Run()
		{
			/*
			 * I don't know how I feel about this one. On one hand, I'm quite 
			 * proud of finding the solution. One the other hand, though, I 
			 * feel like we've definitely entered the realm of esoteric 
			 * mathematics. And, on the gripping hand, the instructions were 
			 * not well worded. I found the right maximum X in D <= 1000 and 
			 * tried it repeatedly until I decided to try the D value. I think
			 * I should have read more clearly, but I still wish they made it
			 * more clear.
			 * 
			 * So how'd I do it? Well, I first tried googling "quadratic 
			 * Diophantine equations" and found that they're a family of 
			 * equations. This searching lasted about 30 minutes before I 
			 * googled "x2 – Dy2 = 1" and came across references to "Pell's 
			 * Equation".
			 * 
			 * https://en.wikipedia.org/wiki/Pell%27s_equation#Fundamental_solution_via_continued_fractions
			 * 
			 * I was really worried. The math was a little thick. But somewhere 
			 * in the middle of the page 
			 * (https://en.wikipedia.org/wiki/Pell%27s_equation#Example) it 
			 * shows you how to find likely candidates for a given D by trying
			 * subsequent convergents of the square root of D. Fortunately, I'd
			 * just done the prior 2 problems, so finding a continued fraction 
			 * for the square root of a number and then finding the convergent 
			 * fraction from that were easy combinations of those 2 problems.
			 * 
			 * */
			int limit = 1000;
			BigInteger maxMinX = 0;
			int dAtMaxX = 0;
			for (int D = 2; D <= limit; D++)
			{
				if (CommonAlgorithms.IsPerfectSquare(D)) continue;
				// find the convergents of the square root of D
				var minX = GetFundamentalX(D);
				if (minX > maxMinX)
				{
					maxMinX = minX;
					dAtMaxX = D;
#if VERBOSEOUTPUT
					Console.WriteLine("new max! {0}:{1}", D, minX);
#endif
				}
			}

			PrintSolution(dAtMaxX.ToString());
			return;
		}
		private BigInteger GetFundamentalX(int D, int loop = 0)
        {
			ContinuedFraction cf = CommonAlgorithms.GetContinuedFractionOfSquareRootOfN(D);
			int repeatLength = cf.subsequentCoefficients.Length;
			if(loop > 0)
            {
				List<int> newCoefficients = cf.subsequentCoefficients.ToList();
				for (int l = 0; l < loop; l++)
                {
					newCoefficients.AddRange(cf.subsequentCoefficients.ToList());
                }
				cf.subsequentCoefficients = newCoefficients.ToArray();
            }
			// check all coefficients in succession to see if any of their
			// convergent fractions solve the equation
			for(int i = (loop * repeatLength); 
				i <= cf.subsequentCoefficients.Length;
				i++)
            {
				ContinuedFraction cf_i = new ContinuedFraction()
				{
					firstCoefficient = cf.firstCoefficient,
					subsequentCoefficients = cf.subsequentCoefficients[0..i]
				};
				var bigF_i = FractionCalculator.GetContinuedFractionConvergence(cf_i);
				BigInteger x = bigF_i.numerator;
				BigInteger y = bigF_i.denominator;
				if (DoesXYDResolve(x, y, D)) return x;
            }
			// oh snap, you ain't found nuttin, hunny!
			// do it again
			if (loop < 20) 
			return GetFundamentalX(D, loop + 1);
			// too many loops. something's wrong
			throw new Exception("Nuttin' found, hunny");
		}
		private bool DoesXYDResolve(BigInteger x, BigInteger y, BigInteger D)
        {
			BigInteger solution = (x * x) - (D * y * y);
			if (solution == 1) return true;
			return false;
        }
		
	}
}
