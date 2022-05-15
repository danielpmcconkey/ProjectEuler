using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Problems
{
    internal  class Euler0004 : Euler
    {
        public Euler0004() : base()
        {
            title = "Largest palindrome product";
            problemNumber = 4;
            PrintTitle();
        }
        public override void Run()
        {
            int lowest3DigitNumber = 100;
            int highest3DigitNumber = 999;
            int highestPalindromicNumber = -1;

            for(int i = lowest3DigitNumber; i <= highest3DigitNumber; i++)
            {
                for (int j = lowest3DigitNumber; j <= highest3DigitNumber; j++)
                {
                    int candidate = i * j;
                    if(Lib.WeirdAlgorithms.isIntPalindromic(candidate))
                    {
                        if(candidate > highestPalindromicNumber)
                            highestPalindromicNumber = candidate;
                    }
                }
            }
            PrintSolution(highestPalindromicNumber.ToString());
            return;
        }
        
    }
}
