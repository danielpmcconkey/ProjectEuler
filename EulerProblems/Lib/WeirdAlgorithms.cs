using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    internal static class WeirdAlgorithms
    {
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
