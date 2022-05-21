using EulerProblems.Lib;
using System.Numerics;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
	internal class Euler0025 : Euler
	{
		public Euler0025() : base()
		{
			title = "1000-digit Fibonacci number";
			problemNumber = 25;
			PrintTitle();
		}
		public override void Run()
		{
			//Run_bruteForce();
			Run_elegant();
		}
		private void Run_elegant()
        {
			/* 
			 * see https://www.mathblog.dk/project-euler-25-fibonacci-sequence-1000-digits/
			 * so, the fibonacci sequence trends toward F(n) = ((phi ^ n)/squareRootOf5) where
			 * phi is the golden ratio (~1.6180). Uncomment the code below to see how close
			 * it gets

			double squareRootOf5 = Math.Sqrt(5);
			double phi = (( 1 + squareRootOf5) / 2);
			for(int n= 1; n < 100; n++)
            {
				double fibonacciTrend = (Math.Pow(phi, n) / squareRootOf5);
				Console.WriteLine(string.Format("{0}{1}", n.ToString().PadRight(10), fibonacciTrend.ToString("#,##0.00").PadLeft(30)));
			}

			 * so, assuming the first Fibonacci number with 1000 digits is large enough to
			 * converge, then we need an inequality set up as such:
			 * 
			 *                (phi ^ n)
			 *              -------------         >        (10 ^ 999)
			 *              squareRootOf5 
			 *      
			 * solve the inequality and you'll get n > ~answer. find the first whole number 
			 * that is greater than your approximate answer and you win the nerd games.
			 * 
			 * so how do we solve that? we have to remember high school, or google high
			 * school, and apply the fact that logs cancel out exponents. Take the log
			 * on both sides.
			 * 
			 *        /               \                   /              \
			 *        |   (phi ^ n)    |                  |              |
			 * log of | -------------  |     >     log of |  (10 ^ 999)  |
			 *        | squareRootOf5  |                  |              |
			 *        \               /                   \              /
			 * 
			 * Apply the logarithms rule that states log_b(a^c) = c * log_b(a)^1
			 * 
			 * n * log(phi) - ((1/2) * log(5))      >       999 * log(10)
			 * 
			 * 
			 * now we want to isolate n start by adding ((1/2) * log(5)) to both sides
			 * 
			 * n * log(phi)      >       999 * log(10) + ((1/2) * log(5))
			 * 
			 * now divide by log(phi)
			 * 
			 *                         999 * log(10) + ((1/2) * log(5))
			 *          n       >     ---------------------------------- 
			 *                                     log(phi)
			 * */

			double squareRootOf5 = Math.Sqrt(5);
			double phi = ((1 + squareRootOf5) / 2);

			double nApproximate = (999 * Math.Log10(10) + (0.5 * Math.Log10(5))) / (Math.Log10(phi));

			var answer = Math.Ceiling(nApproximate);
			PrintSolution(answer.ToString());
		}
		private void Run_bruteForce()
        {
			BigNumber penultimate = new BigNumber(0);
			BigNumber ultimate = new BigNumber(1);
			int position = 2;
			while(true)
            {
				BigNumber currentAnswer = BigNumberCalculator.Add(penultimate, ultimate);
				if(currentAnswer.digits.Length > 999)
                {
					PrintSolution(position.ToString());
					return;
				}
				// Console.WriteLine(string.Format("{0}{1}", position.ToString().PadRight(10), currentAnswer.ToString()));
				penultimate = ultimate;
				ultimate = currentAnswer;
				position++;
            }			
		}
		

	}
}
