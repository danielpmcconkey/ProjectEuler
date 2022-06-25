//#define VERBOSEOUTPUT
using System.Numerics;

namespace EulerProblems.Lib
{
    public struct Fraction : IComparable<Fraction>
    {
        public int numerator;
        public int denominator;

        public Fraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

		public int CompareTo(Fraction other)
		{
            /*
             * return < 0 if this instance precedes other in a sort
             * return 0 if both should occupy the same position in a sort
             * return > 0 if this instance follows other in a sort
             * 
             *      3     2
             *     --- > ---
             *      7     7
             *      
             *  2/7 should precede 3/7 so this should follow other and 
             *  should return > 0
             *  
             *  */
             
            if (this.denominator == other.denominator)
			{
                return this.numerator - other.numerator;
			}
            // seek to share the same denominator
			var normalizedSet = FractionCalculator.NormalizeFractions(this, other);
            return normalizedSet.f1.numerator - normalizedSet.f2.numerator;
		}

		public override string ToString()
        {
            return string.Format("{0} over {1}", numerator, denominator);
        }
    }
    public struct LongFraction : IComparable<LongFraction>
    {
        public long numerator;
        public long denominator;

        public LongFraction(long numerator, long denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public int CompareTo(LongFraction other)
        {
            /*
             * return < 0 if this instance precedes other in a sort
             * return 0 if both should occupy the same position in a sort
             * return > 0 if this instance follows other in a sort
             * 
             *      3     2
             *     --- > ---
             *      7     7
             *      
             *  2/7 should precede 3/7 so this should follow other and 
             *  should return > 0
             *  
             *  */

            if (this.denominator == other.denominator)
            {
                if (this.numerator > other.numerator) return 1;
                if (this.numerator == other.numerator) return 0;
                if (this.numerator < other.numerator) return -1;
            }
            // seek to share the same denominator
            var normalizedSet = FractionCalculator.NormalizeFractions(this, other);
            if (normalizedSet.f1.numerator > normalizedSet.f2.numerator) return 1;
            if (normalizedSet.f1.numerator == normalizedSet.f2.numerator) return 0;
            return -1;
        }

        public override string ToString()
        {
            return string.Format("{0} over {1}", numerator, denominator);
        }
    }
    public struct BigFraction : IComparable<BigFraction>
    {
        public BigInteger numerator;
        public BigInteger denominator;

        public BigFraction(BigInteger numerator, BigInteger denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }
        public int CompareTo(BigFraction other)
        {
            /*
             * return < 0 if this instance precedes other in a sort
             * return 0 if both should occupy the same position in a sort
             * return > 0 if this instance follows other in a sort
             * 
             *      3     2
             *     --- > ---
             *      7     7
             *      
             *  2/7 should precede 3/7 so this should follow other and 
             *  should return > 0
             *  
             *  */

            if (this.denominator == other.denominator)
            {
                if (this.numerator > other.numerator) return 1;
                if (this.numerator == other.numerator) return 0;
                if (this.numerator < other.numerator) return -1;
            }
            // seek to share the same denominator
            var normalizedSet = FractionCalculator.NormalizeFractions(this, other);
            if (normalizedSet.f1.numerator > normalizedSet.f2.numerator) return 1;
            if (normalizedSet.f1.numerator == normalizedSet.f2.numerator) return 0;
            return -1;

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
            var normalizedSet = NormalizeFractions(f1, f2);


            Fraction unReducedAddition = new Fraction(
                normalizedSet.f1.numerator + normalizedSet.f2.numerator,
                normalizedSet.f1.denominator);

            if (shouldReduce) return Reduce(unReducedAddition);
            return unReducedAddition;
        }
        public static BigFraction Add(BigFraction f1, BigFraction f2, bool shouldReduce = true)
        {
            var normalizedSet = NormalizeFractions(f1, f2);
            

            BigFraction unReducedAddition = new BigFraction(
                normalizedSet.f1.numerator + normalizedSet.f2.numerator,
                normalizedSet.f1.denominator);

            if (shouldReduce) return Reduce(unReducedAddition);
            return unReducedAddition;
        }
        internal static bool CanFractionBeReduced(Fraction f)
        {
            if (f.numerator % 2 == 0 && f.denominator % 2 == 0) return true;
            if (CommonAlgorithms.GetGreatestCommonFactor(f.numerator, f.denominator) == 1) return false;
            return true;
        }
        internal static bool CanFractionBeReduced(Fraction f, List<int>[] primeFactors)
        {
            var numeratorFactors = primeFactors[f.numerator];
            var denominatorFactors = primeFactors[f.denominator];

            for (int i = numeratorFactors.Count - 1; i >= 0; i--)
            {
                var n = numeratorFactors[i];
                if (n != 1 && denominatorFactors.Contains(n))
                {
                    return true;
                }
            }

            return false;
        }
        internal static bool CanFractionBeReduced(LongFraction f)
        {
            if (f.numerator % 2 == 0 && f.denominator % 2 == 0) return true;
            if (CommonAlgorithms.GetGreatestCommonFactor(f.numerator, f.denominator) == 1) return false;
            return true;
        }
        public static Fraction Divide(Fraction f1, Fraction f2, bool shouldReduce = true)
        {
            var inverseF2 = Recriprocate(f2);
            return Multiply(f1, inverseF2, shouldReduce);
        }
        public static LongFraction Divide(LongFraction f1, LongFraction f2, bool shouldReduce = true)
        {
            var inverseF2 = Recriprocate(f2);
            return Multiply(f1, inverseF2, shouldReduce);
        }
        public static BigFraction Divide(BigFraction f1, BigFraction f2, bool shouldReduce = true)
        {
            var inverseF2 = Recriprocate(f2);
            return Multiply(f1, inverseF2, shouldReduce);
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
        /// <summary>
        /// converts a continued fraction to an array of digits representing
        /// its decimal form
        /// </summary>
        /// <param name="cf">the continued fraction to expand</param>
        /// <param name="numDigits">numdigits only matters if repeating</param>
        public static int[] GetContinuedFractionDecimalExpansion(ContinuedFraction cf, int numDigits = 0)
        {
            /* 
             * based on https://crypto.stanford.edu/pbc/notes/contfrac/convert.html 
             * if you can figure that nonsense out
             * */

            // set up a list of coefficients to use for getting the convergent fractions
            int coefficientsLength = (cf.doCoefficientsRepeat) ?
                (int)Math.Round(numDigits * 1.75) : // padding
                cf.subsequentCoefficients.Length;
            int[] coefficientsArray = new int[coefficientsLength];
            for (int i = 0; i < coefficientsLength; i++)
            {
                int position = (cf.doCoefficientsRepeat) ?
                    i % cf.subsequentCoefficients.Length :
                    i;
                coefficientsArray[i] = cf.subsequentCoefficients[position];
            }

            // now get the penultimate and ultimate convergent fractions
            var cfUltimate = new ContinuedFraction()
            {
                doCoefficientsRepeat = false,
                firstCoefficient = cf.firstCoefficient,
                subsequentCoefficients = coefficientsArray,
            };
            var convergentUltimate = FractionCalculator.GetContinuedFractionConvergence(cfUltimate);

            var cfPenultimate = new ContinuedFraction()
            {
                doCoefficientsRepeat = false,
                firstCoefficient = cf.firstCoefficient,
                subsequentCoefficients = coefficientsArray[0..(coefficientsLength - 1)],
            };
            var convergentPenultimate = FractionCalculator.GetContinuedFractionConvergence(cfPenultimate);

            // set up the output array and cycle through the algorithm
            List<int> digits = new List<int>();

            var numerator_p = convergentPenultimate.numerator;
            var denominator_p = convergentPenultimate.denominator;
            var numerator_u = convergentUltimate.numerator;
            var denominator_u = convergentUltimate.denominator;

            while (true)
            {
                var floor_p = numerator_p / denominator_p;
                var floor_u = numerator_u / denominator_u;
                if (floor_p != floor_u)
                {
                    return digits.ToArray();
                }

                // add the floor to the digits list and subtract it out
                digits.Add((int)floor_p);
                numerator_p = numerator_p - (floor_p * denominator_p);
                numerator_u = numerator_u - (floor_u * denominator_u);

                // multiply by 10 and repeat
                numerator_p *= 10;
                numerator_u *= 10;
            }
        }
        public static Fraction Multiply(Fraction f1, Fraction f2, bool shouldReduce = true)
		{
            Fraction product = new Fraction();
            product.numerator = f1.numerator * f2.numerator;
            product.denominator = f1.denominator * f2.denominator;
            if(shouldReduce)return Reduce(product);
            return product;
		}
        public static LongFraction Multiply(LongFraction f1, LongFraction f2, bool shouldReduce = true)
        {
            LongFraction product = new LongFraction();
            product.numerator = f1.numerator * f2.numerator;
            product.denominator = f1.denominator * f2.denominator;
            if (shouldReduce) return Reduce(product);
            return product;
        }
        public static BigFraction Multiply(BigFraction f1, BigFraction f2, bool shouldReduce = true)
        {
            BigFraction product = new BigFraction();
            product.numerator = f1.numerator * f2.numerator;
            product.denominator = f1.denominator * f2.denominator;
            if (shouldReduce) return Reduce(product);
            return product;
        }
        public static (Fraction f1, Fraction f2) NormalizeFractions(Fraction f1, Fraction f2)
		{
            // normalize the denominators
            int normalizedDenominator = f1.denominator * f2.denominator;
            int newF1Numerator = normalizedDenominator / f1.denominator * f1.numerator;
            int newF2Numerator = normalizedDenominator / f2.denominator * f2.numerator;
            return (
                new Fraction(newF1Numerator, normalizedDenominator),
                new Fraction(newF2Numerator, normalizedDenominator));
        }
        public static (LongFraction f1, LongFraction f2) NormalizeFractions(LongFraction f1, LongFraction f2)
        {
            // normalize the denominators
            long normalizedDenominator = f1.denominator * f2.denominator;
            long newF1Numerator = normalizedDenominator / f1.denominator * f1.numerator;
            long newF2Numerator = normalizedDenominator / f2.denominator * f2.numerator;
            return (
                new LongFraction(newF1Numerator, normalizedDenominator),
                new LongFraction(newF2Numerator, normalizedDenominator));
        }
        public static (BigFraction f1, BigFraction f2) NormalizeFractions(BigFraction f1, BigFraction f2)
        {
            // normalize the denominators
            BigInteger normalizedDenominator = f1.denominator * f2.denominator;
            BigInteger newF1Numerator = normalizedDenominator / f1.denominator * f1.numerator;
            BigInteger newF2Numerator = normalizedDenominator / f2.denominator * f2.numerator;
            return (
                new BigFraction(newF1Numerator, normalizedDenominator),
                new BigFraction(newF2Numerator, normalizedDenominator));
        }
        public static Fraction Recriprocate(Fraction f, bool shouldReduce = true)
        {
            int numerator = f.denominator;
            int denominator = f.numerator;
            var unReduced = new Fraction(numerator, denominator);
            if (shouldReduce) return Reduce(unReduced);
            return unReduced;
        }
        public static LongFraction Recriprocate(LongFraction f, bool shouldReduce = true)
        {
            long numerator = f.denominator;
            long denominator = f.numerator;
            var unReduced = new LongFraction(numerator, denominator);
            if (shouldReduce) return Reduce(unReduced);
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
        public static LongFraction Reduce(LongFraction f)
        {
            // greatest common factor method
            long[] numeratorFactors = CommonAlgorithms.GetFactors(f.numerator);
            long[] denominatorFactors = CommonAlgorithms.GetFactors(f.denominator);

            long gcf = 0;
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
            long newNumerator = f.numerator / gcf;
            long newDenominator = f.denominator / gcf;

            return new LongFraction(newNumerator, newDenominator);
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
        public static Fraction Subtract(Fraction f1, Fraction f2, bool shouldReduce = true)
        {
            // normalize the denominators
            int normalizedDenominator = f1.denominator * f2.denominator;
            int newF1Numerator = normalizedDenominator / f1.denominator * f1.numerator;
            int newF2Numerator = normalizedDenominator / f2.denominator * f2.numerator;

            Fraction unReducedAddition = new Fraction(
                newF1Numerator - newF2Numerator,
                normalizedDenominator);

            if (shouldReduce && unReducedAddition.numerator > 0) return Reduce(unReducedAddition);
            return unReducedAddition;
        }
        public static LongFraction Subtract(LongFraction f1, LongFraction f2, bool shouldReduce = true)
        {
            // normalize the denominators
            long normalizedDenominator = f1.denominator * f2.denominator;
            long newF1Numerator = normalizedDenominator / f1.denominator * f1.numerator;
            long newF2Numerator = normalizedDenominator / f2.denominator * f2.numerator;

            LongFraction unReducedAddition = new LongFraction(
                newF1Numerator - newF2Numerator,
                normalizedDenominator);

            if (shouldReduce && unReducedAddition.numerator > 0) return Reduce(unReducedAddition);
            return unReducedAddition;
        }
        public static BigFraction Subtract(BigFraction f1, BigFraction f2, bool shouldReduce = true)
        {
            // normalize the denominators
            BigInteger normalizedDenominator = f1.denominator * f2.denominator;
            BigInteger newF1Numerator = normalizedDenominator / f1.denominator * f1.numerator;
            BigInteger newF2Numerator = normalizedDenominator / f2.denominator * f2.numerator;

            BigFraction unReducedAddition = new BigFraction(
                newF1Numerator - newF2Numerator,
                normalizedDenominator);

            if (shouldReduce && unReducedAddition.numerator > 0) return Reduce(unReducedAddition);
            return unReducedAddition;
        }
    }
}
