using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    internal static class BigNumberCalculator
    {
        /// <summary>
        /// long-form way of adding two ridiculuosly large numbers just like elementary school
        /// </summary>
        internal static BigNumber Add(BigNumber a, BigNumber b)
        {
            // first pad the shorter string with zeros so they're both
            // the same length
            var normalizedArrays = NormalizeIntArrays(new BigNumber[] { a, b });
            int length = normalizedArrays[0].digits.Length;

            // now go right to left and add the two like elementary school
            int currentRemainder = 0;
            List<int> result = new List<int>();

            for (int position = length - 1; position >= 0; position--)
            {
                int sumThisPosition = currentRemainder;
                sumThisPosition += normalizedArrays[0].digits[position];
                sumThisPosition += normalizedArrays[1].digits[position];

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

            return new BigNumber(returnArray);
        }
        /// <summary>
        /// raises a to the power of x. only supports x that are integers and greater than 0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        internal static BigNumber Exponent(int a, int x)
        {
            BigNumber answer = new BigNumber(a);
            BigNumber multiplier = new BigNumber(a);
            for (int i = 2; i <= x; i++)
            {
                answer = Multiply(answer, multiplier);
            }
            return answer;
        }
        /// <summary>
        /// long-form way of multiplying two ridiculuosly large numbers just like elementary school
        /// </summary>
        internal static BigNumber Multiply(BigNumber a, BigNumber b)
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


            List<BigNumber> allRows = new List<BigNumber>();
            int powerOf10 = 0;  // used to add the right number of zeros based on which place we're in

            for (int i = b.digits.Length - 1; i >= 0; --i)
            {
                List<int> currentRowResult = new List<int>();
                int currentRemainder = 0;

                // add the placeholder zeros for the 10s, 100s, whatever space
                for (int k = 0; k < powerOf10; ++k)
                {
                    currentRowResult.Add(0);
                }
                for (int j = a.digits.Length - 1; j >= 0; --j)
                {
                    int productAtPoint = (b.digits[i] * a.digits[j]) + currentRemainder;
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
                allRows.Add(new BigNumber(currentRowResultArray));
            }

            BigNumber answer = new BigNumber(new int[] { 0 });
            foreach (var row in allRows)
            {
                answer = Add(answer, row);
            }

            return answer;
        }
        
       

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
        private static BigNumber[] NormalizeIntArrays(BigNumber[] bigguns)
        {
            BigNumber[] returnArray = new BigNumber[bigguns.Length];
            // what's the longest length
            int longestLength = bigguns.Max(a => a.digits.Length);
            // iterate through the arrays and add a "normalized"
            // version to the return arrays at the same position
            for (int i = 0; i < bigguns.Length; i++)
            {
                var arrayAtI = bigguns[i];
                if (arrayAtI.digits.Length == longestLength)
                {
                    returnArray[i] = arrayAtI;
                }
                else
                {
                    int differenceInLengths = longestLength - arrayAtI.digits.Length;
                    int[] newArray = new int[longestLength];
                    for (int j = longestLength - 1; j >= 0; j--)
                    {
                        if (j < differenceInLengths)
                        {
                            newArray[j] = 0;
                        }
                        else
                        {
                            newArray[j] = arrayAtI.digits[j - differenceInLengths];
                        }
                    }
                    returnArray[i] = new BigNumber(newArray);
                }
            }
            return returnArray;
        }
        #endregion
    }
}
