using EulerProblems.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib.Problems
{
    public class Euler0010 : Euler
    {
        public Euler0010() : base()
        {
            title = "Summation of primes";
            problemNumber = 10;
            
        }
        protected override void Run()
        {
            long x = 2000000;
            long[] primes = CommonAlgorithms.GetPrimesUpToN(x);
            long sum = 0;
            for (int i = 0; i < primes.Length; i++)
            {
                if(primes[i] <= x) sum += primes[i];
            }
            PrintSolution(sum.ToString());
            return;
        }
    }
}
