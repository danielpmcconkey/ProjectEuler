using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Problems
{
    internal class Euler0003 : Euler
    {
        public Euler0003() : base()
        {
            title = "Largest prime factor";
            problemNumber = 3;
        }
        public override void Run()
        {
            long initialValue = 600851475143;

            /*
             * start going up from 2 and see if it divides evenly
             * if it does, then 2 is a factor, but so is our big number
             * divided by 2. If *that* number is prime, it's our biggest
             * prime factor. If not, move from 2 to 3 and run the same
             * check. The first "opposite" factor that is also
             * prime is our answer
             * */

            bool isSolved = false;
            long checkLowFactor = 2;

            while (isSolved == false)
            {
                if(initialValue % checkLowFactor == 0)
                {
                    long highFactor = initialValue / checkLowFactor;
                    if(Lib.PrimeHelper.IsXPrime(highFactor))
                    {
                        isSolved = true;
                        PrintSolution(highFactor.ToString());
                        return;
                    }
                }
                checkLowFactor++;
            }
        }
    }
}
