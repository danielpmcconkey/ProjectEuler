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
			int answer = 0;
			int dMax = 12000;

			Fraction maxFraction = new Fraction(1, 2);
			Fraction minFraction = new Fraction(1, 3);
			
			double maxFractionAsD = maxFraction.numerator / (double)maxFraction.denominator;
			double minFractionAsD = minFraction.numerator / (double)minFraction.denominator;
			HashSet<(long numerator, long denominator)> usedFractions =
				new HashSet<(long numerator, long denominator)>();

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
						   usedFractions.Contains((f.numerator, f.denominator)) == false
						&& CommonAlgorithms.CanFractionBeReduced(f) == false
						&& f.numerator <= dMax 
						&& f.denominator <= dMax
						&& f.CompareTo(minFraction) > 0 
						&& f.CompareTo(maxFraction) < 0
						)
					{
						usedFractions.Add((f.numerator, f.denominator));
						answer++;
					}
				}
			}
			
			PrintSolution(answer.ToString());
			return;
		}
	}
}
