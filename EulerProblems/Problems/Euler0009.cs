using EulerProblems.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Problems
{
    internal class Euler0009 : Euler
    {
        public Euler0009() : base()
        {
            title = "Special Pythagorean triplet";
            problemNumber = 9;
            PrintTitle();
        }
        public override void Run()
        {
            const int finalSumExpectation = 1000;
            // get a list of the first 1000 squares
            Dictionary<int,int> squares = new Dictionary<int, int>();
            for(int i = 0; i < finalSumExpectation; i++)
            {
                int thisSquare = (int)Math.Pow(i, 2);
                squares.Add(i, thisSquare);
            }
            // now go through each combination knowing c > b > a
            // might be more efficient if we go from greatest to least
            // but this is easier to think through
            for(int a = 0; a < squares.Count; a++)
            {
                for (int b = a + 1; b < squares.Count; b++)
                {
                    for (int c = b + 1; c < squares.Count; c++)
                    {
                        if(WeirdAlgorithms.IsPythagoreanTriplet(a, b, c))
                        {
                            if(a + b + c == finalSumExpectation)
                            {
                                int product = a * b * c;
                                PrintSolution(product.ToString());
                                return;
                            }
                        }
                    }
                }
            }            
        }
    }
}
