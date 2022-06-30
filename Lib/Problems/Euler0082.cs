//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0082 : Euler
	{
		public Euler0082() : base()
		{
			title = "Path sum: three ways";
			problemNumber = 82;
		}
		protected override void Run()
		{
            /*
             * This one was tough for me. I built this out 3 different ways. 
             * All 3 gave the right answer to the sample 5 x 5 matrix. Only the
             * last one gave a Euler accepted answer. The problem with an 80 x
             * 80 matrix is that there's no way to step through your code and
             * see which of your assumptions is wrong. For me, it turns out 
             * that, somewhere in these 6,400 numbers there must be a couple of
             * large numbers near the ideal path that were big enough to go 
             * around by about 3 steps. That's speculation. 
             * 
             * My first attempt was an elaborate mechanism that sought to 
             * calculate the cost of each node. I would declare an end point, 
             * then calculate the cost of each node that was adjacent to the 
             * end point, then calculate the cost of those nodes' adjacent 
             * nodes to already known nodes. So on and so forth until you get 
             * to the left column and choose the lowest cost among that column.
             * Do this over and over using different nodes on the right column,
             * and you'll eventually have your absolute lowest number. But I 
             * think my cost algorithm didn't take into account going around 
             * big numbers.
             * 
             * Next, I built somethnig in Excel that simply looked at each cell
             * in the penultimate column and calculated its cost for getting to 
             * the back column. Then I'd shift both operations left, using the
             * M cost of the current row and the H cost of the target row. I was 
             * smart enough to anticipate going around big numbers, but I don't 
             * think I used a wide enough check. I think I only took the least
             * of going up 3 to down 3 before turning right.
             * 
             * The final solution just looks at every possible vertical 
             * movement before turning right. I don't know how many extra
             * calculations I made, but it works.
             * 
             * */
            const string filePath = @"E:\ProjectEuler\ExternalFiles\p082_matrix.txt";
            string[] lines = {
                "131,673,234,103,18",
                "201,96,342,965,150",
                "630,803,746,422,111",
                "537,699,497,121,956",
                "805,732,524,37,331" };
            lines = File.ReadLines(filePath).ToArray();
            int[][] intRows = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                string row = lines[i];
                string rowTrimmed = row.Trim();
                if (rowTrimmed.Length > 1)
                {
                    string[] intsAsStrings = rowTrimmed.Split(',');
                    int[] rowOfInts = new int[intsAsStrings.Length];
                    for (int j = 0; j < intsAsStrings.Length; j++)
                    {
                        rowOfInts[j] = int.Parse(intsAsStrings[j]);
                    }
                    intRows[i] = rowOfInts;
                }
            }

            
            
            var nodes = PathFinder.BuildNodesArray(intRows);
            int answer = PathFinder.LeastTravelLeftToRight(nodes);
            
			PrintSolution(answer.ToString());
			return;
		}
    }
}
