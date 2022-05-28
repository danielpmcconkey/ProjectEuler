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
        internal static T[] ArraySwap<T>(T[] array, int indexOfSwap1, int indexOfSwap2)
        {
            T valueAtSwap1 = array[indexOfSwap1];
            T valueAtSwap2 = array[indexOfSwap2];
            T[] newArray = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                if (i == indexOfSwap1) newArray[i] = valueAtSwap2;
                else if (i == indexOfSwap2) newArray[i] = valueAtSwap1;
                else newArray[i] = array[i];
            }
            return newArray;
        }
        /// <summary>
		/// recursive function for determining all the permutations of
		/// an array of integers, returning them in order
		/// </summary>        
		internal static int[][] GetAllLexicographicPermutationsOfIntArray(int[] orderedNumerals)
        {
            if (orderedNumerals.Length == 1)
            {
                // just return it back
                return new int[][] { new int[] { orderedNumerals[0] } };
            }
            if (orderedNumerals.Length == 2)
            {
                // too small for the below algorithm to work. do it manually
                return new int[][] {
                    new int[] { orderedNumerals[0], orderedNumerals[1] },
                    new int[] { orderedNumerals[1], orderedNumerals[0] }
                };
            }

            List<int[]> returnList = new List<int[]>();

            for (int i = 0; i < orderedNumerals.Length; i++)
            {
                int[] newOrderedList = new int[orderedNumerals.Length - 1];
                for (int k = 0; k < orderedNumerals.Length; k++)
                {
                    if (orderedNumerals[k] == orderedNumerals[i])
                    {
                        // do nothing; don't add it because you only want to add the other digits
                    }
                    else if (orderedNumerals[k] < orderedNumerals[i])
                    {
                        newOrderedList[k] = orderedNumerals[k];
                    }
                    else if (orderedNumerals[k] > orderedNumerals[i])
                    {
                        newOrderedList[k - 1] = orderedNumerals[k];
                    }
                }
                int[][] subordinateLists = GetAllLexicographicPermutationsOfIntArray(newOrderedList);
                foreach (var subordinate in subordinateLists)
                {
                    List<int> thisList = new List<int>();
                    thisList.Add(orderedNumerals[i]);
                    thisList.AddRange(subordinate);
                    returnList.Add(thisList.ToArray());
                }
            }
            return returnList.ToArray();
        }
        /// <summary>
        /// Generate permutations using Heap's Algorithm. This can accept an array
        /// whose values are not distict and also doesn't require the array to be
        /// sorted
        /// </summary>        
        internal static T[][] HeapsAlgorithm<T>(T[] array)
        {
            int n = array.Length;

            List<T[]> permutations = new List<T[]>();
            int[] c = new int[n];

            for (int j = 0; j < n; j++)
            {
                c[j] = 0;
            }
            //displayPermutation(array);
            permutations.Add(array);

            int i = 0;
            while (i < n)
            {
                if (c[i] < i)
                {
                    if (i % 2 == 0)
                    {
                        array = ArraySwap(array, 0, i);
                    }
                    else
                    {
                        array = ArraySwap(array, c[i], i);
                    }
                    //displayPermutation(array);
                    permutations.Add(array);
                    c[i] = c[i] + 1;
                    i = 0;
                }
                else
                {
                    c[i] = 0;
                    i++;
                }
            }
            return permutations.ToArray();
        }
        /// <summary>
        /// gets the value in the Fibonacci sequence at position N
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        internal static long GetFinoacciSequenceValueAtPositionN(int n)
        {
            /* 
             * formula used is here 
             * https://www.calculatorsoup.com/calculators/discretemathematics/fibonacci-calculator.php
             * 
             * Fn = ( (1 + √5)^n - (1 - √5)^n ) / (2^n × √5)
             * 
             * */

            double squareRootOf5 = Math.Pow(5, 0.5);

            double numerator = (
                    Math.Pow((1 + squareRootOf5), n)
                  - Math.Pow((1 - squareRootOf5), n));
            double denominator = Math.Pow(2, n) * squareRootOf5;

            return (long)Math.Round((numerator / denominator), 0);
        }
        public static long[] GetFirstNPentagonalNumbers(int n)
        {
            long[] pentagonalNumbers = new long[n];
            for (long i = 1; i <= n; i++)
            {
                pentagonalNumbers[i - 1] = (long)Math.Round((i * ((3 * i) - 1)) * 0.5f, 0);
            }
            return pentagonalNumbers;
        }
        public static long[] GetFirstNTriangularNumbers(int n)
        {
            long[] triangularNumbers = new long[n];
            for (long i = 1; i <= n; i++)
            {
                triangularNumbers[i - 1] = (long)Math.Round((i * ((1 * i) + 1)) * 0.5f, 0);
            }
            return triangularNumbers;
        }
        /// <summary>
        /// returns the position of a letter in a zero-indexed alphabet array
        /// </summary>
        internal static int GetIndexOfLetterInAlphabet(char letter)
        {
            /*
             * in UTF-16, you have the following hexadecimal encodings:
             *    A = 0041
             *    B = 0042
             *    ...
             *    Z = 005a
             *    a = 0061
             *    b = 0062
             *    ...
             *    z = 007a
             *    
             * so, converting to decimal, you have capital letters ranging from
             * 65 to 90 and you have lower case letters ranging from 97 to 122
             * 
             * */
            int index = (int)letter;
            if (index >= 65 && index <= 90) return index - 65;
            if (index >= 97 && index <= 122) return index - 97;

            // anything else is unsupported
            throw new ArgumentException(
                String.Format("Value of {0} is not supported for alphabetical indexing.", letter.ToString()));
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

            if (sumOfDivisorsB == a) return true;

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
        internal static bool IsIntPalindromicInBinary(int n)
        {
            string binaryString = Convert.ToString(n, 2);
            char[] intAsCharArray = binaryString.ToCharArray();
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
        internal static bool IsPandigital(long n)
        {
            int[] digits = MathHelper.ConvertLongToIntArray(n);
            if (digits.Length != 9) return false;
            if (digits.Contains(1))
                if (digits.Contains(2))
                    if (digits.Contains(3))
                        if (digits.Contains(4))
                            if (digits.Contains(5))
                                if (digits.Contains(6))
                                    if (digits.Contains(7))
                                        if (digits.Contains(8))
                                            if (digits.Contains(9))
                                            {
                                                return true;
                                            }
            return false;
        }
        internal static bool IsPentagonal(long n)
        {
            // https://www.divye.in/2012/07/how-do-you-determine-if-number-n-is.html
            /* this isn't working. it shows way more pentagonal numbers than it should
            * run this code to see
                * 
                * 
                var pents = WeirdAlgorithms.GetFirstNPentagonalNumbers(100);
                List<long> otherPents = new List<long>();
                int numFound = 0;
                long i = 1; 
                while (numFound <= 100)
                {
                    if(WeirdAlgorithms.IsPentagonal((int)i))
                    {
                        otherPents.Add(i);
                        numFound++;


                    }
                    i++;
                }
                for(int j = 0; j < 100; j++)
                {
                    Console.WriteLine(string.Format("{0}{1}", pents[j].ToString().PadRight(10), otherPents[j].ToString()));
                }
            */

            long x = 1 + (24 * n);
            if (MathHelper.IsPerfectSquare(x))
            {
                if (Math.Sqrt(x) % 6 == 5)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsPythagoreanTriplet(int a, int b, int c)
        {
            if (Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2)) return true;
            return false;
        }
        internal static bool IsTriangular(long n)
        {
            // https://en.wikipedia.org/wiki/Triangular_number
            if (MathHelper.IsPerfectSquare(1 + (8 * n))) return true;
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
