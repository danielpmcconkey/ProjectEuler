//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0052 : Euler
	{
		public Euler0052() : base()
		{
			title = "Permuted multiples";
			problemNumber = 52;
		}
		protected override void Run()
		{
			int priorNumDigits = 0;
			
			for(int i = 1; i < 10000000; i++)
            {
				int multiple2 = i * 2;
				int multiple6 = i * 6;
				int numDigits2 = CommonAlgorithms.GetOrderOfMagnitude(multiple2) + 1;
				int numDigits6 = CommonAlgorithms.GetOrderOfMagnitude(multiple6) + 1;
				
				// only proceed if the 2x and 6x have the same number of digits
				if(numDigits6 > numDigits2)
                {
					if(priorNumDigits < numDigits6)
                    {
                        priorNumDigits = numDigits6;
						// jump ahead to the next point where 2x and 6x will have the same num digits
						i = (int)(Math.Pow(10, numDigits2) / 2) - 1;
					}
					
					continue;
                }
				
				// do 2x and 6x have the same digits?
				int[] arrayOf2x = CommonAlgorithms.ConvertIntToIntArray(multiple2);
				Array.Sort(arrayOf2x);
				int[] arrayOf6x = CommonAlgorithms.ConvertIntToIntArray(multiple6);
				Array.Sort(arrayOf6x);
				if (hasSameDigits(arrayOf2x, arrayOf6x))
                {
					// same for 3x?
					int multiple3 = i * 3;
					int[] arrayOf3x = CommonAlgorithms.ConvertIntToIntArray(multiple3);
					Array.Sort(arrayOf3x);
					if(hasSameDigits(arrayOf2x, arrayOf3x))
                    {
						// same for 4x?
						int multiple4 = i * 4;
						int[] arrayOf4x = CommonAlgorithms.ConvertIntToIntArray(multiple4);
						Array.Sort(arrayOf4x);
						if(hasSameDigits(arrayOf2x, arrayOf4x))
                        {
							int multiple5 = i * 5;
							int[] arrayOf5x = CommonAlgorithms.ConvertIntToIntArray(multiple5);
							Array.Sort(arrayOf5x);
							if (hasSameDigits(arrayOf2x, arrayOf5x))
                            {
								// winner, winner kumquat dinner
								int answer = i;
								PrintSolution(answer.ToString());
								return;
							}
						}
					}
                }
			}			
		}
		private bool hasSameDigits(int[] a, int[]b)
        {
			for(int i = 0; i < a.Length; i++)
            {
				if (a[i] != b[i]) return false;
            }
			return true;
        }
	}
}
