﻿using EulerProblems.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Problems
{
    internal class Euler0005 : Euler
    {
        private int[] _denominators = {
                    19,
                    18,
                    17,
                    16,
                    15,
                    14,
                    13,
                    12,
                    11
                    // 10 not needed because 20
                    // 9 not needed because 18
                    // 8 not needed because 16
                    // 7 not needed because 14
                    // 6 not needed because 12
                    // 5 not needed because 15
                    // 4 not needed because 12
                    // 3 not needed because 12
                    // 2 not needed because 12
                    // 1 not needed because 1
                };
        public Euler0005() : base()
        {
            title = "Smallest multiple";
            problemNumber = 5;
            PrintTitle();
        }
        public override void Run()
        {
            PrintSolution(SmallestByPrimeFactors(20).ToString());
            return;

            long numerator = 20;

            while (true)
            {
                if (isASolution(numerator))
                {
                    // we've got a solution
                    PrintSolution(numerator.ToString());
                    return;
                }
                numerator += 20;
            }
            
        }
        private bool isASolution(long numerator)
        {
            foreach (int denominator in _denominators)
            {
                if (numerator % denominator != 0)
                {
                    return false;
                }
            }
            return true;
        }

        long SmallestByPrimeFactors(int maxFactor)
        {
            Dictionary<int, int> factors = new Dictionary<int, int>();
            int[] primes = PrimeHelper.GetPrimesUpToN(20);

            foreach (int prime in primes)
            {
                factors[prime] = 0;
            }

            // For each number up to 20
            for (int number = 2; number <= maxFactor; number++)
            {
                int workingNumber = number;

                // Get its prime factorization
                for (int primeIndex = 0; primeIndex < primes.Length; primeIndex++)
                {
                    int numOfThisPrime = 0;

                    // For each prime, while divisible, keep track of that
                    while (workingNumber % primes[primeIndex] == 0)
                    {
                        workingNumber /= primes[primeIndex];
                        numOfThisPrime++;
                    }

                    // If this prime was used more than previously
                    if (numOfThisPrime > factors[primes[primeIndex]])
                    {
                        factors[primes[primeIndex]] = numOfThisPrime;
                        //Console.WriteLine("Prime " + primes[primeIndex] + " now occurs " + numOfThisPrime + " times!");
                    }
                }
            }

            // Calculate Result
            double result = 1;
            foreach (int key in factors.Keys)
            {
                result *= Math.Pow(key, factors[key]);
            }
            return (long)result;
        }
    }
}
