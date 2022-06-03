using System.Numerics;

namespace EulerProblems.Lib
{
    internal static class CommonAlgorithms
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
        internal static long ConvertIntArrayToLong(int[] array)
        {
            long outVal = 0;
            for (int i = 0; i < array.Length; i++)
            {
                int pow = array.Length - i - 1;
                outVal += array[i] * (long)(Math.Pow(10, pow));
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
        internal static T[][] GetAllPermutationsOfArray<T>(T[] array)
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
        internal static BigInteger GetCombinatoricRFromN(int n, int r)
        {
            /*
             * https://en.wikipedia.org/wiki/Combinatorics
             * 
             *      /     \
             *      |  n  |            n!
             *      | --- |  =   -------------
             *      |  r  |       r! (n - r)!
             *      \     /
             * 
             * */

            if (r > n) throw new ArgumentException();

            BigInteger nFact = GetFactorial((BigInteger)n);
            BigInteger rFact = GetFactorial((BigInteger)r);
            BigInteger nMinusRFact = GetFactorial((BigInteger)(n - r));
            BigInteger answer =  nFact/ (rFact * nMinusRFact);
            return answer;
        }
        /// <summary>
        /// used for standard factorials on tame numbers
        /// if nubers are large, use the long form function
        /// </summary>
        internal static long GetFactorial(int n)
        {
            if (n == 0) return 1;
            return (long)n * GetFactorial(n - 1);

        }
        /// <summary>
        /// used for bigger numbers
        /// </summary>
        internal static BigInteger GetFactorial(BigInteger n)
        {
            if (n == 0) return 1;
            return n * GetFactorial(n - 1);

        }
        [Obsolete("GetFactorialLongForm is deprecated, please use GetFactorial (BigInteger) instead.")]
        internal static BigNumber GetFactorialLongForm(long n)
        {
            BigNumber answer = new BigNumber(new int[] { 1 });
            for (long i = n; i > 0; i--)
            {
                BigNumber multiplier = new BigNumber(i);
                answer = BigNumberCalculator.Multiply(answer, multiplier);
            }
            return answer;
        }
        internal static long[] GetFactors(long n)
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
        internal static int[] GetFactors(int n)
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
        public static long[] GetFirstNPerfectSquares(int n)
        {
            // excludes 0
            long[] perfectSquares = new long[n];
            for(long i = 0; i < n; i++)
            {
                perfectSquares[i] = (long) Math.Pow(i + 1, 2);
            }
            return perfectSquares;
        }
        internal static long[] GetFirstNPrimes(int n)
        {
            long[] primes = new long[n];
            primes[0] = 2; // add 2 manually so I can easily skip all even numbers moving forward
            int numPrimesFound = 1;
            long i = 3;
            while (true)
            {
                if (IsPrime(i))
                {
                    numPrimesFound++;
                    primes[numPrimesFound - 1] = i;
                    if (numPrimesFound == n) return primes;
                }
                i++;
            }
            throw new Exception("n number of primes not found");
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
        public static int GetGridOrdinalFromPosition(int gridWidth, int row, int column)
        {
            return (row * gridWidth) + column;
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
        internal static int GetOrderOfMagnitude(int n)
        {
            if (n == 0 || n == 1) return 0;
            int numOOMSupported = 12;
            int currentOOM = -1;
            for (int i = 0; i <= numOOMSupported; i++)
            {
                if (n >= Math.Pow(10, i)) currentOOM++;
                else return currentOOM;
            }
            return currentOOM;
        }
        internal static int[] GetPrimeFactors(int n, bool[] primes)
        {
            int[] factors = GetFactors(n);
            List<int> primeFactors = new List<int>();
            foreach(int f in factors)
            {
                if (primes[f]) primeFactors.Add(f);
            }
            return primeFactors.ToArray();
        }
        internal static long[] GetPrimesUpToN(long n)
        {
            // this will take a long time to run. Use the integer function if you can help it
            List<long> primes = new List<long>();

            if (n >= 2)
            {
                primes.Add(2);
            }
            for (long i = 3; i < n; i += 2)
            {
                if (IsPrime(i))
                {
                    primes.Add(i);
                }
                // if((i-1) % 1000 == 0) Console.WriteLine(String.Format("primes found up to {0}", i));
            }
            return primes.ToArray();
        }
        internal static int[] GetPrimesUpToN(int n)
        {
            // https://www.baeldung.com/cs/prime-number-algorithms
            // https://kalkicode.com/sieve-of-sundaram
            // note this only works for relatively small numbers due to the limits
            // of array sizes
            List<int> primes = new List<int>();
            if (n <= 1)
            {
                //When n are invalid to prime number 
                return new int[0];
            }
            //Calculate the number of  prime of given n
            int limit = ((n - 2) / 2) + 1;
            //This are used to detect prime numbers
            int[] sieve = new int[limit];
            // Loop controlling variables
            long i = 0;
            long j = 0;
            //Set initial all the numbers are non prime
            for (i = 0; i < limit; ++i)
            {
                sieve[i] = 0;
            }
            for (i = 1; i < limit; ++i)
            {
                for (j = i;
                    (i + j + 2 * i * j) < limit; ++j)
                {
                    //(i + j + 2ij) are unset
                    sieve[(i + j + 2 * i * j)] = 1;
                }
            }
            // 2 needs to be added manually
            if (n >= 2)
            {
                primes.Add(2);
            }
            //Display prime element
            for (i = 1; i < limit; ++i)
            {
                if (sieve[i] == 0)
                {
                    if (((i * 2) + 1) > int.MaxValue)
                    {
                        throw new OverflowException("too big to downscale to an int");
                    }
                    primes.Add((int)((i * 2) + 1));
                }
            }
            return primes.ToArray();
        }
        /// <summary>
        /// gets an array of bools of size n + 1. a true at index Y in this 
        /// array would mean that Y is prime. A false at index Y means that
        /// Y is not prime
        /// </summary>
        internal static bool[] GetPrimesUpToNAsBoolArray(int n)
        {
            // todo: make this create its own sieve. The end of the sieve converts from
            // an arary of bools to an array of ints. Here we are converting it back.
            int[] primesArray = GetPrimesUpToN(n);
            int maxPrime = primesArray[primesArray.Length - 1];
            bool[] bools = new bool[maxPrime + 1];
            foreach(var p in primesArray)
            {
                bools[p] = true;
            }
            return bools;
        }
        /// <summary>
        /// returns a list of all proper divisors ordered least to greatest.
        /// a proper divisor is a number less than n which divide evenly into n
        /// </summary>
        internal static long[] GetProperDivisors(long n)
        {
            long[] factors = GetFactors(n);
            long[] properDivisors = factors
                .Where(x => x != n)
                .OrderBy(y => y)
                .ToArray();
            return properDivisors;
        }
        internal static long GetSumOfSquares(long n)
        {
            long sum = 0;
            for (long i = 1; i <= n; i++)
            {
                sum += (long)Math.Pow(i, 2);
            }
            return sum;
        }
        internal static long GetSquareOfSum(long n)
        {
            long sum = 0;
            for (long i = 1; i <= n; i++)
            {
                sum += i;
            }
            return (long)Math.Pow(sum, 2);
        }
        internal static bool IsAmicableNumber(long n)
        {
            // using a and b makes it easier to think through the logic
            long a = n;
            long b = 0;

            long[] properDivisorsA = GetProperDivisors(a);
            long sumOfDivisorsA = properDivisorsA.Sum();

            b = sumOfDivisorsA;

            long[] properDivisorsB = GetProperDivisors(b);
            long sumOfDivisorsB = properDivisorsB.Sum();

            if (a == b) return false;

            if (sumOfDivisorsB == a) return true;

            return false;
        }
        /// <summary>
		/// tells you if a number is a circular prime. https://en.wikipedia.org/wiki/Circular_prime
		/// </summary>
		/// <param name="primeToCheck">A number that you already know is prime</param>
		/// <param name="primes">array of ints you already know are primes</param>
		/// <returns></returns>
		internal static bool IsCircularPrime(long primeToCheck, long[] primes)
        {
            if (primeToCheck == 2) return true; // the lower part of this algorithm would throw out 2
            if (primeToCheck == 5) return true; // the lower part of this algorithm would throw out 2

            // turn the prime to check into an array of digits
            char[] primeToCheckAsChars = primeToCheck.ToString().ToCharArray();
            int[] oldArray = new int[primeToCheckAsChars.Length];
            for (int i = 0; i < primeToCheckAsChars.Length; i++)
            {
                oldArray[i] = int.Parse(primeToCheckAsChars[i].ToString());
            }

            // throw out any number with a 2, 4, 6, 8, 0, or 5 in it
            // this is because any number with a 2, would have a
            // rotation with 2 as the last number and any number 
            // with 2 as its last digit is divisible by 2. Same logic
            // for 4, 6, 8, or 0. Any number with a 5 in it would be
            // divisible by 5
            if (oldArray.Contains(2)) return false;
            if (oldArray.Contains(4)) return false;
            if (oldArray.Contains(6)) return false;
            if (oldArray.Contains(8)) return false;
            if (oldArray.Contains(0)) return false;
            if (oldArray.Contains(5)) return false;

            // still here? create the rotations
            for (int i = 1; i < oldArray.Length; ++i)
            {
                // 1 9 9 3 7
                // 7 1 9 9 3
                // 3 7 1 9 9
                // 9 3 7 1 9
                // 9 9 3 7 1
                int[] newArray = new int[oldArray.Length];
                // take the last number and make it the first number
                newArray[0] = oldArray[oldArray.Length - 1];
                // now move forward and take the j + 1 value and put it in the j position
                for (int j = 1; j < newArray.Length; ++j)
                {
                    newArray[j] = oldArray[j - 1];
                }
                // check for prime of the new array
                int newArrayAsNumber = ConvertIntArrayToInt(newArray);
                if (!primes.Contains(newArrayAsNumber)) return false;
                // make the old array the new array
                oldArray = newArray;
            }
            return true;
        }
        public static bool IsComposite(long n)
        {
            if (n == 0) return false;
            if (n == 1) return false;
            for (long i = 2; i < n; i++)
            {
                if (n % i == 0) return true;
            }
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
        internal static bool IsIntPalindromic(BigNumber n)
        {
            int numberOfDigits = n.digits.Length;
            int halfWayPoint = (numberOfDigits / 2) + 1;

            for (int i = 0; i < halfWayPoint; i++)
            {
                int checkLeft = n.digits[i];
                int checkRight = n.digits[numberOfDigits - i - 1];
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
            int[] digits = ConvertLongToIntArray(n);
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
        internal static bool IsPentagonal(long n, bool isGeneral = false)
        {
            // https://www.divye.in/2012/07/how-do-you-determine-if-number-n-is.html
            
            long x = 1 + (24 * n);
            if (IsPerfectSquare(x))
            {
                if (isGeneral) return true;
                if (Math.Sqrt(x) % 6 == 5)
                {
                    return true;
                }
            }
            return false;
        }
        internal static bool IsPerfectSquare(long n)
        {
            double result = Math.Sqrt(n);
            return (result % 1 == 0);
        }
        internal static bool IsPrime(long x)
        {
            if (x == 1) return false;
            if (x == 2) return true;

            const long bigNumber = -1;// 1000000;
            /*
			 * https://math.stackexchange.com/questions/663736/how-to-determine-if-a-large-number-is-prime
			 * To test if some x is prime, we generally have to do divisibility 
			 * tests only up to and including √x. That's because if some y > √x
			 * were a factor of x, then there would have to be some z such that 
			 * zy = x. And z<√x because if z>√x, then clearly zy>x (as both z 
			 * and y would be greater than √x). But if z<√x, then we've already 
			 * tested z in going up to √x!
			 * */
            long largestValueToCheck = x;
            // as the numbers get big, only check the square root of the number
            if (x > bigNumber) largestValueToCheck = (long)Math.Ceiling(Math.Sqrt(x));
            for (long i = 2; i <= largestValueToCheck; i++)
            {
                if (x % i == 0)
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
        internal static bool IsTriangular(long n)
        {
            // https://en.wikipedia.org/wiki/Triangular_number
            if (IsPerfectSquare(1 + (8 * n))) return true;
            return false;
        }
        /// <summary>
		/// recurrsive function to check if a value is truncatable and prime
		/// splits left and right truncation into two recurrsive paths
		/// </summary>
		/// <param name="valueToCheck">any number; isn't expected to be prime</param>
		/// <param name="primes">a list of pre-known primes</param>
		/// <param name="direction">if null, then go both left and right. 
		/// should be reserved reserved for the top level call</param>
		/// <returns></returns>
		internal static bool IsTruncatableAndPrime(long valueToCheck, long[] primes, Direction? direction = null)
        {
            if (!primes.Contains(valueToCheck)) return false;
            if (valueToCheck < 10) return primes.Contains(valueToCheck);

            int[] digits = ConvertLongToIntArray(valueToCheck);


            // check some easy throw-out values.
            for (int i = 0; i < digits.Length; i++)
            {
                // any occurance of a 4, 6, 8, or 0 will eventually result
                // in a truncated value that is divisible by 2. You cannot
                // say the same for 2 or 5 because they're both prime by 
                // themselves. However, for 2 and 5, if they're in the 
                // middle, you'll eventually get a number that ends in 2
                // that isn't prime
                if (digits[i] == 4 || digits[i] == 6 || digits[i] == 8 || digits[i] == 0) return false;
                if (digits[i] == 2 && i != 0 && i != digits.Length - 1) return false;
                if (digits[i] == 5 && i != 0 && i != digits.Length - 1) return false;
            }


            if (direction == null || direction == Direction.LEFT)
            {
                // shave off the left and check
                int leftCheck = ConvertIntArrayToInt(digits[1..(digits.Length)]);
                if (!IsTruncatableAndPrime(leftCheck, primes, Direction.LEFT)) return false;
            }
            if (direction == null || direction == Direction.RIGHT)
            {
                // shave off the right and check
                int rightCheck = ConvertIntArrayToInt(digits[0..(digits.Length - 1)]);
                if (!IsTruncatableAndPrime(rightCheck, primes, Direction.RIGHT)) return false;
            }

            return true;
        }
        internal static bool IsTruncatablePrime(long primeToCheck, long[] primes)
        {
            if (primeToCheck < 10) return false;
            return IsTruncatableAndPrime(primeToCheck, primes);
        }
    }
}
