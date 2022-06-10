//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0064 : Euler
	{
		public Euler0064() : base()
		{
			title = "Odd period square roots";
			problemNumber = 64;
		}
		protected override void Run()
		{
			/*
			 * This one kicked my ass. First off, I hate these close 
			 * approximation of irrational numbers fractions. I just don't 
			 * think that way, it seems. But the real problem is that there's
			 * just not a lot of "how to" out there on the internet that's 
			 * approachable. I first googled something like "continued fraction
			 * of a square root" and got to a great answer on the math stack 
			 * exchange.
			 * 
			 * https://math.stackexchange.com/questions/265690/continued-fraction-of-a-square-root
			 * 
			 * It walked through a great technique for building the 
			 * coefficients these are the a_0, a_1, etc. that I ended up 
			 * calling "alphas" in my code. But it didn't tell me how to know
			 * when my repeat happened. 
			 * 
			 * So then I googled "continued fraction of a square root repeat" 
			 * and arrived at a fantastic blog post from a math nerd.
			 * 
			 * https://www.johndcook.com/blog/2020/08/04/continued-fraction-sqrt/
			 * 
			 * This told me that when my coefficient was twice a_0 and when the
			 * previous coefficients formed a pallindrome, Bob ought to be my
			 * uncle. This looked great. I thought I had everything I needed 
			 * and I was off to coding. It worked great and I was able to match
			 * all of the samples in the problem and all of the samples in John
			 * Cook's blog. So I ran it for all non-squares up to 10,000.
			 * 
			 * And it ran forever. On n-139, it never found a pallindrome 
			 * followed by a coefficient that was twice the a_0 value. A little
			 * tinkering showed me that the precision of floating point 
			 * decimals just wasn't up to the task and, somewhere around a_12, 
			 * it all unraveled. I felt like I was back to the drawing board.
			 * 
			 * And so then I started googling everything I could about square
			 * roots and continued fractions. I found plenty about how to solve
			 * and continued fraction. I found how to represent a fraction as a 
			 * continued one. But very little on how to form one from an 
			 * irrational decimal.
			 * 
			 * Things were promising after I found the wikipedia on periodic 
			 * continued fractions and a formula under the Canonical form and 
			 * repetend section
			 * 
			 * https://en.wikipedia.org/wiki/Periodic_continued_fraction#Canonical_form_and_repetend
			 * 
			 * But this didn't actually work. Using their Sqrt(114) example, my
			 * a_2 never matched theirs and I couldn't figure out why. But they
			 * had a citation for their algorithm and that was my payday. It 
			 * needed the wayback machine, but here it was.
			 * 
			 * https://web.archive.org/web/20151221205104/http://web.math.princeton.edu/mathlab/jr02fall/Periodicity/mariusjp.pdf
			 * 
			 * Super think math that went entirely over my head. They even have
			 * c code in the appendix for calculating the length of the 
			 * coefficients array. I adapted that code after making sure I 
			 * understood it well enough and used it to create the 
			 * CommonAlgorithms.GetContinuedFractionOfSquareRootOfN function 
			 * referenced used in the solution.
			 * 
			 * */

			// get all the perfect squares below 10,000
			const int limit = 10000;
			bool[] perfectSquareBools = new bool[limit + 1];
			for(int i = 2; true; i++)
            {
				int square = i * i;
				if (square > limit) break;
				perfectSquareBools[square] = true;
            }

			int answer = 0;
			for(int n = 2; n <= limit; n++)
            {
				if (perfectSquareBools[n]) continue;
				var continuedFraction = CommonAlgorithms.GetContinuedFractionOfSquareRootOfN(n);
				int repeatLength = continuedFraction.repeatingAlphas.Length;
				if (repeatLength % 2 == 1)
				{
					answer++;
				}
			}

			PrintSolution(answer.ToString());
			return;
		}		
	}
}
