//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0083 : Euler
	{
		public Euler0083() : base()
		{
			title = "Path sum: four ways";
			problemNumber = 83;
		}
		protected override void Run()
		{
            /*
             * This one took many days for me and I have no one to blame but
             * myself. I knew about Dijkstra's algorithm from having read the
             * problem threads of the past few problems. I knew about A* from
             * video game AI programming. But I don't know why I couldn't put
             * those together. 
             * 
             * With A*, I could only think in terms of grid squares in a video 
             * game where movement cost was fixed at N per vertical / 
             * horizontal or Sqrt(2) for diagonal. I kept thinking of the 
             * movement cost as a "terrain modifier" and I could never figure 
             * out a heuristic cost algorithm that worked (§). I watched this
             * youtube video from Computerphile on A*
             * 
             * https://www.youtube.com/watch?v=ySN5Wnu88nE
             * 
             * And it solidified in my mind that A* wouldn't work here. He was 
             * using the euclidian distances between nodes as his heuristic
             * function. That wouldn't work. I didn't have a physical map in 
             * space. I had a mathematical matrix. So I gave up on A*.
             * 
             * As for Dijkstra's algorithm, all I really understood about it 
             * was that it was an inefficient "ancestor" to A*. I never really
             * cracked open the hood on that one and that was dumb of me.
             * 
             * So I rolled my own. And what kept tripping me up was the 
             * infinite number of possibilities for going around an obstacle. 
             * This was very hard to account for and the prior problem showed 
             * me that edge cases exist in this matrix. I settled on an 
             * elaborate means that calculated the shortest path from any node
             * to any other node within 2 spaces, using an acceptable number of
             * moves. I then used it to gradually sweep through the graph, 
             * updating final costs of each space. 
             * 
             * I wrote it in javascript as I wasn't on my dev PC at the time, 
             * and was suprised by how fast it ran. It gave me the 
             * Euler-accepted answer and reading the problem thread forced me 
             * to reevaluate my understanding of both Dijkstra and A*. In the 
             * end, I implemented both. And, at least for this graph, the 
             * difference was minimal between the two. 
             * 
             * On average, A* beats Dijkstra by 3 milliseconds.
             *              * 
             * Dijkstra's algorithm:
             * Total edges evaluated: 18,879
             * Total nodes examined: 6,399
             * Elapsed time: 37.9915 milliseconds
             * 
             * A* algorithm
             * Total edges evaluated: 10,619
             * Total nodes examined: 3,576
             * Elapsed time: 33.1465 milliseconds
             * 
             * 
             *   (§) as it turns out, getting a heuristic cost modifier
             *       that produced a correct answer was trial and error
             *       anyway, so maybe I gave up on A* too quickly
             * 
             * */
            const string filePath = @"E:\ProjectEuler\ExternalFiles\p083_matrix.txt";
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
            // create a heuristic calculation for the A* to use
            Func<Node[][], xyCoordinate, Node[][]> heuristicFunction = (nodes, end) =>
            {
                // find the average value across all nodes
                var rowSums = new int[nodes.Length];
                for (int i = 0; i < nodes.Length; i++)
                {
                    rowSums[i] = nodes[i].Sum(x => x.mCost);
                }
                var aveMCost = rowSums.Average() / nodes[0].Length;
                var heuristicModifier = 0.5; // I had to play with this before I found one that gave the right answer and lowered the number of evaluations
                var heuristicCostMultiplier = (int)Math.Round(aveMCost * heuristicModifier);
                // assign each nodes H cost to be the average times the manhattan distance from the goal
                for (int y = 0; y < nodes.Length; y++)
                {
                    for (int x = 0; x < nodes.Length; x++)
                    {
                        int xSpaces = Math.Abs(end.x - x);
                        int ySpaces = Math.Abs(end.y - y);
                        nodes[y][x].hCost = (xSpaces + ySpaces) * heuristicCostMultiplier;
                    }
                }
                return nodes;
            };
            var nodes = PathFinder.BuildNodesArray(intRows);
            

            int answer = PathFinder.AStarLeastPathCost(nodes, heuristicFunction);
            PrintSolution(answer.ToString());
			return;
		}
	}
}
