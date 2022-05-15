using EulerProblems.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Problems
{
    internal class Euler0006 : Euler
    {
        public Euler0006() : base()
        {
            title = "Sum square difference";
            problemNumber = 6;
            PrintTitle();
        }
        public override void Run()
        {
            long limit = 100;
            long sumOfSquares = WeirdAlgorithms.SumOfSquares(limit);
            long squareOfSum = WeirdAlgorithms.SquareOfSum(limit);

            long difference = squareOfSum - sumOfSquares;
            PrintSolution(difference.ToString());
            return;
        }

    }
}
