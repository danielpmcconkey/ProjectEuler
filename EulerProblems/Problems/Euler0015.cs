using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
    internal class Euler0015 : Euler
    {
        Dictionary<(int a, int b), long> cache;

        public Euler0015() : base()
        {
            title = "Lattice paths";
            problemNumber = 15;
            PrintTitle();
        }
        public override void Run()
        {
            Run_recurrsion();
            // Run_originalSolution();
            // Run_bruteForce();
        }
        public void Run_originalSolution()
        {
            
            /*
             * Every traversal has the same number of steps. If
             * n is the number wide and m is the number tall,
             * then every traversal must take n + m steps in
             * total. And since every step must be either a 
             * right-ward movement or a down-ward movement, we
             * can treat them as binary. So the maximum bound
             * is no greated than 2 ^ of (n + m).
             * 
             * That said, it's not that easy. You have to have
             * an equal amount of right-ward movements as down-
             * ward movements. 11110000 is fine (meaning 4
             * rights followed by 4 downs), but 11111111 is
             * not. You'd go off the grid and never get to the
             * bottom.
             * 
             * So we need to know all the "possible" binary
             * numbers of n + m digits. I'm pretty sure some
             * awesome bitwise arithematic would work like
             * lightning here, but it'd be very hard to read
             * and maintain moving forward.
             * 
             * Now, then I wrote the solution that you'll see
             * in Run_bruteForce() before I realized that 2^40
             * is 1,099,511,627,776. Doing 1 trillion of any-
             * thing takes time, especially if I'm converting
             * back and forth between strings and numbers. Still,
             * I was able to run it up to a 15 x 15 grid
             * 
             *  ___________________________
             *  |  n	 |   answer       |
             *  |--------|----------------|
             *  |  2	 |   6            |
             *  |  3	 |   20           |
             *  |  4	 |   70           |
             *  |  5	 |   252          |
             *  |  6	 |   924          |
             *  |  7	 |   3,432        |
             *  |  8	 |   12,870       |
             *  |  9	 |   48,620       |
             *  |  10	 |   184,756      |
             *  |  11	 |   705,432      |
             *  |  12	 |   2,704,156    |
             *  |  13	 |   10,400,600   |
             *  |  14	 |   40,116,600   |
             *  |  15	 |   155,117,520  |
             *  ---------------------------  
             *  
             * I took it into Google sheets and tried to find
             * the pattern. After about 20 minutes I stumbled
             * upon the pattern that you'll see below. Basically,
             * I divided each answer by the one previous and saw
             * that that result was approaching 4. So I took
             * that number that was approaching 4 and multiplied
             * it by n. That gave me an integer that rose by
             * 4 with every iteration of n. It turned out to
             * be (n * 4) / 2. From there, figuring out the
             * order of operations was easy
             * 
             * Did I cheat? Yeah. Probably. But I don't know a
             * better way to do it. I'll check the forum for the
             * elegant solution.
             * 
             * */

            int gridWidth = 20;
            int startN = 3;
            long priorAnswer = 6;   // the answer at n = 2
            long numberOfPossibleRoutes = 0;

            for (int n = startN; n <= gridWidth; n++)
            {
                double mysteryCoefficient = (n * 4) - 2;
                double multiplier = mysteryCoefficient / n;
                numberOfPossibleRoutes = (long)Math.Round(priorAnswer * multiplier, 0);
                priorAnswer = numberOfPossibleRoutes;
                string numRoutsStr = numberOfPossibleRoutes.ToString("#,###").PadLeft(20);
                Console.WriteLine(string.Format("{0}{1}", n, numRoutsStr));
            }
            Console.WriteLine();

            PrintSolution(numberOfPossibleRoutes.ToString());
            return;
        }
        public void Run_bruteForce()
        {
            // this is the brute force solution that gave me an answer up to 15 x 15
            int gridWidth = 15;

            long limit = (long)Math.Pow(2, (gridWidth * 2));   // only works with a square
            long numberOfPossibleRoutes = 0;

            for (long i = 0; i < limit; i++)
            {
                // first turn it into a binary string of length (gridWidth * 2)
                string binary = Convert.ToString(i, 2);
                binary = binary.PadLeft(gridWidth * 2, '0');
                // now add the numbers
                char[] binaryAsCharArry = binary.ToCharArray();
                long thisSum = 0;
                foreach (char c in binaryAsCharArry)
                {
                    if (c == '1') thisSum++;
                }
                if (thisSum == gridWidth) numberOfPossibleRoutes++;
            }

            PrintSolution(numberOfPossibleRoutes.ToString());
            return;
        }
        public void Run_recurrsion()
        {
            /*
             *      I--|--|--|
             *      I  |  |  |
             *      I--|--|--|
             *      I  |  |  |
             *      I--|--|--|
             *      I  |  |  |
             *      I==|==|==|
             *    
             * 3 x 3 has 6 moves. 3 left and 3 right. this can be modeled as a 
             * 6-digit binary number with 3 1s (representing right-ward 
             * movement) and 3 0s (representing down-ward movement).
             * 
             * how many possible permutations of that are there?
             * 
             *      0 0 0 1 1 1 <-- this is the picture above with 3 downs, followed by 3 rights
             *      0 0 1 0 1 1
             *      0 0 1 1 0 1
             *      0 0 1 1 1 0
             *      0 1 0 0 1 1
             *      0 1 0 1 0 1
             *      0 1 0 1 1 0
             *      0 1 1 0 0 1
             *      0 1 1 0 1 0
             *      0 1 1 1 0 0
             *      1 0 0 0 1 1
             *      1 0 0 1 0 1
             *      1 0 0 1 1 0
             *      1 0 1 0 0 1
             *      1 0 1 0 1 0
             *      1 0 1 1 0 0
             *      1 1 0 0 0 1
             *      1 1 0 0 1 0
             *      1 1 0 1 0 0
             *      1 1 1 0 0 0 
             *      
             * A 3 x 3 grid has a total of 20 possible permutations. is there 
             * a relationship to the total number of a 2 x 2 grid?
             * 
             *      0 0 1 1
             *      0 1 0 1
             *      0 1 1 0
             *      1 0 0 1
             *      1 0 1 0
             *      1 1 0 0
             *      
             *  a 2 x 2 grid has 6 possibilities. I'll not keep printing the 
             *  different binary routes yet, but it's not hard to open a 
             *  spreadsheet and see the following:
             *  
             *       _______________________
             *       | n x n  | possibile  |
             *       | square | routes     |
             *       |--------|------------|
             *       | 2      | 6          |
             *       | 3      | 20         |
             *       | 4      | 70         |
             *       -----------------------
             *  
             *  it becomes really interesting, when you stop trying to model a
             *  square
             *       ____________________________
             *       |    |    |  possibile     |
             *       | a  | b  |  routes        |
             *       |----|----|----------------|
             *       | 2  | 2  |  6             |
             *       | 2  | 3  |  10            |
             *       | 3  | 2  |  10            |
             *       | 3  | 3  |  20            |
             *       | 3  | 4  |  35            |
             *       | 4  | 3  |  35            |
             *       | 4  | 4  |  70            |
             *       ----------------------------
             *  
             *  so there you go. The number of routes in an A x B square is 
             *  equal to the number of routes in an (A - 1) x B rectangle plus 
             *  the number of routes in an A x (B - 1) rectangle
             * 
             * */
            cache = new Dictionary<(int a, int b), long>();
            long numberOfPossibleRoutes = CountRoutes(20, 20);
            PrintSolution(numberOfPossibleRoutes.ToString());
            return;
        }    
        /// <summary>
        /// a recurrsive function that figures out the paths by figuring out 
        /// the possible paths of smaller rectangles
        /// </summary>
        private long CountRoutes(int a, int b)
        {
            if (a == 0 || b == 0) return 1;
            if(cache.ContainsKey((a, b)))
            {
                return cache[(a, b)];
            }
            cache.Add((a, b), CountRoutes(a - 1, b) + CountRoutes(a, b - 1));
            return cache[(a, b)];
        }
    }
}
