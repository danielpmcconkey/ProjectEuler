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
        internal static int[] AddTwoLargeNumbers(int[] a, int[] b)
        {
            // long-form way of adding two ridiculuosly large numbers
            // just like elementary school

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
        internal static string AddTwoLargeNumbers(string a, string b)
        {
            // long-form way of adding two ridiculuosly large numbers
            // assumes the strings contain only numbers. no commas or
            // periods
            // just like elementary school

            // first pad the shorter string with zeros so they're both
            // the same length
            int length = (a.Length > b.Length) ? a.Length : b.Length;
            a = a.PadLeft(length, '0');
            b = b.PadLeft(length, '0');

            // convert to an array of numbers
            var aAsChars = a.ToCharArray();
            var bAsChars = b.ToCharArray();
            var aAsNums = new short[length];
            var bAsNums = new short[length];
            for (int i = 0; i < length; i++)
            {
                aAsNums[i] = Int16.Parse(aAsChars[i].ToString());
                bAsNums[i] = Int16.Parse(bAsChars[i].ToString());
            }

            // now go right to left and add the two like elementary school
            int currentRemainder = 0;
            List<int> result = new List<int>();

            for (int position = length - 1; position >= 0; position--)
            {
                int sumThisPosition = currentRemainder;
                sumThisPosition += aAsNums[position];
                sumThisPosition += bAsNums[position];

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

            // finally, go through the result list in reverse order to produce your final string
            string answer = "";
            for (int i = result.Count - 1; i >= 0; --i)
            {
                answer += result[i].ToString();
            }
            return answer;
        }
        internal static List<long> GetFactorsOfN(long n)
        {
            if (n <= 0) throw new ArgumentException("n must be greater than 0");

            List<long> factors = new List<long>();
            if (n == 1)
            {
                factors.Add(1);
                return factors;
            }

            long maxVal = (long)Math.Floor(n * .5); // no sense looking at anything above half
            long lowestOppositeFactor = n;

            for (long i = 1; i <= maxVal; i++)
            {
                if (i >= lowestOppositeFactor) return factors;
                if (n % i == 0)
                {
                    factors.Add(i);
                    // also add the opposite factor
                    long oppositeFactor = n / i;
                    factors.Add(oppositeFactor);
                    lowestOppositeFactor = oppositeFactor;
                }
            }
            return factors;
        }
        #endregion


        #region private methods
        private static (int lastPlace, int remainder) GetLastPositionValAndRemainder(int n)
        {
            // splits a number between its right-most digit (last place)
            // and the rest (its remainder). Used for long-form
            // addition like when you're adding two very large numbers
            // together
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
