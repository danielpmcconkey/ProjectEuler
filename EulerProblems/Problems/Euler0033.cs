using EulerProblems.Lib;
using System.Text;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
	internal class Euler0033 : Euler
	{
		public Euler0033() : base()
		{
			title = "Digit cancelling fractions";
			problemNumber = 33;
			PrintTitle();
		}
		public override void Run()
		{
			List<Fraction> fractions = new List<Fraction>();

			for(int numerator = 10; numerator <= 99; numerator++)
			{ 
				for (int denominator = 10; denominator <= 99; denominator++)
                {
					if(numerator >= denominator)
                    {
						// not less than 1
						continue;
                    }
					if(numerator % 10 == 0 && denominator % 10 == 0)
                    {
						// trivial
						continue;
                    }
					// check to see if the two share a digit
					int numeratorTens = (int)Math.Floor(numerator / (decimal)10);
					int denominatorTens = (int)Math.Floor(denominator / (decimal)10);
					int numeratorOnes = numerator % 10;
					int denominatorOnes = denominator % 10;
					if(numeratorTens == denominatorOnes && denominatorTens != 0)
                    {
						decimal valueBeforeSplitting = ((decimal)numerator / (decimal)denominator);
						if (valueBeforeSplitting == ((decimal)numeratorOnes / (decimal)denominatorTens))
						{
							// we gots one
							fractions.Add(new Fraction(numeratorOnes, denominatorTens));
						}
					}
					else if (numeratorOnes == denominatorTens && denominatorOnes != 0)
					{
						decimal valueBeforeSplitting = ((decimal)numerator / (decimal)denominator);
						if (valueBeforeSplitting == ((decimal)numeratorTens / (decimal)denominatorOnes))
						{
							// we gots one
							fractions.Add(new Fraction(numeratorTens, denominatorOnes));
						}
					}
				}
            }
			int productNumerator = 1;
			int productDenominator = 1;
			foreach(var fraction in fractions)
            {
				productNumerator *= fraction.numerator;
				productDenominator *= fraction.denominator;
			}
			Fraction product = new Fraction(productNumerator, productDenominator);
			product.Reduce();
			PrintSolution(product.denominator.ToString());
			return;
		}
		
		
		
	}
}
