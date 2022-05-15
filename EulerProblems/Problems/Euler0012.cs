﻿using EulerProblems.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EulerProblems.Problems
{
    internal class Euler0012 : Euler
    {
        public Euler0012() : base()
        {
            title = "Highly divisible triangular number";
            problemNumber = 12;
            PrintTitle();
        }
        public override void Run()
        {
            long i = 1;
            long triangleSum = 0;
            const int victoryNumOfFactors = 500;
                        
            while (true)
            {
                triangleSum += i;
                
                List<long> factors = MathHelper.GetFactorsOfN(triangleSum);
                int numFactors = factors.Count;
                if (numFactors > victoryNumOfFactors)    // the problem says to have more than this many
                {
                    PrintSolution(triangleSum.ToString());
                    return;
                }
                                
                i++;
            }            
        }
    }
}
