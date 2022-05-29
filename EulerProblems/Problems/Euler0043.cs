using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0043 : Euler
	{
		public Euler0043() : base()
		{
			title = "Sub-string divisibility";
			problemNumber = 43;
			PrintTitle();
		}
		public override void Run()
		{
			int[] numerals = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			int[][] permutations = CommonAlgorithms.GetAllPermutationsOfArray(numerals);
			//int[][] permutations = new int[1][];
			//permutations[0] = new int[] { 1, 4, 0, 6, 3, 5, 7, 2, 8, 9 };
			long answer = 0;
			foreach(var p in permutations)
            {
                // throw out anything that wouldn't be divisible by 2 with d2, d3, d4
                if (p[3] == 0 || p[3] == 2 || p[3] == 4 || p[3] == 6 || p[3] == 8)
                {
					// throw out anything that wouldn't be divisible by 5 with d4, d5, d6
					if (p[5] == 0 || p[5] == 5)
                    {
						// throw out anything that wouldn't be divisible by 3 with d3, d4, d5
						// all multiples of 3 must have the sum of its digits be divisble by 
						if(p[2..5].Sum() % 3 == 0)
                        {
                            // throw out anything that wouldn't be divisible by 7 with d5, d6, d7
                            // there is a divisibility rule of 7, but it would be more processing
                            // intensive than just checking the digits, I think
                            if (((p[4] * 100) +(p[5] * 10) + p[6]) % 7 == 0)
                            {
								// throw out anything that wouldn't be divisible by 11 with d6, d7, d8
								// there is a divisibility rule of 11, but it would be more processing
								// intensive than just checking the digits, I think
								if (((p[5] * 100) + (p[6] * 10) + p[7]) % 11 == 0)
                                {
									// throw out anything that wouldn't be divisible by 13 with d7, d8, d9
									if (((p[6] * 100) + (p[7] * 10) + p[8]) % 13 == 0)
									{
										// throw out anything that wouldn't be divisible by 17 with d8, d9, d10
										if (((p[7] * 100) + (p[8] * 10) + p[9]) % 17 == 0)
										{
											// winner, winner, chicken dinner!
											long pNum = CommonAlgorithms.ConvertIntArrayToLong(p);
											answer += pNum;
										}
									}
								}
							}
						}
					}

				}
            }
			PrintSolution(answer.ToString());
			return;
		}
	}
}
