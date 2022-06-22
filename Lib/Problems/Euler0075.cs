//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
    internal struct Triangle 
    { 
        public long a; 
        public long b;
        public long c;
        public long perimeter;
    }
	public class Euler0075 : Euler
	{
		public Euler0075() : base()
		{
			title = "Singular integer right triangles";
			problemNumber = 75;
		}
        protected override void Run()
        {
            Run_bruteForce(); // never finishes
            Run_slow(); // takes about 8 minutes
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

        private List<Triangle> GeneratePythagoreanTrianglesSlow(int maxPerimeter)
        {
            List<Triangle> triangles = new List<Triangle>();
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
                    Triangle t = new Triangle()
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
