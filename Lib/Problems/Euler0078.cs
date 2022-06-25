#define VERBOSEOUTPUT
using System.Numerics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0078 : Euler
	{
		public Euler0078() : base()
		{
			title = "Coin partitions";
			problemNumber = 78;
		}
		protected override void Run()
		{
            /*
             * This one was too easy if you'd already learned how to solve
             * problem 76 using a partition function. It's literally the same
             * problem, but requires you to run it over and over again, persist
             * the cache across all runs and move into BigInteger territory 
             * since the final answer produces this many partitions:
             * 
             *     3632530092543578593083233157739676164671583617363389322
             *     7071086460709268608053489541731404543537668438991170680
             *     7452721591544937406153858232021581676352762505545553421
             *     1585542459892015903541304481124508219733509795357091188
             *     4252410730174907784762924663654000000
             *     
             * A lot of folks in the problem thread talk about only needing to 
             * solve for the last 6 digits. I'm not sure how I'd adapt this to
             * do that though. 
             * 
             * */

            Dictionary<BigInteger, BigInteger> cache = new Dictionary<BigInteger, BigInteger>();
            cache[0] = 1;            

            BigInteger start = 1;
            BigInteger targetDivisor = 1000000;// 100;
            for(BigInteger i = start; true; i++)
            {
                var partition = CommonAlgorithms.PartitionFunction(i, cache);
                BigInteger howMany = partition.count;
                cache = partition.cache;

#if VERBOSEOUTPUT
                Console.WriteLine("i = {0}. count = {1}", i, howMany); 
#endif
                if (howMany % targetDivisor == 0)
                {
                    PrintSolution(i.ToString());
                    return;
                }
            }
        }
	}
}
