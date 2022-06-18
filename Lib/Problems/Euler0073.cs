//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0073 : Euler
	{
		public Euler0073() : base()
		{
			title = "Counting fractions in a range";
			problemNumber = 73;
		}
		protected override void Run()
		{
			/*
			 * After the last problem, this one wasn't very hard to figure out.
			 * Instead, this problem's challenge was optimization. I don't 
			 * remember the timings involved, but I don't think it took over a 
			 * minute for the first run. At least, I never bothered to create
			 * a separate version of the Run function to compare different 
			 * approaches. But still, it ran too slowly for my tastes.
			 * 
			 * My initial big sticking point was in the method I used to see if
			 * a fraction could reduce. Unlike the prior problem, it was 
			 * absolutely required here. And my version calculated factors for
			 * each numerator and denominator as it went. Very slow.
			 * 
			 * And then I found the Euclidean algorithm for calculating the 
			 * greatest common factor between two integers. Implementing that 
			 * got me down to around 3 seconds. But I decided to re-use problem
			 * 72's mechanism of calculating prime factors up front. I did so 
			 * and passed them to a new "can fraction be reduced" method for 
			 * the comparison of factors.
			 * 
			 * Lastly, I'd been maintaining a HashSet of fractions that had 
			 * already been counted and checking against it each time to ensure
			 * I wasn't double adding any fraction. I realized this was 
			 * redundant, since I had to ensure it was reduced anyway. So I 
			 * took that out.
			 * 
			 * Run time at d <= 12000 is slightly over a second. In fact here 
			 * are the run times at different dMax values:
			 * 
			 *		  dMax		seconds
			 *		-------------------
			 *		 10000		 0.687
			 *		 12000		 1.052
			 *		 20000		 2.58
			 *		 30000		 5.378
			 *		 40000		 9.355
			 *		100000		57.937
			 *	
			 *	It's a pretty linear growth for the number of milliseconds it 
			 *	takes for each denominator you check. Based on this, it 
			 *	projects to a dMax of 1MM taking 106 minutes. 
			 * 
			 * */
			int answer = 0;
			int dMax = 12000;

			var primeFactors = CommonAlgorithms.GetUniquePrimeFactorsUpToN(dMax);

			Fraction maxFraction = new Fraction(1, 2);
			Fraction minFraction = new Fraction(1, 3);
			
			double maxFractionAsD = maxFraction.numerator / (double)maxFraction.denominator;
			double minFractionAsD = minFraction.numerator / (double)minFraction.denominator;

			for (int d = 2; d <= dMax; d++)
			{
				// what's the closest thing (but under) to min fraction in this denomination?
				double oneOverD = 1 / (double)d;
				int start = (int)Math.Floor(minFractionAsD / oneOverD);

				// what's the closest thing (but over) to 3/7 in this denomination?
				int end = (int)Math.Ceiling(maxFractionAsD / oneOverD);

				for (int n = start + 1; n < end; n++)
				{
					Fraction f = new Fraction(n, d);
					if (
						   FractionCalculator.CanFractionBeReduced(f, primeFactors) == false
						&& f.numerator <= dMax 
						&& f.denominator <= dMax
						&& f.CompareTo(minFraction) > 0 
						&& f.CompareTo(maxFraction) < 0
						)
					{
						answer++;
					}
				}
			}
			
			PrintSolution(answer.ToString());
			return;
		}
	}
}
