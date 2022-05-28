using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0045 : Euler
	{
		long currentTripVal = 1;
		long currentPentVal = 1;
		long currentHexVal = 1;
		long currentTripN = 1;
		long currentPentN = 1;
		long currentHexN = 1;
		long[] knownWinVals;
		public Euler0045() : base()
		{
			title = "Triangular, pentagonal, and hexagonal";
			problemNumber = 45;
			PrintTitle();
		}
		public override void Run()
		{
			/*
			 * this approach has us increment the triangular, pentagonal, and 
			 * hexagonal numbers to leapfrom one another and check at each leap
			 * whether we have achieved victory. It works and runs incredibly 
			 * fast.
			 * 
			 * this was not my first line of reasoning, though. my first 
			 * thought was to increment up each hexagonal number (since they
			 * go up the fastest) and test each hex number for whether it was
			 * also a pent and also a trip. The first to reach all 3 was my 
			 * winner.
			 * 
			 * However, while I believe that approach is correct, I couldn't 
			 * get it to work. Wikipedia lists means of testing whether a value
			 * is a pentagonal number:
			 * 
			 *      For generalized pentagonal numbers, it is sufficient 
			 *      to just check if 24x + 1 is a perfect square.
			 *      
			 *      For non-generalized pentagonal numbers, in addition to
			 *      the perfect square test, it is also required to check 
			 *      if the square root of (24x + 1) mod 6 = 5
			 *      
			 * I never got this to work. The generalized set uses negative 
			 * numbers for N and thus not in bounds for problem 45. And I never
			 * found a number that passed both of these tests. I'll revisit my
			 * implementation tomorrow. For now, I'm pleased with how this 
			 * runs.
			 * 
			 * */
			knownWinVals = new long[] { 1, 40755 }; // win states that are <= 40755
			while (true)
            {
				if (isWinState())
				{
					PrintSolution(currentTripVal.ToString());
					return;
				}
				incrementTrip();
				if (isWinState())
				{
					PrintSolution(currentTripVal.ToString());
					return;
				}
				incrementPent();
				if (isWinState())
				{
					PrintSolution(currentTripVal.ToString());
					return;
				}
				incrementHex();				
			}
		}
		private void incrementTrip()
		{
			do
			{
				currentTripN++;
				currentTripVal = (long)(currentTripN * ((currentTripN + 1) / 2d));
			}
			while (currentTripVal < currentPentVal || currentTripVal < currentHexVal);
		}
		private void incrementPent()
		{
			do {
				currentPentN++;
				currentPentVal = (long)(currentPentN * (((3 * currentPentN) - 1) / 2d));
			}
			while (currentPentVal < currentTripVal || currentPentVal < currentHexVal) ;
		}
		private void incrementHex()
		{
			do {
				currentHexN++;
				currentHexVal = currentHexN * ((2 * currentHexN) - 1);
			}
			while (currentHexVal < currentTripVal || currentHexVal < currentPentVal) ;
		}
		private bool isWinState()
        {
			if (currentTripVal == currentPentVal && currentPentVal == currentHexVal)
			{
				if (!knownWinVals.Contains(currentHexVal))
				{
					return true;
				}
			}
			return false;
		}

	}
}
