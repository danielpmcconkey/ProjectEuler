using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0045 : Euler
	{
		
		public Euler0045() : base()
		{
			title = "Triangular, pentagonal, and hexagonal";
			problemNumber = 45;
			PrintTitle();
		}
		public override void Run()
        {
			// two ways to solve. both take about the same time


			//Run_incrementing();     // runs in 11ms
			Run_checking();	// runs in 14 ms
		}
		public void Run_checking()
        {
			/* according to wikipedia
			 *      Every hexagonal number is a triangular number
			 * 
			 * so we only need to increment every hexagonal number and check to 
			 * see if it is also pentagonal. The first that is (after the 
			 * initial 40755 given in the problem) is our answer.
			 * 
			 * */

			int n = 144;
			while (true)
            {
				long h_n = n * ((2 * n) - 1);
				if(CommonAlgorithms.IsPentagonal(h_n))
                {
					PrintSolution(h_n.ToString());
					return;
				}
				n++;
            }
        }
		public void Run_incrementing()
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
			long currentTripVal = 40755;
			long currentPentVal = 40755;
			long currentHexVal = 41328; // the next value after the problem starts
			long currentTripN = 285;
			long currentPentN = 165;
			long currentHexN = 144; // the next value after the problem starts
			bool isProblemSolved = false;

			while (!isProblemSolved)
            {
				// check for win
				if (currentTripVal == currentPentVal && currentPentVal == currentHexVal)
				{
					isProblemSolved = true;
					break;
				}
				// increment the trip
				do
				{
					currentTripN++;
					currentTripVal = (long)(currentTripN * ((currentTripN + 1) / 2d));
				}
				while (currentTripVal < currentPentVal || currentTripVal < currentHexVal);
				
				
				// check for win
				if (currentTripVal == currentPentVal && currentPentVal == currentHexVal)
				{
					isProblemSolved = true;
					break;
				}
				// increment the pent
				do
				{
					currentPentN++;
					currentPentVal = (long)(currentPentN * (((3 * currentPentN) - 1) / 2d));
				}
				while (currentPentVal < currentTripVal || currentPentVal < currentHexVal);
				
				
				// check for win
				if (currentTripVal == currentPentVal && currentPentVal == currentHexVal)
				{
					isProblemSolved = true;
					break;
				}
				// increment the hex
				do
				{
					currentHexN++;
					currentHexVal = currentHexN * ((2 * currentHexN) - 1);
				}
				while (currentHexVal < currentTripVal || currentHexVal < currentPentVal);
			}
			PrintSolution(currentTripVal.ToString());
			return;
		}
	}
}
