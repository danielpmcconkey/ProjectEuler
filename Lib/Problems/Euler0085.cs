//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0085 : Euler
	{
		public Euler0085() : base()
		{
			title = "Counting rectangles";
			problemNumber = 85;
		}
		protected override void Run()
		{
            /*
             * This was fun and offered a reasonable challenge. At the 
             * beginning, I was certain that I'd be able to create a recursive
             * algorithm that would do something like total rectangles you can
             * fit inside a rectangle of width W and height H would equal the
             * total rectangles you could fit inside (W-1 x H) + (W x H-1).
             * 
             * I went into Excel and manually fitted rectangles, only to learn
             * that A) I couldn't find any such relationship; and B) I'm really
             * good at making mistakes at things like this. I'm certain that 
             * there *is* a relationship, as too many of the numbers are sums
             * or multiples of prior numbers. I never found it, though (§).
             * 
             * In the end, I brute forced it. It turns out that calculating the
             * number for each dimension is computationally trivial. My solution
             * runs in less than 30 milliseconds.
             * 
             *   (§) After reading the problem thread, I learned that 
             *       each value is the product of 2 triangle numbers.
             *       Let the triangle function T = n * (n + 1) / 2.
             *       Then, your number of rectangles you can fit is 
             *       T(width) * T(height)
             * 
             * */

            const int target = 2000000;
            var closestToTarget = int.MaxValue;
            var closestWidth = 0;
            var closestHeight = 0;
            for(int width = 1; width < 100; width++)
            {
                for (int height = 1; height <= width; height++)
                {
                    int count = 0;
                    for (int insideWidth = 1; insideWidth <= width; insideWidth++)
                    {
                        for (int insideHeight = 1; insideHeight <= height; insideHeight++)
                        {
                            // how many can you place width-wise?
                            var fitW = width - insideWidth + 1;

                            // how many can you place height-wise?
                            var fitH = height - insideHeight + 1;

                            count += fitW * fitH;

                        }
                    }
                    if(Math.Abs(target - count) < closestToTarget)
                    {
                        closestToTarget = Math.Abs(target - count);
                        closestWidth = width;
                        closestHeight = height;
#if VERBOSEOUTPUT
                        Console.WriteLine("{0}|{1}|{2}", width, height, count); 
#endif
                    }
                }
            }
			int answer = closestWidth * closestHeight;
			PrintSolution(answer.ToString());
			return;
		}
	}
}
