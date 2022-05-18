using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    internal static class WeirdAlgorithms
    {
        /// <summary>
        /// used for sorting large string lists
        /// </summary>
        internal static string[] AlphabeticalSort(string[] inputList)
        {
            // just sorting these using linq takes a very long time
            // split them out by first letter, then sort
            // I'm sure I could've copied a fast sorting algorithm
            // from the internet instead but this seemed fun
            List<string> listSorted = new List<string>();
            var firstLetterGroups = inputList
                .GroupBy(name => name[0])
                .Select(group => new
                {
                    firstLetter = group.Key,
                    names = group.Select(x => x)
                }).OrderBy(x => x.firstLetter);
            foreach (var group in firstLetterGroups)
            {
                listSorted.AddRange(group.names.OrderBy(x => x));
            }
            return listSorted.ToArray();
        }
        internal static bool IsAmicableNumber(long n)
        {
            // using a and b makes it easier to think through the logic
            long a = n;
            long b = 0;

            long[] properDivisorsA = MathHelper.GetProperDivisorsOfN(a);
            long sumOfDivisorsA = properDivisorsA.Sum();

            b = sumOfDivisorsA;

            long[] properDivisorsB = MathHelper.GetProperDivisorsOfN(b);
            long sumOfDivisorsB = properDivisorsB.Sum();
            
            if (a == b) return false;

            if(sumOfDivisorsB == a) return true;

            return false;
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
