//#define VERBOSEOUTPUT

namespace EulerProblems.Lib.Problems
{
	public class Euler0094 : Euler
	{
		public Euler0094() : base()
		{
			title = "Almost equilateral triangles";
			problemNumber = 94;
		}


        protected override void Run() 
        {
            /*
             * So this is the first one I cheated on. Let me explain. As is 
             * often my MO, I started with a brute force approach. I like to 
             * get the answer, then optimize. So I set about creating something
             * very similar to the Run_slow method below. I enumerated all the 
             * numbers from 2 to 1e9 and tried a + 1 combination and a - 1 
             * combination. I checked whether each area was an integer and 
             * thought Bob was my uncle. But the answer it generated was wrong.
             * 
             * I tried for hours to tweak it slightly, trying different ways to
             * calculate the area. I even got different results most of the 
             * time. I must have tried putting in 13 or 14 different answers 
             * into the problem form. No dice. I became certain that I must've
             * misunderstood the problem. So I looked up someone else's 
             * solution online.
             * 
             *    https://www.mathblog.dk/project-euler-94-almost-equilateral-triangles/
             *    
             * And, while I get Pell's equation, I didn't see the need to use 
             * it here. I wasn't looking for a efficient solution. I was 
             * looking for why my brute force approach gave me a different 
             * answer. I went from article to article online, only to see 
             * everyone riff on the same theme. So I finally just stole someone
             * else's code (that's the Run_Pell method here) so that I could 
             * print out their "almost" equilateral triangles and compare with 
             * mine.
             * 
             * An example of one my solution considered right but isn't is 
             * 93686, 93683, and 93687. Put this one in Excel and it spits out
             * an integer for area. But the height is sorta wonky. Almost looks
             * irrational. Ah crap, this is about decimal precision, isn't it?
             * 
             * I don't know why I didn't get there myself. I've spent more than
             * a couple of hours already in this Euler journey handling 
             * floating-point decimal fun. But whatever, I eventually learned 
             * to back-check my triangles' height values to confirm that they
             * weren't off by 1 and got the answer in about 30 seconds. 
             * 
             * But that was hollow. I owed it to myself to learn this one 
             * better and optimize it myself. I'd given up on understanding the
             * Pell solution. I don't remember my thought process during 
             * problem 66, so I don't remember how well I actually understood
             * the Pell equation. But I couldn't get it here. I just couldn't 
             * see how height ^ 2 = c ^ 2 - a ^ 2 and area = 1/2 base * height
             * could turn into some of the weird equations printed as pictures
             * in that mathblog.dk article.
             * 
             * So I tried going the opposite way. What if I could generated 
             * pythagorean triples and check them against the problem 
             * constraints? I knew I could generate pythagorean triples pretty
             * easily (see problem 75), but I wasn't sure that all "almost" 
             * equilateral triangle could be broken into 2 whole integer right
             * triangles. Again, I cheated and ran my old slow method to print
             * out all the winning triangles and confirmed that that was, 
             * indeed the case.
             * 
             * As it turns out, generating pythagorean triples up to a maximum 
             * perimeter of 1e9 is way longer than problem 75's max perimeter 
             * of 1.5e6. But that's okay. I knew I could use this method to 
             * generate all triangles necessary and could optimize later. I 
             * did and learned a couple of valuable insights along the way.
             * 
             * First, I had to conceive of the pythagorean triangles in a 
             * different configuration (rotation?) for the hypotenuse + 1 
             * variant as I did for the hypotenuse - 1 variant. That took a 
             * while. Next, I learned that I didn't have to check all the 
             * possible combinations of m and n that my common algorithms 
             * function goes through. I didn't need all right triangles, just 
             * those that formed half of an almost equilateral. And that turns
             * into a very slim subset. And finally, I learned that I didn't 
             * have to expand my triangles out by a k factor of the ratio. All
             * almost equilateral triangles are formed from 2 primitive right
             * triangles.
             * 
             * Put all that together, and my Pythagoras method for solving this
             * problem runs just as fast as the Pell version. I'm satisified 
             * and, while I'm dissappointed in myself for not persevering 
             * through frustration, I *do* feel that I've redeemed myself 
             * through my journey of solving this damned thing a "right" way.
             *
             * */

            //Run_slow(); // Elapsed time: 28590.0104 milliseconds
            //Run_Pell(); // Elapsed time: 1.4892 milliseconds
            Run_Pythagoras();
        }
        private void Run_Pythagoras()
        {
            Func<int, List<Triangle>> getSpecialPythagoreanTriangles = (maxPerimiter) =>
            {
                /* this function is stolen from the
                 * CommonAlgorithms.GetPythagoreanTriangles() used in problem
                 * 75. But, that algorithm is too slow and returns many
                 * triangles that aren't almost equilateral. So I copied the
                 * logic here, but forced a check to see if the twice the base
                 * would be +- 1 of the hypotenuse.
                 * */

                Func<int, int, (bool,Triangle)> evalMandN = (m, n) =>
                {
                    if (n == 0) return (false, new Triangle());
                    int hypotenuse = ((m * m) + (n * n));
                    int halfBase = ((m * m) - (n * n));
                    int height = 2 * m * n;
                    int fullBase = halfBase * 2;
                    int perimeter = hypotenuse + hypotenuse + fullBase;
                    if (perimeter <= maxPerimiter &&
                        (fullBase == hypotenuse + 1 || fullBase == hypotenuse - 1))
                    {
#if VERBOSEOUTPUT
                        Console.WriteLine("hypotenuse: {0}  full base: {1}  perimeter {2} | m: {3} n: {4}",
                            hypotenuse, fullBase, perimeter, m, n);
#endif
                        return (true, new Triangle()
                        {
                            a = hypotenuse,
                            b = halfBase,
                            c = height,
                            perimeter = perimeter
                        });
                    }
                    if ((2 * height) + 1 == hypotenuse)
                    {
#if VERBOSEOUTPUT
                        Console.WriteLine("hypotenuse: {0}  full base: {1}  perimeter {2} | m: {3} n: {4}",
                            hypotenuse, (2 * height), (hypotenuse * 2) + (2 * height), m, n);
#endif
                        return (true, new Triangle()
                        {
                            a = hypotenuse,
                            b = halfBase,
                            c = height,
                            perimeter = (hypotenuse * 2) + (2 * height),
                        });
                    }
                    return (false, new Triangle());
                };
                 

                List<Triangle> triangles = new List<Triangle>();

                int maxM = (int)Math.Ceiling(Math.Sqrt(maxPerimiter / 2.0));
                int m = 2;
                int n = 1;
                int priorN = 0;
                /* 
                 * after building an algorithm that looped through all m and n 
                 * combinations and checking each for relative prime-ness, like
                 * in the original CommonAlgorithms.GetPythagoreanTriangles()
                 * function, I checked to see if these had a special n/m 
                 * relationship. Turns out they do and I was able to greatly 
                 * speed this up.
                 * */
                while (m < maxM)
                {
                    // this is for the variant where the odd length is 1 plus the shared length 
                    var result1 = evalMandN(m, n);
                    if (result1.Item1) triangles.Add(result1.Item2);

                    // this is for the variant where the odd length is 1 minus the shared length
                    var result2 = evalMandN(priorN, n);
                    if (result2.Item1) triangles.Add(result2.Item2);


                    int sumMN = m + n;
                    int nextN = n + sumMN;
                    int nextM = nextN + sumMN;
                    m = nextM;
                    priorN = n;
                    n = nextN;
                }
                return triangles;
            };
            int limit = (int)1E9;
            long answer = 0;

            var triangles = getSpecialPythagoreanTriangles(limit);
            answer = triangles.Sum(x => x.perimeter);

            PrintSolution(answer.ToString());
            return;
        }
        private void Run_Pell()
        {
            long limit = (long)1E9;
            long answer = 0;

            long x = 2;
            long y = 1;

            while(true)
            {
                // first do the base = a - 1 case
                long aTimes3 = 2 * x - 1;
                long areaTimes3 = y * (x - 2);
                if (aTimes3 > limit) break;

                if(aTimes3 > 0 && areaTimes3 > 0 && aTimes3 % 3 == 0)
                {
                    long a = aTimes3 / 3;
                    long area = areaTimes3 / 3;
                    long perimeter = (3 * a) - 1;
                    answer += perimeter;
#if VERBOSEOUTPUT
                    Console.WriteLine("{0}  {1}  {2}", a, a - 1, perimeter);
#endif
                }

                // now do the base = a + 1 case
                aTimes3 = 2 * x + 1;
                areaTimes3 = y * (x + 2);

                if (aTimes3 > 0 && areaTimes3 > 0 && aTimes3 % 3 == 0)
                {
                    long a = aTimes3 / 3;
                    long area = areaTimes3 / 3;
                    long perimeter = (3 * a) + 1;
                    answer += perimeter;
#if VERBOSEOUTPUT
                    Console.WriteLine("{0}  {1}  {2}", a, a + 1, perimeter);
#endif
                }

                // set up the next loop
                long nextX = (2 * x) + (y * 3);
                long nextY = (2 * y) + x;
                x = nextX;
                y = nextY;
            }

            PrintSolution(answer.ToString());
            return;
        } 
        private void Run_slow()
        {            
            Func<long, long, bool> isIntegral = (sharedLength, baseLength) =>
            {
                double halfBase = baseLength * 0.5D;
                double aSquared = halfBase * halfBase;
                double cSquared = sharedLength * sharedLength;
                double bSquared = cSquared - aSquared;
                double height = Math.Sqrt(bSquared);
                double area = height * halfBase;
                if (!CommonAlgorithms.IsInteger(area)) return false;

                /*
                 * we KNOW we have a non-integral area if the above was false
                 * but we don't yet know we have a true integral area yet 
                 * because of wonky decimal precision. So now I'm going to back
                 * check my figuring to see if the result of any square root 
                 * operations produces a different value when squared.
                 * */

                long backCheckASquared = (long)halfBase * (long)halfBase;
                long backCheckBSquared = (long)height * (long)height;
                long backCheckCSquared = sharedLength * sharedLength;
                if (backCheckASquared + backCheckBSquared == backCheckCSquared) return true;
                return false;
            };
            
            var test = isIntegral(93686, 93687);
            long limit = (long)1E9;
            long answer = 0;

            long sideABLength = 2;
            List<(long a, long b, long c)> solutions = new List<(long a, long b, long c)>();
            while (true)
            {
                long c1 = sideABLength - 1;
                long c2 = sideABLength + 1;
                long p1 = (sideABLength * 2) + c1;
                long p2 = (sideABLength * 2) + c2;
                if (p1 <= limit && isIntegral(sideABLength, c1))
                {
                    answer += p1;
#if VERBOSEOUTPUT
                    Console.WriteLine("{0}  {1}  {2}", sideABLength, c1, p1); 
#endif
                    solutions.Add((sideABLength, sideABLength, c1));
                }
                if (p2 <= limit && isIntegral(sideABLength, c2))
                {
                    answer += p2;
#if VERBOSEOUTPUT
                    Console.WriteLine("{0}  {1}  {2}", sideABLength, c2, p2); 
#endif
                    solutions.Add((sideABLength, sideABLength, c2));
                }
                if (p1 > limit) break;
                sideABLength++;
            }
            
            PrintSolution(answer.ToString());
            return;
        }        
    }
}
