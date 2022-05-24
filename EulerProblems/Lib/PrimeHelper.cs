using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    internal static class PrimeHelper
    {
		internal static long[] GetFirstNPrimes(int n)
        {
			long[] primes = new long[n];
			primes[0] = 2; // add 2 manually so I can easily skip all even numbers moving forward
			int numPrimesFound = 1;
			long i = 3;
			while(true)
            {
				if(IsXPrime(i))
                {
					numPrimesFound++;
					primes[numPrimesFound -1] = i;
					if(numPrimesFound == n) return primes;
				}
				i++;
            }
			throw new Exception("n number of primes not found");
		}
		internal static List<long> GetPrimesUpToX(long x)
        {
			// this will take a long time to run. Use the integer function if you can help it
			List<long> primes = new List<long>();

			if (x >= 2)
			{
				primes.Add(2);
			}
			for (long i = 3; i < x; i += 2)
            {
				if (IsXPrime(i))
				{
					primes.Add(i);
				}
				// if((i-1) % 1000 == 0) Console.WriteLine(String.Format("primes found up to {0}", i));
            }
			return primes;
        }
		internal static List<int> GetPrimesUpToX(int x)
		{
			// https://www.baeldung.com/cs/prime-number-algorithms
			// https://kalkicode.com/sieve-of-sundaram
			// note this only works for relatively small numbers due to the limits
			// of array sizes
			List<int> primes = new List<int>();
			if (x <= 1)
			{
				//When n are invalid to prime number 
				return primes;
			}
			//Calculate the number of  prime of given n
			int limit = ((x - 2) / 2) + 1;
			//This are used to detect prime numbers
			int[] sieve = new int[limit];
			// Loop controlling variables
			int i = 0;
			int j = 0;
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
			if (x >= 2)
			{
				primes.Add(2);
			}
			//Display prime element
			for (i = 1; i < limit; ++i)
			{
				if (sieve[i] == 0)
				{
					primes.Add((i * 2) + 1);
				}
			}
			return primes;
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
			for(int i = 1; i < oldArray.Length; ++i)
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
				for(int j = 1; j < newArray.Length; ++j)
                {
					newArray[j] = oldArray[j - 1];
				}
				// check for prime of the new array
				int newArrayAsNumber = MathHelper.ConvertIntArrayToInt(newArray);
				if (!primes.Contains(newArrayAsNumber)) return false;
				// make the old array the new array
				oldArray = newArray;
			}
			return true;
		}
		internal static bool IsXPrime(long x)
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
	}
}
