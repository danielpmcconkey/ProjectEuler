using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
    internal class Euler0015 : Euler
    {
        public Euler0015() : base()
        {
            title = "Lattice paths";
            problemNumber = 15;
            PrintTitle();
        }
        public override void Run()
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
    }
}
