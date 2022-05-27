using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0040 : Euler
	{
		public Euler0040() : base()
		{
			title = "Champernowne's constant";
			problemNumber = 40;
			PrintTitle();
		}
        public override void Run()
        {
            Run_bruteForce();
        }
        public void Run_elegant()
        {
			int[] checkVals = new int[] { 1, 10, 100, 1000, 10000, 100000, 1000000 };

            int product = 1;

            

            /* 
             *  place: 9; i: 9; current str len: 9
                place: 98; i: 54; current str len: 99
                place: 997; i: 369; current str len: 999
                place: 9998; i: 2777; current str len: 10001
                place: 99995; i: 22221; current str len: 99999
                place: 999994; i: 185184; current str len: 999999
            */
            //int d_1 = 1;
            //int d_10 = 1;
            //int d_100 = -1;
            //int d_1000 = -1;
            //int d_10000 = -1;
            //int d_100000 = -1;
            //int d_1000000 = -1;

            //int numberAtD_10 = 10;
            //int strLenAfterD_10 = 11;
            //int nextMilestoneStrLen = 100;
            //int numDigitsThisOOM = 2;
            //int howManyIntsUntilNextMilestone = (nextMilestoneStrLen = strLenAfterD_10) / numDigitsThisOOM;
            //int numberAtD_100 = numberAtD_10 + 10;



            //int nextOOMChange = 100;
            //int howManyIntsUntilNextOOMChange = nextOOMChange - numberAtD_10;
            //int positionAtNextOOMChange = howManyIntsUntilNextOOMChange * numDigitsThisOOM + numberAtD_10;
            //numDigitsThisOOM = 3;
            //int howManySpacesUntilNextMilestone = numDigitsThisOOM *

            PrintSolution(product.ToString());
			return;
		}
        public void Run_bruteForce()
        {
            /*
			 * first we have 9 1-digit numbers (1 - 9)
			 * then we'll have 90 2-digit numbers (10 - 99)
			 * then we (100 - 999)
			 * */

            int[] checkVals = new int[] { 1, 10, 100, 1000, 10000, 100000, 1000000 };

            int product = 1;

            int placeCurrent = 0;
            int placePrior = 0;

            for (int i = 1; i < 300000; i++)
            {
                int oom = MathHelper.GetOrderOfMagnitudeOfInt(i);
                int numDigits = oom + 1;
                placeCurrent = placePrior + numDigits;
                for (int j = 0; j < checkVals.Length; j++)
                {
                    if (placeCurrent >= checkVals[j] && placePrior < checkVals[j])
                    {
                        int[] iAsArray = MathHelper.ConvertIntToIntArray(i);
                        Array.Reverse(iAsArray);    // makes it easier to pull the right digit
                        int positionInArray = placeCurrent - checkVals[j];
                        int digit = iAsArray[positionInArray];
                        Console.WriteLine(string.Format("digit: {2}; place: {0}; i: {1};",
                            placeCurrent, i, digit));

                        product *= digit;

                    }

                }
                placePrior = placeCurrent;
            }

            PrintSolution(product.ToString());
            return;
        }

    }
}
