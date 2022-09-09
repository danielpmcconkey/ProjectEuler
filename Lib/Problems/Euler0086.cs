//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0086 : Euler
	{
		public Euler0086() : base()
		{
			title = "Cuboid route";
			problemNumber = 86;
		}
        protected override void Run()
        {

            /*
             * Boy, was this one a doozey. My first attempt was to imagine two 
             * right triangles. Take a look at the diagram below. If the spider
             * needs to go from (S) to (F) by first walking diagonally up the 
             * front wall then across the ceiling, the spider would hit the 
             * corner at point (A). This gives you two right triangles: (SHA) 
             * and (AGF). Add the two hypotenuses and you get your distance. 
             * 
             *         (E)---------------(F)
             *         /|                /|
             *      (H)-|-----(A)------(G)|
             *       |  |               | |
             *       | (P)--------------+(Q)
             *       |/                 |/
             *      (S)----------------(R)
             *      
             * So now to find the minimum. I came up with what I thought was a
             * clever linear regression technique to arrive at the minimum in 
             * only a few steps (about 5 to 10).All I had to do from there was 
             * perform that same algorithm on each of the six possible wall 
             * combinations the spider could traverse. This gave very promising
             * results...But not entirely accurate results.
             * 
             * The problem statement said that M of 99 gives 1975 integer 
             * solutions and M of 100 gives 2060. By playing with how I rounded
             * my answers, I could get either of those, but never both. I 
             * probably lost two full weekend days trying to refine my 
             * algorithm to only spot true integer solutions. I was convinced
             * that square roots were causing numbers to come out as 
             * 10.0000000001 or 13.9999999998. Eventually, I had to give up and
             * turn to my last resort: research.
             * 
             * That's where I found inspiration. Someone was trying to find 
             * distances between cuboid vertices and an example someone gave
             * was a cardboard box you could unfold. I should've thought of 
             * that. 
             * 
           (E)----------------(F)
            |                  |
            |                  |
   (E)-----(H)-------(A)------(G)-----(F)
    |       |                  |       | 
    |       |                  |       |
    |       |                  |       |
   (P)-----(S)----------------(R)-----(Q)
            |                  |
            |                  |
           (P)----------------(Q)
           
             * This made the problem so much easier. I could instantly get the 
             * minimum for any of the spider's paths. At M = 99 I got 1975. At
             * M = 100, I got 2060. I set my target to 1M, hit play, and 
             * waited...
             * 
             * ...and waited...
             * 
             * I always knew I'd have to optimize on time. But I typically want 
             * to get it working first. This one wouldn't have finished in my 
             * lifetime, I think, though. So I built memoization into the 
             * solution based on ratios between height, width, and depth. I 
             * couldn't just use the H, W, and X ints themselves as I hoped to 
             * never repeat them through clever i loops. The problem there was
             * that I eventually filled up my RAM with my dictionary and 
             * accessing the memo ground it to a halt right around the time 
             * when I was getting M values that returned 800k integer 
             * solutions. So close. 
             * 
             * I turned my memoization of ratios off and instead wrote it on my
             * getHypotenuse function. This was getting re-used a lot with 
             * repeat values. That brought execution time down to around 6 
             * minutes and gave a Euler-accepted answer. That's the code in the
             * Run_slow function. 
             * 
             * The Run_fast function combines two of the dimensions, checks for
             * whether that combination plus a third dimension produces an
             * integer solution, then determines how many times that 
             * combination can be formed.
             * 
             * */
            //Run_slow();
            Run_fast();
        }
        private void Run_fast()
        {
            Func<int, int, bool> isShortestRouteAnInt = (x, yPlusZ) =>
            {
                var min = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(yPlusZ, 2));
                return CommonAlgorithms.IsInteger(min);
            };
            var target = 1000000;
            var numberOfIntegerSolutions = 0;

            int m = 1;
            while (true)
            {
                int x = m;
                for (int yPlusZ = 1; yPlusZ <= x + x; yPlusZ++)
                {
                    if (isShortestRouteAnInt(x, yPlusZ))
                    {
                        int yzCombinations = 0;
                        if(yPlusZ > x + 1)
                        {
                            yzCombinations = (x + x + 2 - yPlusZ) / 2;
                        }
                        else
                        {
                            yzCombinations = yPlusZ / 2;
                        }
                        numberOfIntegerSolutions += yzCombinations;
                    }
                }
                if(numberOfIntegerSolutions > target)
                {
                    Console.WriteLine("num solutions: {0}", numberOfIntegerSolutions);
                    PrintSolution(m.ToString());
                    return;
                }
                m++;
            }
        }
        private void Run_slow() { 
            Dictionary<(int, int), double> hypotenuses = new Dictionary<(int, int), double>();
            Func<int, int, int, bool> isShortestRouteAnInt = (width, height, depth) =>
            {
                

                Func<int, int, double> getHypotenuse = (a, b) =>
                {
                    var key = (a <= b) ? (a, b) : (b, a);
                    if (hypotenuses.ContainsKey(key)) return hypotenuses[key];
                    var aSquared = Math.Pow(a, 2);
                    var bSquared = Math.Pow(b, 2);
                    var cSquared = Math.Sqrt(aSquared + bSquared);
                    hypotenuses[key] = cSquared;
                    return cSquared;
                };

                var min = getHypotenuse(width, height + depth);
                min = Math.Min(min, getHypotenuse(width + height, depth));
                min = Math.Min(min, getHypotenuse(width + depth, height));

                return CommonAlgorithms.IsInteger(min);
            };


            var target = 1000000;
            var numberOfIntegerSolutions = 0;
            var m = 1;

            while (true)
            {


                int width = m;
                
                for (int height = 1; height <= m; height++)
                {
                    for (int depth = height; depth <= m; depth++)
                    {
                        if (isShortestRouteAnInt(width, height, depth))
                        {
                            numberOfIntegerSolutions++;
                        }
                    }
                }
                
                if (m % 10 == 0)
                {
                    Console.WriteLine("{2} max M of {0} is complete. {1} integer solutions", m, numberOfIntegerSolutions, DateTime.Now.ToString("HH-mm-ss"));
                }
                if(numberOfIntegerSolutions > target)
                {
                    int answer = m;
                    PrintSolution(answer.ToString());
                    return;
                }
                m++;
            }

			
		}
	}
}
