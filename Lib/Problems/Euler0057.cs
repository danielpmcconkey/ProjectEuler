//#define VERBOSEOUTPUT
using System.Numerics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0057 : Euler
	{
		public Euler0057() : base()
		{
			title = "Square root convergents";
			problemNumber = 57;
		}
		protected override void Run()
		{
			/*
			 * first off, a more legible representation of Sqrt2 represented as a 
			 * contiued fraction can be found on Wikipedia.
			 * 
			 * https://en.wikipedia.org/wiki/Square_root_of_2#Continued_fraction
			 * 
			 * I actually solved this in excel first. Again. For some reason, I 
			 * struggled getting my head around a brute force method of adding up the 
			 * various fractions. I tried it by long hand and I tried doing 
			 * *that* in excel.
			 * 
			 * So I fell back on the same way I solved problem 15. By laying out 
			 * the given expansions' numerators and denominators and trying to 
			 * find a relation. I expected that Sqrt2 had to do with it.
			 * 
			 * When I did this, I noticed that dividing one expansion's denominator
			 * by the prior expansion's denominator, I got a value that approached 
			 * 1 + Sqrt2. So I multiplied a known denominator by 1 + Sqrt2 and 
			 * rounded to the nearest int, I got exactly the next denominator. 
			 * Cool. From there, it wasn't hard to spot that the numerators had
			 * the same relationship.
			 * 
			 * Unfortunately, these numbers got big quick and Excel crapped out at 
			 * expansion 806, when the denominator moves from 308 digits to 309. 
			 * But that was enough for me to count all the places where the 
			 * numerator went up an order of magnitude before the denominator.
			 * 
			 * What I observed is that, for every 100 expansions where the numerator
			 * changed OOM was either 38 or 39 (average 38.375) and the number of
			 * times the numerator went up in OOM before the denominator did was
			 * either 15 or 16 (average) 15.25. So I knew my answer would be
			 * 10 * 15.25 plus or minus 1.
			 * 
			 * That worked. 
			 * 
			 * Now I just needed to take the excel method described above and 
			 * recreate in C#
			 * 
			 * */
			const double sqrt2 = 1.414213562; // close enough
			int limit = 1000;
			long priorDenominator = 985; // expansion 8 = 1393 over 985 as given in the problem statement
			int answer = 1; // expansion 8 was given as the first expansion with numerator.len > denominator.len
			
			// longs can only take you up through the 49th expansion, after which, the
			// denominator becomes bigger than 20 digits long. However, it's important
			// to use longs in the beginning so that rounding doesn't throw off the
			// calculations
			for(int i = 9; i <= 49; i++)
            {
				// the next denominator goes up by priordenominator * (1 + sqrt2)
				long newDenominator = (long)Math.Round(priorDenominator * (1 + sqrt2), 0);
				// numerator is always sqrt2 * the denominator
				long newNumerator = (long)Math.Round(newDenominator * sqrt2, 0);
				if (newNumerator.ToString().Length > newDenominator.ToString().Length)
				{
					answer++;
				}
				priorDenominator = newDenominator;
            }
			// from here on out, we have to use BigIntegers. But the BigInteger
			// multiplication doesn't support doubles as input. So we multiply
			// our sqrt2 variable by 100000 and will later divide it out. If we
			// don't do this, then sqrt2 will always round to 1 and that's a
			// huge difference. We also do the same with 1 + sqrt2
			double sqrt2plus1 = sqrt2 + 1;
			int sqrt2plus1x100000 = (int)Math.Round(sqrt2plus1 * 100000, 0);
			int sqrt2x100000 = (int)Math.Round(sqrt2 * 100000, 0);
			BigInteger priorDenominatorXL = new BigInteger(priorDenominator);
			for (int i = 50; i <= limit; i++)
            {
				BigInteger newDenominatorXL = priorDenominatorXL * sqrt2plus1x100000 / 100000;
				// numerator is always sqrt2 * the denominator
				BigInteger newNumeratorXL = newDenominatorXL * sqrt2x100000 / 100000;
				if(newNumeratorXL.ToString().Length > newDenominatorXL.ToString().Length)
                {
					answer++;
                }
				priorDenominatorXL = newDenominatorXL;
			}


			PrintSolution(answer.ToString());
			return;
		}
	}
}
