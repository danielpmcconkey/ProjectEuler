//#define VERBOSEOUTPUT
using System.Numerics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0071 : Euler
	{
		public Euler0071() : base()
		{
			title = "Ordered fractions";
			problemNumber = 71;
		}
		protected override void Run() 
		{
			/*
			 * This is another one I struggled with. I first tried a truly 
			 * brute force approach of combining every denominator and every
			 * numerator between 1 and 1MM. I knew that wouldn't work but I 
			 * sort of wanted to see the range of bad I was in. It turns out 
			 * that 1MM times 1MM is 1T and that doing 1T of anything is 
			 * time-consuming. Even if all that was was a simple division. And 
			 * oh yeah. that simple division won't work either because decimal 
			 * precision would never get this done. 
			 * 
			 * So, after thinking through it a bit, and a little trial and 
			 * error, I eventually divised what I thought was a very clever 
			 * algorithm. I knew that the answer was somewhere between 2/5 and 
			 * 3/7. So, to my naive tuchus, I thought I could find the answer 
			 * by first finding the difference between those two (1/35) and 
			 * dividing that by 1MM. I figured this would yield a 
			 * fine-grainedness that could bind my search. So I took 3/7 and
			 * subtracted my grain (1/35 div 1MM). I kept doing this until I 
			 * found one that, when reduced, had a numerator and denominator 
			 * that were both <= 1MM. 
			 * 
			 * It required me to modifiy my fraction classes a good bit, 
			 * though, before I could even get started. I had to create 
			 * subtraction and division functions for one. I'd only had 
			 * multiply and add before. And then I had to create a LongFraction
			 * struct (and copy all of the int functions) because doing such 
			 * operations required putting my numerators and denominators into 
			 * equal terms. This put my fractions up into the long spectrum of 
			 * numbers that I didn't realize until I saw negative numbers as 
			 * the result of integer overflows.
			 * 
			 * So, finally, I had all of my code in place and I tried it out on
			 * the example in the problem statement. 8. Sure enough, my method 
			 * produced 2 over 8. I was super happy. I changed 8 to 1MM and it 
			 * ran in about a second. 267857 over 625000. Awesome!
			 * 
			 * But wrong.
			 * 
			 * Maybe my granularity was off? I set it to 1/35 divided by 2MM. 
			 * Same answer, but it took 11 seconds. 1 / 35 div 10MM also 
			 * yielded the same answer in a little over a minute. I knew that 
			 * the solution checker wasn't wrong, but I couldn't see why my 
			 * answer wasn't working. I re-read the problem's wording VERY 
			 * carefully. Twice. I've been bitten before by spending an extra 
			 * hour trying to debug my code and methodology only to learn that 
			 * I'd misunderstood which piece of the puzzle the Euler problem 
			 * actually wanted me to type in. But no, I'd read it right. I just
			 * thought it wrong.
			 * 
			 * I was suspiciously worried that I needed to try to solve this 
			 * with continued fractions. Problems in the recent past wanted me 
			 * to find fractions that trended toward a specific irrational 
			 * decimal. There may be some merit in that line of thinking, but I 
			 * couldn't figure out how to turn 3/7 into a continued fraction to 
			 * begin with. Back to the drawing board.
			 * 
			 * This was when I found the method that ultimately proved to give 
			 * a valid answer. Take the possible denominators, from 1MM to 1 
			 * and descend through them. For each, find a fraction with that 
			 * denominator that was just under 2/5 and also find a fraction 
			 * with that denominator that was just over 3/7. For example, if my
			 * chosen denominator is 8, the lowest 8ths fraction is 3/8 and the
			 * highest is 4/8. I would then add those to a list of possible 
			 * candidates as well as any 8ths in between. Once I'd done this 
			 * for all denominators, I had a list of all the fractions that 
			 * were between 2/5 and 3/7 (and a few that weren't). 
			 * 
			 * From there, I had to find the greatest of these that was still 
			 * less that 3/7. This required me to create a comparison function
			 * for fractions that didn't rely on decimal conversion. That 
			 * wasn't tough and I eventually found the answer to the problem 
			 * after 5 minutes of run-time. It was the right answer. But I 
			 * could optimize siginificantly by checking candidates starting at
			 * my old version's incorrect answer instead of 2/5. Checking each
			 * denominator's closest proximity to 267857/625000 instead of 2/5
			 * brought runtime down to 1 min 17 sec.
			 * 
			 * I could still do better. I didn't have to start at 
			 * 267857/625000 every time. I could check each fraction for 
			 * whether it was greater than the current champion and use that as
			 * my starting point with every subsequent run. But at that point, 
			 * starting at 267857 / 625000 felt like cheating. So I started at 
			 * 2/5 and my runtime was now 24 seconds. Lower than the 1 minute 
			 * rule, but still too slow. 
			 * 
			 * Well, previously, I'd been checking every denominator's closest
			 * to 2/5 but lower all the way to every denominator's closest to 
			 * 3/7 but higher. What if I switched that? I bet that skips a ton
			 * of fractions. Sure enough, it now runs in 10.7 seconds. But can
			 * we do better? 
			 * 
			 * I thought about trying to determine if we'd hit the right answer
			 * before we continued through the loop. But I put a break in the 
			 * code to test that. This program "finds" the right answer in 
			 * about 10.3 seconds. The remaining time running through potential 
			 * denominators after that takes up about 400ms. Probably because 
			 * my logic above means that any denominator wouldn't have a 
			 * fraction that was both above 2/5 and below 3/7. Duh.
			 * 
			 * So really, the inneficiency left in this code is due to my 
			 * fraction maths. There's probably a lot of time spent in my 
			 * FractionCalculator.Reduce function. But I'm not gonna try. I've 
			 * put in an honest effort. 
			 * 
			 * QED
			 * 
			 * */

			// Run_bruteForce(); // Elapsed time: 77691.532 milliseconds (but really 5 mins if I started at 2/5)
			Run_faster();  // Elapsed time: 10775.2532 milliseconds
		}
		private void Run_faster()
		{
			int dMax = 1000000;
			
			LongFraction threeSevenths = new LongFraction(3, 7);
			LongFraction currentChampion = new LongFraction(2, 5);
			decimal currentChampionAsD = currentChampion.numerator / (decimal)currentChampion.denominator;
			decimal threeSeventhsAsD = 3 / 7.0M;
			for (int d = dMax; d > 0; d--)
			{
				// what's the closest thing (but over) to current champion in this denomination?
				decimal oneOverD = 1 / (decimal)d;
				int start = (int)Math.Ceiling(currentChampionAsD / oneOverD);

				// what's the closest thing (but under) to 3/7 in this denomination?
				int end = (int)Math.Floor(threeSeventhsAsD / oneOverD);

				for (int n = start + 1; n < end; n++)
				{
					LongFraction lf = new LongFraction(n, d);
					FractionCalculator.Reduce(lf);
					if (lf.numerator <= dMax && lf.denominator <= dMax)
					{
						if(lf.CompareTo(currentChampion) > 0 && lf.CompareTo(threeSevenths) < 0)
						{
							// new champion
							currentChampion = lf;
							currentChampionAsD = lf.numerator / (decimal)lf.denominator;							
						}
					}
				}
			}
			long answer = currentChampion.numerator;
			PrintSolution(answer.ToString());
			return;
		}
		private void Run_bruteForce()
		{
			int dMax = 1000000;
			List<LongFraction> candidates = new List<LongFraction>();

			LongFraction threeSevenths = new LongFraction(3, 7);
			LongFraction twoFifths = new LongFraction(2, 5);
			LongFraction currentChampion = new LongFraction(267857, 625000);
			decimal currentChampionAsD = currentChampion.numerator / (decimal)currentChampion.denominator;
			decimal threeSeventhsAsD = 3 / 7.0M;
			for(int d = dMax; d > 0; d--)
			{
				// what's the closest thing (but under) to current champion in this denomination?
				decimal oneOverD = 1 / (decimal)d;
				int start = (int) Math.Floor(currentChampionAsD / oneOverD);

				// what's the closest thing (but over) to 3/7 in this denomination?
				int end = (int)Math.Ceiling(threeSeventhsAsD / oneOverD);

				for(int n = start + 1; n < end; n++)
				{
					LongFraction lf = new LongFraction(n, d);
					FractionCalculator.Reduce(lf);
					if (lf.numerator <= dMax && lf.denominator <= dMax)
					{
						candidates.Add(lf);
					}
				}
			}
			var orderedCandidates = candidates
				.Where(y => y.CompareTo(threeSevenths) < 0)
				.OrderByDescending(x => x);
			var firstLower = orderedCandidates.First();
			

			// winner winner
			long answer = firstLower.numerator;
			PrintSolution(answer.ToString());
			return;
		}	
	}
}
