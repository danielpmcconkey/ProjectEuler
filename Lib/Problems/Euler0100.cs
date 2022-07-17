//#define VERBOSEOUTPUT
using System.Numerics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0100 : Euler
	{
		public Euler0100() : base()
		{
			title = "Arranged probability";
			problemNumber = 100;
		}
        protected override void Run() 
        {
            /* 
             * This was a great challenge. Just enough math. Some fun with 
             * decimal precision. A ridiculously large target answer that
             * forced creativity. I liked it a lot. And, it's the 100th 
             * problem. Here is where I stop. For now.
             * 
             * As per usual, I started with brute force, but never got anywhere
             * close to the right answer. But it did show me a couple of 
             * things:
             * 
             *    1) The ratio of blue discs to total discs will be somewhere 
             *    close to 70.7106%. That fact ultimately didn't get used, but 
             *    it's good to know.
             *    
             *    2) The multiple between each successive 50% odds solution 
             *    approaches 5.8284271... That fact did get used.
             * 
             * The decimal precision bothered me for a while. I went into 
             * long-hand pencil and paper and figured that for every whole 
             * integer representing blue discs (let's call that y), you could
             * solve the total discs (called x) as:
             * 
             *    y + 0.25 = (x - 0.5)^2
             *    
             * Or, said another way:
             * 
             *    x = sqrt((((y ^2) - y) * 2) + 0.25)
             *    
             * Test x for being a whole integer and you have a solution. The 
             * problem is the damned square root. My math isn't good enough to
             * factor it out and square roots invoke floating point arithmetic 
             * that will drift toward imprecision at such large numbers.
             * 
             * So I decided to "back" check my findings by performing a 
             * long-hand style calculation after I identified a probably hit.
             * This worked, but still took way too long to find a solution 
             * above 1e12. 
             * 
             * So that's when I invoked finding #2 above. The trick was that, 
             * until I'd tightened up my algorithm, I wasn't certain that the 
             * 5.82... multiplier was true. Prior versions had skipped 
             * generations so it looked like I had a 30x multiplier at random
             * times. But once I got it really tight, I saw that the 5.82... 
             * multiplier stayed true and I was able to skip checking a lot of
             * potential Y values.
             *    
             * Very fun.
             * 
             * */

            long target = (long)1e12;
            long start = 15;
            double jumpMultiplier = 5;
            long lastY = 3;
            for (long y = start; true; y++)
            {
                double xEquivalent = ((((double)y * y) - y) * 2) + 0.25;
                double x = Math.Sqrt(xEquivalent) + 0.5;
                
                if (CommonAlgorithms.IsInteger(x))
                {
                    // back check it

                    BigInteger f1Numerator = y;
                    BigInteger f1Denominator = (long)x;
                    BigInteger f2Numerator = f1Numerator - 1;
                    BigInteger f2Denominator = f1Denominator - 1;
                    BigInteger productNumerator = f1Numerator * f2Numerator * 2;
                    BigInteger productDenominator = f1Denominator * f2Denominator;
                    if (productNumerator == productDenominator)
                    {
                        jumpMultiplier = y / (double)lastY;                        
                        lastY = y;
                        var nextY = (long)Math.Floor(y * (jumpMultiplier)) - 1;

#if VERBOSEOUTPUT
                        Console.WriteLine("{0}	{1}	{2}	{3}	{4}", x, y, lastY, jumpMultiplier, nextY); 
#endif
                        if (x >= target)
                        {
                            PrintSolution(y.ToString());
                            return;
                        }
                        y = nextY;
                    }
                }
            }
        }
	}
}
