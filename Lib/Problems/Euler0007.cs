﻿using EulerProblems.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib.Problems
{
    public class Euler0007 : Euler
    {
        public Euler0007() : base()
        {
            title = "10001st prime";
            problemNumber = 7;
            
        }
        protected override void Run()
        {
            int n = 10001;
            var primes = CommonAlgorithms.GetFirstNPrimes(n);
            PrintSolution(primes[n - 1].ToString());
            return;
        }

    }
}
