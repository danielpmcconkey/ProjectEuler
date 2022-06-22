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
            Dictionary<int,int> cache = new Dictionary<int, int>();
            cache[0] = 1;

            Func<int, int> howManyWaysToSumANumber = null;
            howManyWaysToSumANumber = (n) =>
            {
                /* uses a partition function as described
                 * https://mathworld.wolfram.com/PartitionFunctionP.html
                 * 
                 * P(n) = 
                 *  sum(
                 *      for k = 1 to n, 
                 *          (-1)^(k+1) * 
                 *          [
                 *              P(n-((1/2) * (k*(3*k - 1))) 
                 *              +
                 *              P(n-((1/2) * (k*(3*k + 1)))
                 *          ]
                 * */

                if (n < 0)
                {
                    return 0;
                }
                
                if(cache.ContainsKey(n)) return cache[n];

                int Pn = 0;
                for(int k = 1; k <= n; k++)
                {
                    int n1 = n - k * (3 * k - 1) / 2;
                    int n2 = n - k * (3 * k + 1) / 2;

                    if (n1 < 0 && n2 < 0) break; // we've refined it as far as it will go

                    int Pn1 = howManyWaysToSumANumber(n1);
                    int Pn2 = howManyWaysToSumANumber(n2);

                    Pn += (int)(Math.Pow(-1, k + 1) * (Pn1 + Pn2));                    
                }
                

#if VERBOSEOUTPUT
                Console.WriteLine("{1} = {0}", Pn, n); 
#endif
                cache.Add(n, Pn);
                return Pn;
            };
            int target = 100;// 100;
            int howMany = howManyWaysToSumANumber(target);
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
