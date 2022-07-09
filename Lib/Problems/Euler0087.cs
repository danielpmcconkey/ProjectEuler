//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0087 : Euler
	{
		public Euler0087() : base()
		{
			title = "Prime power triples";
			problemNumber = 87;
		}
		protected override void Run()
		{
            /*
             * This one was comically easy. I could see someone getting in the 
             * trap of cycling through for i in 1 .. numPrimes, j in 
             * 1 .. numPrimes, k in 1 .. numPrimes, but that would only be 
             * 748MM loops of k. Maybe it'd be longer than a minute, but it'd 
             * still run. I don't know. I just got this one right away. Solved
             * it on my lunch break.
             * 
             * */

            const int limit = (int)5e7;// 50000000;
            var lowestPrime = (int)Math.Floor(Math.Sqrt(limit));
            var primes = CommonAlgorithms.GetPrimesUpToN(lowestPrime);

            var count = 0;
            HashSet<int> memo = new HashSet<int>();

            for(int i = 0; i < primes.Length; i++)
            {
                int exp4 = (int)Math.Pow(primes[i], 4);
                if (exp4 > limit) break;

                for(int j = 0; j < primes.Length; j++)
                {
                    int exp3PlusExp4 = exp4 + (int)Math.Pow(primes[j], 3);
                    if (exp3PlusExp4 > limit) break;

                    for(int k = 0; k < primes.Length; k++)
                    {
                        int sumExp2through4 = exp3PlusExp4 + (int)Math.Pow(primes[k], 2);
                        if (sumExp2through4 > limit) break;

                        if(!memo.Contains(sumExp2through4))
                        {
                            count++;
                            memo.Add(sumExp2through4);
                        }
                    }
                }
            }

            int answer = count;
			PrintSolution(answer.ToString());
			return;
		}
	}
}
