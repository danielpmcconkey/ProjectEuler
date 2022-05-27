using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    internal static class MathHelper
    {
        #region inteface methods
        internal static int ConvertIntArrayToInt(int[] array)
        {
            int outVal = 0;
            for (int i = 0; i < array.Length; i++)
            {
                int pow = array.Length - i - 1;
                outVal += array[i] * (int)(Math.Pow(10, pow));
            }
            return outVal;
        }
        internal static int[] ConvertIntToIntArray(int n)
        {
            int ordersOfMagnitudeToSupport = 12;
            List<int> digitsInReverse = new List<int>();
            for (int i = 0; i < ordersOfMagnitudeToSupport; i++)
            {
                if (n >= Math.Pow(10, i))
                {
                    digitsInReverse.Add(
                       (int)(Math.Floor(
                            n % Math.Pow(10, i + 1)
                            /
                            Math.Pow(10, i)
                            )));
                }
            }
            // now turn it to an array and reverse
            int[] digits = digitsInReverse.ToArray().Reverse().ToArray();
            return digits;
        }
        internal static int[] ConvertLongToIntArray(long n)
        {
            int ordersOfMagnitudeToSupport = 12;
            List<int> digitsInReverse = new List<int>();
            for (int i = 0; i < ordersOfMagnitudeToSupport; i++)
            {
                if (n >= Math.Pow(10, i))
                {
                    digitsInReverse.Add(
                       (int)(Math.Floor(
                            n % Math.Pow(10, i + 1)
                            /
                            Math.Pow(10, i)
                            )));
                }
            }
            // now turn it to an array and reverse
            int[] digits = digitsInReverse.ToArray().Reverse().ToArray();
            return digits;
        }
        /// <summary>
        /// used for standard factorials on tame numbers
        /// if nubers are large, use the long form function
        /// </summary>
        internal static long GetFactorialOfN(int n)
        {
            if (n == 0) return 1;
            return (long)n * GetFactorialOfN(n - 1);

        }
        /// <summary>
        /// used to get factorials when you know the result
        /// will be too large for a long to hold
        /// </summary>
        internal static BigNumber GetFactorialOfNLongForm(long n)
        {
            BigNumber answer = new BigNumber(new int[] { 1 });
            for(long i = n; i > 0; i--)
            {
                BigNumber multiplier = new BigNumber(i);
                answer = BigNumberCalculator.Multiply(answer, multiplier);
            }
            return answer;
        }
        internal static int GetOrderOfMagnitudeOfInt(int n)
        {
            if(n == 0 || n == 1) return 0;
            int numOOMSupported = 12;
            int currentOOM = -1;
            for (int i = 0; i <= numOOMSupported; i++)
            {
                if (n >= Math.Pow(10, i)) currentOOM++;
                else return currentOOM;
            }
            return currentOOM;
        }
        /// <summary>
        /// returns a list of all proper divisors ordered least to greatest.
        /// a proper divisor is a number less than n which divide evenly into n
        /// </summary>
        internal static long[] GetProperDivisorsOfN(long n)
        {
            long[] factors = MathHelper.GetFactorsOfN(n);
            long[] properDivisors = factors
                .Where(x => x != n)
                .OrderBy(y => y)
                .ToArray();
            return properDivisors;
        }
        internal static long[] GetFactorsOfN(long n)
        {
            if (n <= 0) throw new ArgumentException("n must be greater than 0");

            List<long> factors = new List<long>();
            if (n == 1)
            {
                factors.Add(1);
                return factors.ToArray();
            }

            long maxVal = (long)Math.Floor(n * .5); // no sense looking at anything above half
            long lowestOppositeFactor = n;

            for (long i = 1; i <= maxVal; i++)
            {
                if (i >= lowestOppositeFactor) return factors.ToArray();
                if (n % i == 0)
                {
                    factors.Add(i);
                    // also add the opposite factor
                    long oppositeFactor = n / i;
                    if (oppositeFactor != i)
                    {
                        factors.Add(oppositeFactor);
                    }
                    lowestOppositeFactor = oppositeFactor;
                }
            }
            return factors.ToArray();
        }
        internal static int[] GetFactorsOfN(int n)
        {
            if (n <= 0) throw new ArgumentException("n must be greater than 0");

            List<int> factors = new List<int>();
            if (n == 1)
            {
                factors.Add(1);
                return factors.ToArray();
            }

            long maxVal = (int)Math.Floor(n * .5); // no sense looking at anything above half
            long lowestOppositeFactor = n;

            for (int i = 1; i <= maxVal; i++)
            {
                if (i >= lowestOppositeFactor) return factors.ToArray();
                if (n % i == 0)
                {
                    factors.Add(i);
                    // also add the opposite factor
                    int oppositeFactor = n / i;
                    if (oppositeFactor != i)
                    {
                        factors.Add(oppositeFactor);
                    }
                    lowestOppositeFactor = oppositeFactor;
                }
            }
            return factors.ToArray();
        }        
        internal static bool IsPerfectSquare(int n)
        {
            double result = Math.Sqrt(n);
            return (result % 1 == 0);
        }

        #endregion



    }
}
