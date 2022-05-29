using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0044 : Euler
	{
		public Euler0044() : base()
		{
			title = "Pentagon numbers";
			problemNumber = 44;
			PrintTitle();
		}
		public override void Run()
		{
			const int howManyToGenerate = 10000;
			// first generate an array of all the pentagonal numbers
			var pentagonalNumbers = CommonAlgorithms.GetFirstNPentagonalNumbers(howManyToGenerate);
			// because Array.Contains is slow, turn that into an array of bools 0 to the
			// largest pentagonal you just generated. Only the array indices of
			// pentagonal numbers will be true
			long largestPentagonalNumber = pentagonalNumbers[pentagonalNumbers.Length - 1];
			bool[] pentagonalBools = new bool[largestPentagonalNumber + 1];
			foreach(var p in pentagonalNumbers) pentagonalBools[p] = true;
			
			// Now go through every combo of pentagonal numbers.
			// I didn't know how many I had to go up to. adding a variable maximum was
			// a way to gradually expand without previously knowing how large the lists 
			// had to be. In fact, I ran it a few times and already knew that the list
			// only needs to be about 3k in length. I'm certain I could think through
			// how to not restart i and j at 0 with every expansion. But that's hard and
			// I want beer. This runs plenty fast.

			int variableMax = 1000; 
			while (variableMax <= howManyToGenerate)
			{
				for (int i = 0; i < variableMax; i++)
				{
					for (int j = 0; j < variableMax; j++)
					{
						long p1 = pentagonalNumbers[i];
						long p2 = pentagonalNumbers[j];
						long sum = p1 + p2;
						long diff = Math.Abs(p2 - p1);

						if (pentagonalBools[sum])
						{
							if (pentagonalBools[diff])
							{
								// winner, winner, pork chop dinner
								PrintSolution(diff.ToString());
								return;
							}
						}
					}
				}
				variableMax += 1000;
			}			
		}		
	}
}
