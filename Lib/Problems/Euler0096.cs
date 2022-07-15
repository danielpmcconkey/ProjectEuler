//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0096 : Euler
	{
		public Euler0096() : base()
		{
			title = "Su Doku";
			problemNumber = 96;
		}
		protected override void Run()
		{
            /*
             * This one was a LOT of fun for me. I'll echo what someone in the 
             * problem thread said. I was very happy to see a challenge that 
             * focused on the programming rather than the mathematics. (Though
             * while reading that thread you can see there's a clear graph 
             * search math-y solution that many used. I didn't even think along
             * those lines. I had a puzzle game and that sounded like business
             * rules to learn and implement. This is my wheelhouse.
             * 
             * Nothing really fancy to say about the solution. I've since 
             * learned a little Sudoku lingo and have come to realize that my
             * solution spots all the Naked singles and moves straight to guess
             * work after we run out of those. I didn't know it at the time, 
             * but I could've continued with hidden singles, house interactions,
             * naked pairs, naked triplets, hidden pairs, naked quadruplets, 
             * and, most enigmatically, X-wings. I thought about later 
             * implementing those, but was rather proud of how my guess work
             * recursion plays out. 
             * 
             * Bottom line, I'm happy with this.
             * 
             * */

            const string filePath = @"E:\ProjectEuler\ExternalFiles\p096_sudoku.txt";
            string[] lines = File.ReadLines(filePath).ToArray();
            int gridCount = 0;
            int answer = 0;
            for (int i = 1; i < lines.Length; i += 10)
            {
                Sudoku s = new Sudoku();
                gridCount++;
                for(int row = 0; row < 9; row++)
                {
                    var chars = lines[i + row].ToCharArray();
                    for(int column = 0; column < 9; column++)
                    {
                        s.AddPoint(column, row, chars[column] - (int)'0');
                    }
                }
#if VERBOSEOUTPUT
                s.PrintTable();
#endif
                var solution = s.Solve();
                if (solution.isSolved == false) throw new Exception("ya done goofed");
#if VERBOSEOUTPUT
                s.PrintTable();
                Console.WriteLine("Grid {0} solution is {1}. It took {2} turns and {3} guesses", 
                    gridCount, solution.EulerSolution, s.numTurns, s.numGuesses);
#endif

                answer += solution.EulerSolution;
            }
            
			PrintSolution(answer.ToString());
			return;
		}
	}
}
