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
