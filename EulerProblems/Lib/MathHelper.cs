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
        /// <summary>
        /// long-form way of adding two ridiculuosly large numbers just like elementary school
        /// </summary>
        internal static int[] LongFormAddition(int[] a, int[] b)
        {
            // first pad the shorter string with zeros so they're both
            // the same length
            var normalizedArrays = NormalizeIntArrays(new int[][] { a, b });
            int length = normalizedArrays[0].Length;
            
            // now go right to left and add the two like elementary school
            int currentRemainder = 0;
            List<int> result = new List<int>();

            for (int position = length - 1; position >= 0; position--)
            {
                int sumThisPosition = currentRemainder;
                sumThisPosition += normalizedArrays[0][position];
                sumThisPosition += normalizedArrays[1][position];

                // add the last digit to the stack
                (int lastPlace, int remainder) positionResult = GetLastPositionValAndRemainder(sumThisPosition);
                result.Add(positionResult.lastPlace);
                currentRemainder = positionResult.remainder;
            }

            // done going through the columns, now handle the final remainder
            if (currentRemainder > 0)
            {
                char[] remainderAsCharArray = currentRemainder.ToString().ToCharArray();
                // go right to left and pop any digits onto the list
                for (int i = remainderAsCharArray.Length - 1; i >= 0; i--)
                {
                    int valueAtPlace = int.Parse(remainderAsCharArray[i].ToString());
                    result.Add(valueAtPlace);
                }
            }
            // finally, go through the result list in reverse order to produce your final array
            int[] returnArray = result.ToArray();
            returnArray = returnArray.Reverse().ToArray();

            return returnArray;
        }
        /// <summary>
        /// long-form way of multiplying two ridiculuosly large numbers just like elementary school
        /// </summary>
        internal static int[] LongFormMultiplication(int[] a, int[] b)
        {
            /* 
             *            492
             *     *       33
             *    ------------
             *           1476
             *     +    14760
             *    ------------
             *          16236
             *          
             *    (there were remainders carried over not shown above)
             * 
             * */
            

            // start with the right-most digit of b and multiply it by each 
            
            
            List<int[]> allRows = new List<int[]>();
            int powerOf10 = 0;  // used to add the right number of zeros based on which place we're in

            for(int i = b.Length - 1; i >= 0; --i)
            {
                List<int> currentRowResult = new List<int>();
                int currentRemainder = 0;

                // add the placeholder zeros for the 10s, 100s, whatever space
                for (int k = 0; k < powerOf10; ++k)
                {
                    currentRowResult.Add(0);
                }
                for (int j = a.Length - 1; j >= 0; --j)
                {
                    int productAtPoint = (b[i] * a[j]) + currentRemainder;
                    (int lastPlace, int remainder) valAndRemainder = 
                        GetLastPositionValAndRemainder(productAtPoint);
                    currentRemainder = valAndRemainder.remainder;
                    currentRowResult.Add(valAndRemainder.lastPlace);
                }
                // add the current remainder in reverse order
                if (currentRemainder > 0)
                {
                    char[] remainderAsCharArray = currentRemainder.ToString().ToCharArray();
                    // go right to left and pop any digits onto the list
                    for (int k = remainderAsCharArray.Length - 1; k >= 0; k--)
                    {
                        int valueAtPlace = int.Parse(remainderAsCharArray[k].ToString());
                        currentRowResult.Add(valueAtPlace);
                    }
                }
                // and now move the power of ten up
                powerOf10++;

                // reverse current row and add it to the all rows list
                int[] currentRowResultArray = currentRowResult.ToArray();
                currentRowResultArray = currentRowResultArray.Reverse().ToArray();
                allRows.Add(currentRowResultArray);
            }

            int[] answer = new int[] { 0 };
            foreach(var row in allRows)
            {
                answer = LongFormAddition(answer, row);
            }

            return answer;
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
        internal static int[] GetFactorialOfNLongForm(long n)
        {
            int[] answer = new int[] { 1 };
            for(long i = n; i > 0; i--)
            {
                int[] multiplier = LongToIntArray(i);
                answer = LongFormMultiplication(answer, multiplier);
            }
            return answer;
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
        /// <summary>
        /// takes an int and turns it into an array of integers
        /// representing each of the digits in the original
        /// number. Used for doing long form arithematic
        /// </summary>
        internal static int[] IntToIntArray(int n)
        {
            return LongToIntArray((long)n);
        }
        /// <summary>
        /// takes a long and turns it into an array of integers
        /// representing each of the digits in the original
        /// number. Used for doing long form arithematic
        /// </summary>
        internal static int[] LongToIntArray(long n)
        {
            int ordersOfMagnitudeToSupport = 12;
            List<int> digitsInReverse = new List<int>();
            for (int i = 0; i < ordersOfMagnitudeToSupport; i++)
            {
                if(n >= Math.Pow(10, i))
                {
                    digitsInReverse.Add(
                       (int) (Math.Floor(
                            n % Math.Pow(10, i + 1)
                            /
                            Math.Pow(10, i)
                            )));
                }
            }
            // now turn it to an array and reverse
            return digitsInReverse.ToArray().Reverse().ToArray();
        }
        #endregion


        #region private methods
        /// <summary>
        /// splits a number between its right-most digit (last place)
        /// and the rest (its remainder). Used for long-form
        /// addition like when you're adding two very large numbers
        /// together
        /// </summary>        
        private static (int lastPlace, int remainder) GetLastPositionValAndRemainder(int n)
        {
            
            string sumAsString = n.ToString();
            int length = sumAsString.Length;
            string lastPlaceString = sumAsString.Substring(length - 1, 1);
            string remainderString = sumAsString.Substring(0, length - 1);
            int lastPlace = int.Parse(lastPlaceString);
            int remainder = (remainderString == String.Empty) ? 0 : int.Parse(remainderString);
            return (lastPlace, remainder);
        }
        /// <summary>
        /// takes int arrays of uneven length and returns 
        /// int arrays of the same length, left padded 
        /// with zeros
        /// </summary>
        private static int[][] NormalizeIntArrays(int[][] arrays)
        {
            int[][] returnArray = new int[arrays.Length][];
            // what's the longest length
            int longestLength = arrays.Max(a => a.Length);
            // iterate through the arrays and add a "normalized"
            // version to the return arrays at the same position
            for(int i = 0; i < arrays.Length; i++)
            {
                var arrayAtI = arrays[i];
                if(arrayAtI.Length == longestLength)
                {
                    returnArray[i] = arrayAtI;
                }
                else
                {
                    int differenceInLengths = longestLength - arrayAtI.Length;
                    int[] newArray = new int[longestLength];
                    for(int j = longestLength - 1; j >= 0; j--)
                    {
                        if (j < differenceInLengths)
                        {
                            newArray[j] = 0;
                        }
                        else
                        {
                            newArray[j] = arrayAtI[j - differenceInLengths];
                        }
                    }
                    returnArray[i] = newArray;
                }
            }
            return returnArray;
        }
        #endregion
    }
}
