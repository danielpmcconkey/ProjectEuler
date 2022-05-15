using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Problems
{
    internal class Euler0002 : Euler
    {
        public Euler0002() : base()
        {
            title = "Even Fibonacci numbers";
            problemNumber = 2;
            PrintTitle();
        }
        public override void Run()
        {
            int sum = 2;
            bool isLessThan4Million = true;
            int i = 1;
            int j = 2;
            while (isLessThan4Million)
            {
                int iPlusJ = i + j;
                if (j < 4000000)
                {
                    if (iPlusJ % 2 == 0)
                    {
                        sum += iPlusJ;
                    }
                    i = j;
                    j = iPlusJ;
                }
                else isLessThan4Million = false;
                // Console.WriteLine(String.Format("i: {0}; j: {1}; sum: {2}", i, j, sum));
            }
            PrintSolution(sum.ToString());
        }
    }
}
