using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
    internal class Euler0016 : Euler
    {
        public Euler0016() : base()
        {
            title = "Power digit sum";
            problemNumber = 16;
            PrintTitle();
        }
        public override void Run()
        {
            int finalExponent = 1000;
            BigNumber valueAsString = new BigNumber(new int[] { 2 });
            for(int currentExponent = 2; currentExponent <= finalExponent; currentExponent++)
            {
                valueAsString = BigNumberCalculator.Add(valueAsString, valueAsString);
            }

            // now that we have the 2 ^ n result, sum up the digits
            long sumOfDigits = 0;
            
            for(int i = 0; i < valueAsString.digits.Length; i++)
            {
                sumOfDigits += Int16.Parse(valueAsString.digits[i].ToString());
            }

            PrintSolution(sumOfDigits.ToString());
            return;
        }
        

        
    }
}
