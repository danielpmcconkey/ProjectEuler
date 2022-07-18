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
            const int limit = 2000000;
            var primes = CommonAlgorithms.GetPrimesUpToN(limit);
            long sum = 0;
            for (int i = 0; i < primes.Length; i++)
            {
                sum += primes[i];
            }           

            PrintSolution(sum.ToString());
            return;
        }
    }
}
