using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Problems
{
    internal class Euler0001 : Euler
    {
        
        public Euler0001() : base()
        {
            title = "Multiples of 3 or 5";
            problemNumber = 1;
            PrintTitle();
        }
        
        public override void Run()
        {
            int sum = 0;
            for(int i = 0; i < 1000; i++)
            {
                if (i % 3 == 0 || i % 5 == 0) sum += i;
            }
            PrintSolution(sum.ToString());
        }        
    }
}
