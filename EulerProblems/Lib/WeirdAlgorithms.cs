using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    internal static class WeirdAlgorithms
    {
        internal static string AddTwoStrings(string a, string b)
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
            if(currentRemainder > 0)
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
        internal static (int lastPlace, int remainder) GetLastPositionValAndRemainder(int n)
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
        internal static bool IsIntPalindromic(int n)
        {
            char[] intAsCharArray = n.ToString().ToCharArray();
            int numberOfDigits = intAsCharArray.Length;
            int halfWayPoint = (numberOfDigits / 2) + 1;

            for (int i = 0; i < halfWayPoint; i++)
            {
                char checkLeft = intAsCharArray[i];
                char checkRight = intAsCharArray[numberOfDigits - i - 1];
                if (checkLeft != checkRight)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsPythagoreanTriplet(int a, int b, int c)
        {
            if (Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2)) return true;
            return false;
        }
        internal static long SumOfSquares(long n)
        {
            long sum = 0;
            for (long i = 1; i <= n; i++)
            {
                sum += (long)Math.Pow(i, 2);
            }
            return sum;
        }
        internal static long SquareOfSum(long n)
        {
            long sum = 0;
            for (long i = 1; i <= n; i++)
            {
                sum += i;
            }
            return (long)Math.Pow(sum, 2);
        }
    }
}
