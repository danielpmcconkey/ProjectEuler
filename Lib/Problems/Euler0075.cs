//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
    
	public class Euler0075 : Euler
	{
		public Euler0075() : base()
		{
			title = "Singular integer right triangles";
			problemNumber = 75;
		}
        protected override void Run()
        {
            /*
             * This one kicked my tail, but it was so deceptive. See, for once,
             * I actually remembered a prior problem. Problem 39. It looks so
             * very similar. The difference is in your maximum perimeter, 
             * though. Problem 39s is 1,000 while this one's is 1,500,000. 
             * And that makes *so* much difference.
             * 
             * My typical approach to these is to first brute force an answer.
             * Then, once I'm positive in the answer, I will find ways to 
             * optimize. But here, the brute force mechanism took so long. By 
             * brute force, I mean testing all the possibilities of A, B, and C
             * to see if they were a pythagorean triplet. Some back of the 
             * napkin math tells me that that's something like 2 * 10^19 
             * floating point operations. 
             * 
             * So I spent some time trying to limit which values I had to 
             * process. That's the Run_bruteForce() method I left intact here.
             * And I used it to produce things like the first 10,000 triples so
             * I could study them for relationships. I spent hours on that. No
             * dice. There just wasn't anything I could tie to all the triples
             * together. 
             * 
             * I actually did get a brute force approach to produce an answer 
             * in about an hour, but it was wrong. I don't know what was wrong
             * as it's awfully tough to troubleshoot something that takes an 
             * hour to run when you don't actually know what the right output
             * should be.
             * 
             * So I hit the books. I read a lot about pythagorean triples and 
             * right triangles. There's a real nice formula to get all the 
             * possible integer triagles for a given perimeter, but that wasn't
             * constrained to just right triangles. I also found many different
             * ways to produce all of the primitive pythagorean triples, but 
             * those wouldn't give the right answer without combining with the
             * non-primitive triples.
             * 
             * Eventually, I found Dixon's method
             * https://en.m.wikipedia.org/wiki/Formulas_for_generating_Pythagorean_triples#Dickson.27s_method
             * 
             * This was the first thing I found that promised to produce all 
             * triples. But the first time I ran it, I got the wrong answer. 
             * That sent me down no end of wild goose chases. At first, I was
             * certain that Dickson's method was producing duplicates and I 
             * wrote multiple different methods to check that. I also found a 
             * mathematics stack overflow question that was certain Dixon's 
             * didn't produce them all, but the rest of that community 
             * disagreed.
             * 
             * So then I re-wrote my method for getting all factors from the 
             * r^2/2 coefficient at the heart of Dickson's method. I did find 
             * an error there that was ignoring the duplicated factor pair in 
             * perfect squares. eg: 36 = (1 * 36) , (2 * 18), (3 * 12), 
             * (4 * 9), and (6 * 6). In that example, it was dropping the (6,
             * 6) pair. I was certain that that was it. I re-ran, but got the 
             * same output. Presumably because r^2 / 2 will never produce a 
             * perfect square.
             * 
             * Ultimately, the problem came down to a limiter I put on my r
             * value. I knew that the B calculation in Dixon's method was 
             * r + coefficient and that all coefficients have a factor pair of
             * (1, coefficient). So dumb me thought that this could be used as
             * a safe limiter. I didn't realize, though, that the same 
             * coefficient could produce (2, coefficient/2) and 
             * (4, coefficient / 4). So, no matter how big the coefficient was, 
             * it could often produce a factor pair that yielded A + B + C
             * perimeters that were lower than the 1.5MM threshold.
             * 
             * So I removed my governor and it now took 8 minutes to run. And 
             * still produce the wrong result. It took forever to debug that, 
             * but eventually realized my problem was in integer overruns 
             * because, even though A, B, C, r, and perimiter are all safely 
             * withn integer range, some valid r values produced r^2/2 values
             * that weren't. So I "longed" it all up, re-ran, and got the 
             * right answer.
             * 
             * In 8 minutes.
             * 
             * So back to my MO. Get the right answer and optimize. Reading the
             * problem thread, I was shocked to see that many used an algorithm
             * that only produced primitive pythagorean triples. I did know, 
             * because of my earlier research, that you could take any triple 
             * and scale it up. Eg: 3, 4, 5 (a primitive) can be multiplied by
             * 2 to create 6, 8, 10. I haddn't realized that *every* 
             * pythagorean triple is either a primitive or a product of a 
             * primitive. I could've learned this if I'd read the wikipedia 
             * article on Pythagrean triples more closely:
             * 
             *     Despite generating all primitive triples, Euclid's 
             *     formula does not produce all triples—for example, 
             *     (9, 12, 15) cannot be generated using integer m and n. 
             *     This can be remedied by inserting an additional 
             *     parameter k to the formula. 
             *     
             * With that in hand, I quickly wrote a Euclid's formula method, 
             * that included a check that m and n are coprime to avoid 
             * duplications, then added a mechanism that expanded it out by 
             * factors of k.
             * 
             * It runs in less than a second now.
             * 
             * */

            //Run_bruteForce(); // never finishes
            //Run_slow(); // takes about 8 minutes
            Run_fast();
        }
        private void Run_fast()
        {
            int maxPerimeter = 1500000;
            var triangles = CommonAlgorithms.GetPythagoreanTriangles(maxPerimeter);

            var pGroups = from t in triangles
                          group t by t.perimeter into groups
                          select new { perimeter = groups.Key, count = groups.Count(), triangles = groups };

            var singles = pGroups.Where(g => g.count == 1).OrderBy(x => x.perimeter);
            var answer = singles.Count();
            PrintSolution(answer.ToString());
            return;
        }
        private void Run_slow()
        {
            int maxPerimeter = 1500000;
            var triangles = GeneratePythagoreanTrianglesSlow(maxPerimeter);
            
            var pGroups = from t in triangles
                          group t by t.perimeter into groups
                          select new { perimeter = groups.Key, count = groups.Count(), triangles = groups };

            var singles = pGroups.Where(g => g.count == 1).OrderBy(x => x.perimeter);
            var answer = singles.Count();
            PrintSolution(answer.ToString());
            return;
        } 
        private void Run_bruteForce()
		{
            int maxPerimeter = 1500000;
            Dictionary<int, List<(int a, int b, int c)>> solutions = new Dictionary<int, List<(int a, int b, int c)>>();
            int maxC = (int)Math.Ceiling(maxPerimeter * 0.64); // may be a bad guess. but { 384, 512, 640 } sums to 1536
            int maxA = maxC; //  (int)Math.Floor(maxC * 0.5); // another ill-advised guess
            int maxB = maxA;

            for (int a = 1; a <= maxA; a++)
            {
                for (int b = a; b <= maxB; b++) // setting b = a keeps a, b, and c in order and prevents duplicates
                {
                    int aSquared = a * a;
                    int bSquared = b * b;
                    int cSquared = aSquared + bSquared;
                    // is cSquared a perfect square?
                    if (CommonAlgorithms.IsPerfectSquare(cSquared))
                    {
                        // we have a pythagorean triple of integers
                        int c = (int)Math.Sqrt(cSquared);
                        int p = a + b + c;
                        
                        if (!solutions.ContainsKey(p))
                        {
                            solutions.Add(p, new List<(int a, int b, int c)>());
                        }
                        solutions[p].Add((a, b, c));
                    }
                }
            }

            var bark = solutions.Where(x => x.Value.Count() == 1).ToList();
            
            int answer = bark.Count();
            PrintSolution(answer.ToString());
            return;
        }

        private List<TriangleLong> GeneratePythagoreanTrianglesSlow(int maxPerimeter)
        {
            List<TriangleLong> triangles = new List<TriangleLong>();
            Dictionary<long, List<(long, long)>> factorPairsCache = new Dictionary<long, List<(long, long)>>();

            

            Func<long, List<(long, long)>> factorize = (n) =>
            {
                Func<long, List<long>> getFactors = (n) =>
                {
                    long sqrt = (long)Math.Ceiling(Math.Sqrt(n));

                    List<long> factors = new List<long>();
                    // Start from 1 as we want our method to also work when numberToCheck is 0 or 1.
                    for (int i = 1; i < sqrt; i++)
                    {
                        if (n % i == 0)
                        {
                            factors.Add(i);
                            factors.Add(n / i);
                        }
                    }

                    // Check if our number is an exact square.
                    if (sqrt * sqrt == n)
                    {
                        factors.Add(sqrt);
                        factors.Add(sqrt);
                    }
                    return factors;
                };

                if (factorPairsCache.ContainsKey(n))
                {
                    return factorPairsCache[n];
                }

                var factors = getFactors(n).ToArray(); //CommonAlgorithms.GetFactors(n);
                List<(long, long)> factorPairs = new List<(long, long)>();
                for(int i = 0; i < factors.Length; i+=2)
                {
                    var f1 = factors[i];
                    var f2 = (i < factors.Length - 1) ? factors[i+1] : factors[i]; // perfect squares print their last factor only once
                    factorPairs.Add((f1, f2));
                }
                

                factorPairsCache.Add(n, factorPairs);
                return factorPairs;
            };


            var stupid = factorize(36);
            var stupid2 = factorize(24);

            long biggestCoefficient = 0;

            int maxR = (int)Math.Ceiling(maxPerimeter * 0.4);
            for(int r = 2; r <= maxR; r += 2)
            {
                
                long coefficient = (long)(Math.Round(Math.Pow(r, 2) / 2));
                
                var factorPairs = factorize(coefficient);
                foreach(var pair in factorPairs)
                {
                    TriangleLong t = new TriangleLong()
                    {
                        a = r + pair.Item1,
                        b = r + pair.Item2,
                        c = r + pair.Item1 + pair.Item2,
                    };
                    t.perimeter = t.a + t.b + t.c;
                    if (t.perimeter <= maxPerimeter)
                    {
                        biggestCoefficient = Math.Max(biggestCoefficient, coefficient);
                        triangles.Add(t);
                    }
                }
            }

            return triangles; 
        }
	}
}
