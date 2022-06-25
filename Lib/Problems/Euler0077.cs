//#define VERBOSEOUTPUT // prints out count of the total possible prime summations for each number 
//#define VERYVERBOSEOUTPUT // prints out each individual permutation
namespace EulerProblems.Lib.Problems
{
	public class Euler0077 : Euler
	{
		public Euler0077() : base()
		{
			title = "Prime summations";
			problemNumber = 77;
		}
        protected override void Run() 
        {
            /*
             * I had a lot of fun with this one. Perhaps it's because I'm 
             * learning new ways to think through problem solving. I dunno.
             * 
             * My first instinct was to start from problem 31's code base. That
             * problem asks how many different ways you can spend coins to sum
             * up to 2 GPB. This seems like an obvious place to start. But I 
             * never got it to work. I do think I could've adapted that code to
             * make it work, but I ended up starting over. I went through many 
             * different variations of brute forcing the problem. At one point,
             * I set the program to run overnight and, around midnight, when I 
             * got up, I checked and it still hadn't gotten past n = 51.
             * 
             * I knew my problem was with clumsy / lazy thinking. In that 
             * version, I was getting duplicates so I ran a check to see if I
             * had created a duplicate and just threw out that instance. This 
             * was very computationally expensive in a recursive algorithm. So,
             * in the morning, I fixed that issue and landed very close to what
             * you see below as my Run_originalAndAdaptable method. It produced
             * the correct answer in about 4 seconds. It has the added benefit 
             * of being able to print out each permutation of primes for each 
             * value of n. I feel this would be very useful for adapting this
             * code to use in another problem.
             * 
             * Once I got the answer, I saw a post in the problem thread to:
             * 
             *      https://mathworld.wolfram.com/PrimePartition.html
             *     
             * This seemed promising. I'd tried reseaching a nifty Euclid or 
             * Euler solution on my own and never got anywhere. I do feel I'm
             * getting better at coming up with which terms to google, though.
             * Anyway, in that link, it says: 
             * 
             *     If a_n=1 for n prime and a_n=0 for n composite, then 
             *     the Euler transform b_n gives the number of partitions 
             *     of n into prime parts (Sloane and Plouffe 1995, p. 21). 
             *     
             * I checked out the Euler transformations page at: 
             * 
             *     https://mathworld.wolfram.com/EulerTransform.html 
             *     
             * but couldn't figure out what c was supposed to be in that 
             * formula. But that allowed me to google other prime partition
             * web pages and found this:
             * 
             *     https://math.stackexchange.com/questions/89240/prime-partition
             *     
             * That showed me two ways to calculate it and I went for the Euler
             * transformation, thinking it'd be faster. Interestingly, it was 
             * actually slower than my original version. This was before I'd 
             * added memoization to both. Now, after the memoization, the Euler
             * transformation prime partition calculator is about 3x faster.
             * 
             * */


            //Run_originalAndAdaptable(); // 21.4173 milliseconds
            //Run_originalAndOptimized(); // 20.3334 milliseconds
            Run_primePartition(); // Elapsed time: 8.6065 milliseconds
        } 
        private void Run_primePartition()
        {
            int target = 5000;
            var primes = CommonAlgorithms.GetPrimesUpToN(target);
            var primeBools = CommonAlgorithms.GetPrimesUpToNAsBoolArray(target);

            Func<int, int> sumOfPrimeFactors = (n) =>
            {
                Func<int, int, bool> isMaFactorOfN = (m, n) =>
                {
                    return (n % m == 0);
                };
                return primes.Where(m => m <= n && isMaFactorOfN(m, n)).Sum();
            };
            Dictionary<int, int> cache = new Dictionary<int, int>();

            Func<int, int> primePartition = null;
            primePartition = (n) =>
            {
                if (n == 1) return 0;
                if (cache.ContainsKey(n)) return cache[n];
                var sopfN = sumOfPrimeFactors(n);
                var sum = sopfN;
                for(int j = n - 1; j >= 1; j--)
                {
                    var sopfJ = sumOfPrimeFactors(j);
                    var subPrimePartition = primePartition(n - j);
                    sum += (sopfJ * subPrimePartition);
                }
                var result = sum / n;
                cache[n] = result;
                return result;
            };

            int start = 10;
            for (int i = start; true; i++)
            {
                var primeSummations = primePartition(i);
                // subtract 1 if i is prime because the problem states that it
                // wants to count only *sums* of prime and 11 + null is not a
                // sum, so shouldn't count for 11's prime summations. But we
                // can't subtract it in the primePartition function or it'd
                // break the recursion 
                if (primeBools[i]) primeSummations--;

#if VERBOSEOUTPUT
                Console.WriteLine("i = {0}. count = {1}", i, primeSummations); 
#endif
                if (primeSummations > target)
                {
                    //var test = cache.OrderBy(x => x.n).ThenBy(y => y.startVal).ToArray();
                    PrintSolution(i.ToString());
                    return;
                }

            }
        }
        private void Run_originalAndOptimized()
        {
            int target = 5000;
            int[] primes = CommonAlgorithms.GetPrimesUpToN(target);
            var primeBools = CommonAlgorithms.GetPrimesUpToNAsBoolArray(target);

            Dictionary<(int, int), int> cache = new Dictionary<(int, int), int>();

            Func<int, int, int> HowManyWaysToPrimeSumN = null;
            HowManyWaysToPrimeSumN = (n, startVal) =>
            {
                if (cache.ContainsKey((n, startVal))) return cache[(n, startVal)];
                var tally = 0;
                var primesUnderN = primes.Where(x => x <= Math.Min(n, startVal)).OrderByDescending(y => y);
                var pCount = primesUnderN.Count();
                if (pCount < 1) return pCount;
                foreach (var starter in primesUnderN)
                {
                    int remainder = n - starter;
                    if (remainder == 0)
                    {
                        tally++;
                    }
                    else
                    {
                        tally += HowManyWaysToPrimeSumN(remainder, starter);
                    }
                }
                var result = tally;
                cache.Add((n, startVal), result);
                return result;
            };

            int start = 10;
            for (int i = start; true; i++)
            {
                var count = HowManyWaysToPrimeSumN(i, i);
                if (primeBools[i]) count--;
#if VERBOSEOUTPUT
                Console.WriteLine("i = {0}. count = {1}", i, count); 
#endif
                if (count > target)
                {
                    PrintSolution(i.ToString());
                    return;
                }
            }
        }
        private void Run_originalAndAdaptable()
		{
            int target = 5000;
            int[] primes = CommonAlgorithms.GetPrimesUpToN(target);
            var primeBools = CommonAlgorithms.GetPrimesUpToNAsBoolArray(target);

            Func<List<int>, List<int>> copyList = (l1) =>
            {
                List<int> l2 = new List<int>();
                foreach (int i in l1) l2.Add(i);
                return l2;
            };
            

            Dictionary<(int, int), int> cache = new Dictionary<(int, int), int>();

            Func<int, int, List<int>, List<List<int>>, int> HowManyWaysToPrimeSumN = null;
            HowManyWaysToPrimeSumN = (n, startVal, primesSoFar, combos ) =>
            {
                if (cache.ContainsKey((n, startVal))) return cache[(n, startVal)];
                var tally = 0;
                var primesUnderN = primes.Where(x => x <= Math.Min(n, startVal)).OrderByDescending(y => y);
                var pCount = primesUnderN.Count();
                if (pCount < 1) return pCount;
                foreach(var starter in primesUnderN)
                {
                    var newList = copyList(primesSoFar);
                    newList.Add(starter);
                    
                    int remainder = n - starter;
                    if (remainder == 0)
                    {
                        
                        newList = newList.OrderByDescending(x => x).ToList();
                        tally++;
#if VERYVERBOSEOUTPUT
                        Console.WriteLine(string.Join(", ", newList));
                        combos.Add(newList); 
#endif
                        
                    }
                    else
                    {
                        tally += HowManyWaysToPrimeSumN(remainder, starter, newList, combos);
                    }                    
                }
                var result = tally;
                cache.Add((n, startVal), result);
                return result;
            };

            int start = 10;
            for (int i = start; true; i++)
            {
                var origAsk = i;
                var primesSoFar = new List<int>();
                var count = HowManyWaysToPrimeSumN(origAsk, origAsk, primesSoFar, new List<List<int>>());
                if (primeBools[i]) count--;
#if VERBOSEOUTPUT
                Console.WriteLine("i = {0}. count = {1}", i, count); 
#endif
                if (count > target)
                {
                     PrintSolution(i.ToString());
                    return;
                }
            }
        }

        
    }
}
