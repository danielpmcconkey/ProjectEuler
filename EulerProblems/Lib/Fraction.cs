using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    public class Fraction
    {
        public int numerator;
        public int denominator;

        public Fraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }
        public void Reduce()
        {
            // greatest common factor method
            int[] numeratorFactors = CommonAlgorithms.GetFactors(numerator);
            int[] denominatorFactors = CommonAlgorithms.GetFactors(denominator);

            int gcf = 0;
            Array.Sort(numeratorFactors);
            for(int i = numeratorFactors.Length - 1; i >= 0; i--)
            {
                if(denominatorFactors.Contains(numeratorFactors[i]))
                {
                    gcf = numeratorFactors[i];
                    break;
                }
            }

            // divide each number by the gcf
            numerator /= gcf;
            denominator /= gcf;
        }
    }
}
