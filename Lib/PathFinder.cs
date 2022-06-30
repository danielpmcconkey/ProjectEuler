//#define VERBOSEOUTPUT

namespace EulerProblems.Lib
{
    public struct xyCoordinate { 
        public int x; public int y; 
        public xyCoordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public struct Node
    {
        public int mCost { get; set; } // movement cost        
        public int hCost { get; set; } // heuristic cost, or distance to the goal
        public xyCoordinate position { get; set; }
        public bool isEvaluated { get; set; }
    }
    public static class PathFinder
    {

        /// <param name="movementCosts">rows and columns representing the nodes 
        /// to traverse. the int value is each node's movement cost</param>
        public static Node[][] BuildNodesArray (int[][] movementCosts)
        {
            int width = movementCosts[0].Length;
            int height = movementCosts.Length;
            Node[][] nodes = new Node[height][];


            for (int i = 0; i < height; i++)
            {
                Node[] row = new Node[width];
                for (int j = 0; j < width; j++)
                {
                    Node n = new Node
                    {
                        mCost = movementCosts[i][j],
                        hCost = -1,
                        isEvaluated = false,
                        position = new xyCoordinate(j, i),
                    };
                    row[j] = n;
                }
                nodes[i] = row;
            }  
            return nodes;
        }
        public static int LeastTravelLeftToRight(Node[][] nodes)
        {
            /*
             * This code progressively updates every node with a heuristic 
             * cost. This is the cost to get from it to the imaginary column 
             * beyond the right-most column. To do this, find the minimal way
             * for each node to travel to the column one to the right of it, 
             * using this column's movement cost, or M cost, and the column to
             * the right's heuristic cost, or H cost. Once we've gone right to
             * left all the way to the left-most column, our least H cost in 
             * that left-most column will be our answer.
             * */

            Func<Node, int, Node> UpdateHVal = (n, hVal) =>
            {
                n.hCost = hVal;
                n.isEvaluated = true;
#if VERBOSEOUTPUT
            Console.WriteLine("updated {0},{1} to {2}", n.position.x, n.position.y, hVal); 
#endif
                return n;
            };

            int height = nodes.Length;
            int width = nodes[0].Length;
            // update the back row's h costs as their m costs, since they're already in the back row
            for (int y = 0; y < height; y++)
            {
                nodes[y][width - 1] = UpdateHVal(nodes[y][width - 1], nodes[y][width - 1].mCost);
            }

            // start with the second from the last column and move toward the first
            for (int x = width - 2; x >= 0; x--)
            {
                for (int y = 0; y < height; y++)
                {
                    // calculate the least distance of the 3 ways to get to the next row
                    var least = int.MaxValue;
                    var thisCellsMCost = nodes[y][x].mCost;

                    for(int i = 0; i < height; i++)
                    {
                        var sum = 0;
                        // add h cost of node to the right
                        sum += nodes[i][x + 1].hCost;
                        // add all the m costs of nodes in this row between our
                        // base node at X Y and this node at X i
                        if(i < y)
                        {
                            // go up i to y
                            for(int j = i; j <= y; j++)
                            {
                                sum += nodes[j][x].mCost;
                            }
                        }
                        if(i > y)
                        {
                            // go up y to i
                            for (int j = y; j <= i; j++)
                            {
                                sum += nodes[j][x].mCost;
                            }
                        }
                        if(i == y)
                        {
                            sum += thisCellsMCost;
                        }

                        if (sum < least) least = sum;
                    }

                    nodes[y][x] = UpdateHVal(nodes[y][x], least);
                }
            }
            var minimum = int.MaxValue;
            for (int i = 0; i < height; i++)
            {
                if (nodes[i][0].hCost < minimum) minimum = nodes[i][0].hCost;
            }
            return minimum;
        }
    }
}
