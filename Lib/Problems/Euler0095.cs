//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0095 : Euler
	{
		public Euler0095() : base()
		{
			title = "Amicable chains";
			problemNumber = 95;
		}
		protected override void Run()
		{
            /*
             * After problem 94, I think I need to stop calling these easy or 
             * hard. For me, this one was so simple while I struggled mightily
             * with 94. Others seem to have had it backwards. I think, all in 
             * all, the difference is where inspiration hits. For me, on this 
             * problem I pretty quickly realized how simple it would be to 
             * proactively create a table of known factor sums for each number.
             * I toyed with the idea of doing this as I went, sort of like a 
             * cache, but I realized how easy and fast it would be to just 
             * increment a factor sum by going up the integers list and 
             * multiplying each number N times until you got to the limit.
             * 
             * Having that list handy made it super simple to find the longest 
             * chain. I thought about caching known "bad" numbers. Say like the 
             * number 10. 10's proper divisors are 1, 2, and 5, sum to 8. This 
             * goes 8 -> 7 -> 1 -> 1. So that, if I ever came across a number 
             * that had a 10 in its chain, I could stop. But that would have 
             * been a lot of implementation for little gain. And I'm not even
             * certain it would work. 
             * 
             * In the end, this runs in half a second and makes me happy.
             * 
             * */
            const int limit = (int)1e6;
            int[] factorSums = new int[limit + 1];
            Array.Fill(factorSums, 1);
            factorSums[0] = 0;

            // fill the factor sums by adding factors
            for(int i = 2; i <= limit; i++)
            {
                for (int j = 2; true; j++)
                {
                    int product = i * j;
                    if (product > limit) break;
                    factorSums[product] += i;
                }
            }

            int longestLength = 0;
            int answer = 0;
            for (int i = 2; i <= limit; i++)
            {
                int numToCheck = i;
                List<int> links = new List<int>();
                bool isLooped = false;
                while(isLooped == false)
                {
                    if (numToCheck > limit) break;
                    links.Add(numToCheck);
                    int nextNum = factorSums[numToCheck];
                    // is nextNum already in the chain?
                    if(links.Contains(nextNum))
                    {
                        isLooped = true;
                        if (links[0] == nextNum)
                        {
                            // we've got a start-to-finish loop
                            if(links.Count > longestLength)
                            {
                                longestLength = links.Count;
                                answer = links.Min();
                            }
                        }
                    }
                    numToCheck = nextNum;
                }
            }
			
			PrintSolution(answer.ToString());
			return;
		}
	}
}
