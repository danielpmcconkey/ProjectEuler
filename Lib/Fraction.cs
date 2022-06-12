//#define VERBOSEOUTPUT
using System.Numerics;

namespace EulerProblems.Lib
{
    public struct Fraction
    {
        public int numerator;
        public int denominator;

        public Fraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }
        public override string ToString()
        {
            return string.Format("{0} over {1}", numerator, denominator);
        }
    }
    public struct BigFraction
    {
        public BigInteger numerator;
        public BigInteger denominator;

        public BigFraction(BigInteger numerator, BigInteger denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }
        public override string ToString()
        {
            return string.Format("{0} over {1}", numerator, denominator);
        }
    }
    public struct ContinuedFraction
    {
        // https://en.wikipedia.org/wiki/Continued_fraction
        public int firstCoefficient;
        public int[] subsequentCoefficients;
        public bool doCoefficientsRepeat;

        public override string ToString()
        {
            string notation = string.Format("[{0}", firstCoefficient);
            for(int i = 0; i < subsequentCoefficients.Length; i++)
            {
                notation += (i == 0) ? ";" : ",";
                notation += subsequentCoefficients[i].ToString();
            }
            return notation;
        }
    }
    public static class FractionCalculator
    { 
        public static Fraction Add(Fraction f1, Fraction f2, bool shouldReduce = true)
        {
            // normalize the denominators
            int normalizedDenominator = f1.denominator * f2.denominator;
            int newF1Numerator = normalizedDenominator / f1.denominator * f1.numerator;
            int newF2Numerator = normalizedDenominator / f2.denominator * f2.numerator;

            Fraction unReducedAddition = new Fraction(
                newF1Numerator + newF2Numerator, 
                normalizedDenominator);

            if(shouldReduce) return Reduce(unReducedAddition);
            return unReducedAddition;
        }
        public static BigFraction Add(BigFraction f1, BigFraction f2, bool shouldReduce = true)
        {
            // normalize the denominators
            BigInteger normalizedDenominator = f1.denominator * f2.denominator;
            BigInteger newF1Numerator = normalizedDenominator / f1.denominator * f1.numerator;
            BigInteger newF2Numerator = normalizedDenominator / f2.denominator * f2.numerator;

            BigFraction unReducedAddition = new BigFraction(
                newF1Numerator + newF2Numerator,
                normalizedDenominator);

            if (shouldReduce) return Reduce(unReducedAddition);
            return unReducedAddition;
        }
        public static BigFraction GetContinuedFractionConvergence(ContinuedFraction f)
        {
            BigFraction currentFraction = new BigFraction(0, 1);
            for (int i = f.subsequentCoefficients.Length - 1; i >= 0; i--)
            {
                int newNumber = f.subsequentCoefficients[i];
                // add the new number and take the reciprocal
                currentFraction = FractionCalculator.Add(
                    currentFraction,
                    new BigFraction(newNumber, 1),
                    false
                    );
                currentFraction = FractionCalculator.Recriprocate(currentFraction, false);

#if VERBOSEOUTPUT
					Console.WriteLine(currentFraction.ToString()); 
#endif
            }
            // finally, add the first coefficient
            // just add the number, don't take the reciprocal
            currentFraction = FractionCalculator.Add(
                currentFraction,
                new BigFraction(f.firstCoefficient, 1),
                false
                );
            return currentFraction;
        }
        public static Fraction Recriprocate(Fraction f, bool shouldReduce = true)
        {
            int numerator = f.denominator;
            int denominator = f.numerator;
            var unReduced = new Fraction(numerator, denominator);
            if(shouldReduce) return Reduce(unReduced);
            return unReduced;
        }
        public static BigFraction Recriprocate(BigFraction f, bool shouldReduce = true)
        {
            BigInteger numerator = f.denominator;
            BigInteger denominator = f.numerator;
            var unReduced = new BigFraction(numerator, denominator);
            if (shouldReduce) return Reduce(unReduced);
            return unReduced;
        }
        public static Fraction Reduce(Fraction f)
        {
            // greatest common factor method
            int[] numeratorFactors = CommonAlgorithms.GetFactors(f.numerator);
            int[] denominatorFactors = CommonAlgorithms.GetFactors(f.denominator);

            int gcf = 0;
            Array.Sort(numeratorFactors);
            for(int i = numeratorFactors.Length - 1; i >= 0; i--)
            {
                if(denominatorFactors.Contains(numeratorFactors[i]))
                {
                    gcf = numeratorFactors[i];
                    break;
                }
            }

            // divide each number by the gcf
            int newNumerator = f.numerator / gcf;
            int newDenominator = f.denominator / gcf;

            return new Fraction(newNumerator, newDenominator);
        }
        public static BigFraction Reduce(BigFraction f)
        {
            // greatest common factor method
            BigInteger[] numeratorFactors = CommonAlgorithms.GetFactors(f.numerator);
            BigInteger[] denominatorFactors = CommonAlgorithms.GetFactors(f.denominator);
            
            BigInteger gcf = 0;
            Array.Sort(numeratorFactors);
            for (int i = numeratorFactors.Length - 1; i >= 0; i--)
            {
                if (denominatorFactors.Contains(numeratorFactors[i]))
                {
                    gcf = numeratorFactors[i];
                    break;
                }
            }

            // divide each number by the gcf
            BigInteger newNumerator = f.numerator / gcf;
            BigInteger newDenominator = f.denominator / gcf;

            return new BigFraction(newNumerator, newDenominator);
        }
    }
}
