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
        public int fCost { get; set; } // cost to get from start to this node
        public xyCoordinate position { get; set; }
        public xyCoordinate arrivedAtVia { get; set; }
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
                        fCost = int.MaxValue,
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
        public static int DijkstrasLeastPathCost(Node[][] nodes)
        {
            xyCoordinate start = new xyCoordinate { x = 0, y = 0 };
            xyCoordinate end = new xyCoordinate { x = nodes[0].Length - 1, y = nodes.Length - 1 };
            return DijkstrasLeastPathCost(nodes, start, end);
        }
        public static int DijkstrasLeastPathCost(Node[][] nodes, xyCoordinate start, xyCoordinate end)
        {
            Func<Node[][], xyCoordinate, int, xyCoordinate, Node[][]> setFCost = 
                (nodes, pos, cost, arrivedAtVia) =>
            {
                nodes[pos.y][pos.x].fCost = cost;
                nodes[pos.y][pos.x].arrivedAtVia = arrivedAtVia;
                return nodes;
            };
            Func<Node[][], xyCoordinate, Node> getNodeAt = (nodes, pos) =>
            {
                // do not use this to write
                return nodes[pos.y][pos.x];
            };            

            var width = nodes[0].Length;
            var height = nodes.Length;

            Dictionary <xyCoordinate, int > priorityQue = new Dictionary<xyCoordinate, int>();
            HashSet<xyCoordinate> finished = new HashSet<xyCoordinate>();

            nodes = setFCost(nodes, start, 0, start);
            priorityQue.Add(start, 0);
#if VERBOSEOUTPUT
            var edgesEvaluated = 0;
            var nodesExamined = 0;
#endif

            while (true)
            {
                // take the lowest value in the priority queue
                var lowestEntry = priorityQue.OrderBy(x => x.Value).First();

                var thisNode = getNodeAt(nodes, lowestEntry.Key);
                var arrivedAt = thisNode.arrivedAtVia;
                var travelCost = thisNode.mCost + thisNode.fCost;
                var up = new xyCoordinate(lowestEntry.Key.x, lowestEntry.Key.y - 1);
                var down = new xyCoordinate(lowestEntry.Key.x, lowestEntry.Key.y + 1);
                var right = new xyCoordinate(lowestEntry.Key.x + 1, lowestEntry.Key.y);
                var left = new xyCoordinate(lowestEntry.Key.x - 1, lowestEntry.Key.y);

                // get all neighbors except the one currently in the "arrived at via" location
                var neighbors = new List<xyCoordinate>();
                if (lowestEntry.Key.y > 0 && Equals(arrivedAt, up) == false) neighbors.Add(up);
                if (lowestEntry.Key.y < height - 1 && Equals(arrivedAt, down) == false) neighbors.Add(down);
                if (lowestEntry.Key.x > 0 && Equals(arrivedAt, left) == false) neighbors.Add(left);
                if (lowestEntry.Key.x < width - 1 && Equals(arrivedAt, right) == false) neighbors.Add(right);

                // evaluate all neighbors
                foreach(var n in neighbors)
                {
#if VERBOSEOUTPUT
                    edgesEvaluated++;
#endif
                    if (getNodeAt(nodes, n).fCost > travelCost)
                    {
                        // update it and put it on the priority queue
#if VERBOSEOUTPUT
                        Console.WriteLine("Updated {0},{1} to {2}", n.x, n.y, travelCost);
#endif
                        nodes = setFCost(nodes, n, travelCost, lowestEntry.Key);
                        priorityQue[n] = travelCost;
                    }
                }
                // remove this entry from our priority queue and move it to finished
                priorityQue.Remove(lowestEntry.Key);
                finished.Add(lowestEntry.Key);
#if VERBOSEOUTPUT
                nodesExamined++;
#endif

                if (Equals(lowestEntry.Key, end))
                {
                    // finished, now tabulate the result
                    int sum = thisNode.mCost;
#if VERBOSEOUTPUT
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Sum = {0},{1} M cost of {2}  ...  {3}", end.x, end.y, sum, sum);
#endif

                    var stepC = arrivedAt;
                    while(true)
                    {
                        var stepN = getNodeAt(nodes, stepC);
                        sum += stepN.mCost;
#if VERBOSEOUTPUT
                        Console.WriteLine("      {0},{1} M cost of {2}  ...  {3}", 
                            stepC.x, stepC.y, stepN.mCost, sum);
#endif
                        stepC = stepN.arrivedAtVia;

                        if (Equals(stepN.position, start))
                        {
#if VERBOSEOUTPUT
                            Console.WriteLine("Total edges evaluated: {0}", edgesEvaluated);
                            Console.WriteLine("Total nodes examined: {0}", nodesExamined);
#endif
                            return sum;
                        }
                    }
                }
            }

            throw new ArgumentException("This graph cannot be traversed.");
        }
        public static int AStarLeastPathCost(
            Node[][] nodes, 
            Func<Node[][], xyCoordinate, Node[][]> heuristicFunction)
        {
            xyCoordinate start = new xyCoordinate { x = 0, y = 0 };
            xyCoordinate end = new xyCoordinate { x = nodes[0].Length - 1, y = nodes.Length - 1 };
            return AStarLeastPathCost(nodes, heuristicFunction, start, end);
        }

        public static int AStarLeastPathCost(
            Node[][] nodes, 
            Func<Node[][], xyCoordinate, Node[][]> heuristicFunction, 
            xyCoordinate start, 
            xyCoordinate end)
        {
            var width = nodes[0].Length;
            var height = nodes.Length;

            Func<Node[][], xyCoordinate, int, xyCoordinate, Node[][]> setFCost =
                (nodes, pos, cost, arrivedAtVia) =>
                {
                    nodes[pos.y][pos.x].fCost = cost;
                    nodes[pos.y][pos.x].arrivedAtVia = arrivedAtVia;
                    return nodes;
                };
            Func<Node[][], xyCoordinate, Node> getNodeAt = (nodes, pos) =>
            {
                // do not use this to write
                return nodes[pos.y][pos.x];
            };
            


            nodes = heuristicFunction(nodes, end);

            Dictionary<xyCoordinate, int> priorityQue = new Dictionary<xyCoordinate, int>();
            HashSet<xyCoordinate> finished = new HashSet<xyCoordinate>();

            nodes = setFCost(nodes, start, 0, start);
            priorityQue.Add(start, 0);
#if VERBOSEOUTPUT
            var edgesEvaluated = 0;
            var nodesExamined = 0;
#endif

            while (true)
            {
                // take the lowest value in the priority queue
                var lowestEntry = priorityQue.OrderBy(x => x.Value).First();

                var thisNode = getNodeAt(nodes, lowestEntry.Key);
                var arrivedAt = thisNode.arrivedAtVia;
                var travelCost = thisNode.mCost + thisNode.fCost;
                var heuristicCost = thisNode.hCost;
                var up = new xyCoordinate(lowestEntry.Key.x, lowestEntry.Key.y - 1);
                var down = new xyCoordinate(lowestEntry.Key.x, lowestEntry.Key.y + 1);
                var right = new xyCoordinate(lowestEntry.Key.x + 1, lowestEntry.Key.y);
                var left = new xyCoordinate(lowestEntry.Key.x - 1, lowestEntry.Key.y);

                // get all neighbors except the one currently in the "arrived at via" location
                var neighbors = new List<xyCoordinate>();
                if (lowestEntry.Key.y > 0 && Equals(arrivedAt, up) == false) neighbors.Add(up);
                if (lowestEntry.Key.y < height - 1 && Equals(arrivedAt, down) == false) neighbors.Add(down);
                if (lowestEntry.Key.x > 0 && Equals(arrivedAt, left) == false) neighbors.Add(left);
                if (lowestEntry.Key.x < width - 1 && Equals(arrivedAt, right) == false) neighbors.Add(right);

                // evaluate all neighbors
                foreach (var n in neighbors)
                {
#if VERBOSEOUTPUT
                    edgesEvaluated++;
#endif
                    if (getNodeAt(nodes, n).fCost > travelCost)
                    {
                        // update it and put it on the priority queue
#if VERBOSEOUTPUT
                        Console.WriteLine("Updated {0},{1} to {2}", n.x, n.y, travelCost);
#endif
                        nodes = setFCost(nodes, n, travelCost, lowestEntry.Key);
                        priorityQue[n] = travelCost + heuristicCost;
                    }
                }
                // remove this entry from our priority queue and move it to finished
                priorityQue.Remove(lowestEntry.Key);
                finished.Add(lowestEntry.Key);
#if VERBOSEOUTPUT
                nodesExamined++;
#endif

                if (Equals(lowestEntry.Key, end))
                {
                    // finished, now tabulate the result
                    int sum = thisNode.mCost;
#if VERBOSEOUTPUT
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Sum = {0},{1} M cost of {2}  ...  {3}", end.x, end.y, sum, sum);
#endif

                    var stepC = arrivedAt;
                    while (true)
                    {
                        var stepN = getNodeAt(nodes, stepC);
                        sum += stepN.mCost;
#if VERBOSEOUTPUT
                        Console.WriteLine("      {0},{1} M cost of {2}  ...  {3}", 
                            stepC.x, stepC.y, stepN.mCost, sum);
#endif
                        stepC = stepN.arrivedAtVia;

                        if (Equals(stepN.position, start))
                        {
#if VERBOSEOUTPUT
                            Console.WriteLine("Total edges evaluated: {0}", edgesEvaluated);
                            Console.WriteLine("Total nodes examined: {0}", nodesExamined);
#endif
                            return sum;
                        }
                    }
                }
            }

            throw new ArgumentException("This graph cannot be traversed.");
        }
    }
}
