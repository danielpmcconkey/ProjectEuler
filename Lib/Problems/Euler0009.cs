using EulerProblems.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib.Problems
{
    public class Euler0009 : Euler
    {
        public Euler0009() : base()
        {
            title = "Special Pythagorean triplet";
            problemNumber = 9;
            
        }
        protected override void Run()
        {
            //Run_slow(); // 7428.503 milliseconds
            Run_fast(); // 2.6781 milliseconds
        }
        protected void Run_fast()
        {
            const int finalSumExpectation = 1000;
            // making an assumption that a + b is always bigger than c. I
            // couldn't find any examples of pythagorean triples that proved me
            // wrong
            int largestSquareToConsider = (int)Math.Pow(Math.Floor(finalSumExpectation * 0.5f),2);
            List<(int, int)> squares = new List<(int, int)>();
            bool isBigEnough = false;
            for (int i = 1; !isBigEnough; i++)
            {
                int squaredVal = (int)Math.Pow(i, 2);
                if (squaredVal <= largestSquareToConsider)
                {
                    squares.Add((i, squaredVal));
                }
                else isBigEnough = true;
            }
            // now create a bool array to make it easier to tell if a number is a perfect square
            bool[] perfectSquaresAsBools = new bool[largestSquareToConsider + 1];
            foreach (var s in squares)
            {
                perfectSquaresAsBools[s.Item2] = true;
            }
            for (int i = squares.Count - 1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    int a = squares[j].Item1;
                    int b = squares[i].Item1;
                    int aSq = squares[j].Item2;
                    int bSq = squares[i].Item2;
                    int aSqPlusBSq = aSq + bSq;
                    if(perfectSquaresAsBools.Length > aSqPlusBSq && perfectSquaresAsBools[aSqPlusBSq])
                    {
                        // we have a pythagorean triangle. let's see if they add to 1000
                        int c = (int)Math.Sqrt(aSqPlusBSq);
                        if (a + b + c == finalSumExpectation)
                        {
                            int product = a * b * c;
                            PrintSolution(product.ToString());
                            return;
                        }
                    }
                }
            }
        }
        protected void Run_slow()
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
                        if(CommonAlgorithms.IsPythagoreanTriplet(a, b, c))
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
