using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Lib.Problems
{
    public class Euler0020 : Euler
    {
        public Euler0020() : base()
        {
            title = "Factorial digit sum";
            problemNumber = 20;
            
        }
        protected override void Run()
        {
            int n = 100;

            BigNumber factorial = CommonAlgorithms.GetFactorialLongForm(n);
            int answer = factorial.digits.Sum(x => x);
            

            PrintSolution(answer.ToString());
            return;
        }        
    }
}
