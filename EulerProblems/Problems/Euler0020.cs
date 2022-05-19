using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
    internal class Euler0020 : Euler
    {
        public Euler0020() : base()
        {
            title = "Factorial digit sum";
            problemNumber = 20;
            PrintTitle();
        }
        public override void Run()
        {
            int n = 100;

            BigNumber factorial = MathHelper.GetFactorialOfNLongForm(n);
            int answer = factorial.digits.Sum(x => x);
            

            PrintSolution(answer.ToString());
            return;
        }        
    }
}
