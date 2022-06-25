//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0076 : Euler
	{
		public Euler0076() : base()
		{
			title = "Template";
			problemNumber = 76;
		}
        protected override void Run() 
        {
            /*
             * So, first off, this is a straight copy of problem 31 with a 
             * little extra processing power required. Instead of 9 coins, we 
             * now have 99. I literally copied and pasted 31, made a couple of
             * minor tweaks, and got the solution right away. That's the 
             * Run_slow() method.
             * 
             * I then went into the thread and read all about generating 
             * functions and how this problem is really called a partition 
             * function in the math world. I more or less copied my Run_fast()
             * code from the problem thread to see if it would work and then 
             * went back to understand why. Bottom line: I don't. But I do know
             * how to read the math symbols underneath the line:
             * 
             *     Euler invented a generating function which gives rise to a 
             *     recurrence equation in P(n)
             *     
             * */

            //Run_slow(); takes 8 seconds
            Run_fast(); // takes 5 ms
        }
        private void Run_fast()
        {
            int target = 100;// 100;
            int howMany = CommonAlgorithms.PartitionFunction(target).count;
            // subtract 1 because the Euler problem is "How many different ways
            // can one hundred be written as a sum of at least two positive
            // integers?". Emphasis on the 2 there. that means that you can't
            // use 100 plus nothing in the answer, but the recursive function
            // needs it.
            int answer = howMany - 1; 
            PrintSolution(answer.ToString());
            return;
        }
        private void Run_slow()
		{
            Func<int, int[], int> howManyWaysToSumANumber = null;
            howManyWaysToSumANumber = (n, digitsArray) =>
            {
                if (digitsArray.Length == 1) return 1;
                int tally = 0;
                for (int i = 0; i < digitsArray.Length; i++)
                {
                    int remainder = n - digitsArray[i];
                    if (remainder == 0)
                    {
                        tally++;
                    }
                    if (remainder > 0)
                    {
                        int[] newCoinArray = digitsArray[i..digitsArray.Length];
                        tally += howManyWaysToSumANumber(remainder, newCoinArray);
                    }
                }
                return tally;
            };
            int target = 100;
            int[] allDigits = Enumerable.Range(1, target - 1).Reverse().ToArray();
            int howMany = howManyWaysToSumANumber(target, allDigits);
            PrintSolution(howMany.ToString());
            return;
        }
        
    }
}
