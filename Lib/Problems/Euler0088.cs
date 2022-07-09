//#define VERBOSEOUTPUT
using System.Text;

namespace EulerProblems.Lib.Problems
{
	public class Euler0088 : Euler
	{
        private Dictionary<int, int[][]> dedupedWaysToSumMemo;
        private Dictionary<int, int[][]> factorProductSumsMemo;

        public Euler0088() : base()
		{
			title = "Product-sum numbers";
			problemNumber = 88;
		}
        protected override void Run() 
        {
            Run_slow(); // takes 2.5 hours
        }
        protected void Run_old()
        {
            Func<int[][], bool> printList = (l) =>
            {
                string p = "[";
                foreach (var i in l)
                {
                    p += "\n   [";
                    foreach (var n in i)
                    {
                        p += string.Format("{0}, ", n);
                    }
                    p += "],";
                }
                p += "\n];";
                Console.WriteLine(p);
                return true;
            };
            Func<int[], string> printWay = (way) =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                for (int i = 0; i < way.Length; i++)
                {
                    var n = way[i];
                    sb.Append(string.Format("{0}{1}", n, i < way.Length - 1 ? ", " : ""));
                }
                sb.Append("],");
                return sb.ToString();
            };
            dedupedWaysToSumMemo = new Dictionary<int, int[][]>();
            factorProductSumsMemo = new Dictionary<int, int[][]>();

            var sum = 0;
            var limit = 12000;

            var test = CommonAlgorithms.GetWaysToSumANumber(30, new int[] { 30, 15, 10, 6, 5, 3, 2, 1 });

            for (int i = 30; i < 100; i++)
            {
                var waysAtI = GetAllProductFactorSums(i);
                foreach (var way in waysAtI)
                {
                    Console.WriteLine("i: {0}; {1}", i, printWay(way));
                }
            }

            for (int k = 2; k <= limit; k++)
            {
                var hasFonudMin = false;
                for (int j = 2; hasFonudMin == false; j++)
                {
                    hasFonudMin = false;
                    var waysAtK = GetAllProductFactorSums(j).Where(x => x.Length == k);
                    var hasWayAtK = (waysAtK.Count() > 0) ? true : false;
                    if (hasWayAtK)
                    {
                        var solution = waysAtK.First();
                        hasFonudMin = true;
                        Console.WriteLine("k: {0}; j: {1}; {2}", k, j, printWay(solution));
                        sum += j;
                        break;
                    }
                }
            }

            //var sexy = GetAllProductFactorSums(8);
            //printList(sexy);
            int answer = sum;
            PrintSolution(answer.ToString());
            return;
        }
        protected void Run_slow()
		{
            Func<int[], string> printWay = (way) =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                for (int i = 0; i < way.Length; i++)
                {
                    var n = way[i];
                    sb.Append(string.Format("{0}{1}", n, i < way.Length -1 ? ", ": ""));
                }
                sb.Append("]");                
                return sb.ToString();
            };
            Func<int[][], string> printList = (l) =>
            {
                string p = "[";
                foreach (var i in l)
                {
                    p += "\n   ";
                    p += printWay(i);
                }
                p += "\n];";
                
                return p;
            };
            
            Func<int, int[], int[]> getProductSumLengths = (n, productsOfN) =>
            {
                //if (memo.ContainsKey(n)) return memo[n];

                Func<int[], int[][], int[][]> addWayToTableCell = (way, cell) =>
                {
                    int[][] newCell = new int[cell.Length + 1][];
                    for (int i = 0; i < cell.Length; i++)
                    {
                        newCell[i] = cell[i];
                    }
                    newCell[cell.Length] = way;
                    return newCell;
                };
                Func<int, int[], int[]> appendProductToWay = (product, way) =>
                {
                    if (way.Length == 0) return new int[] { product };

                    int[] newWay = new int[way.Length + 1];
                    for (int i = 0; i < way.Length; i++)
                    {
                        newWay[i] = way[i];
                    }
                    newWay[way.Length] = product;
                    return newWay;
                };
                
                Func<int[], int> multiplyArrayElements = (arr) =>
                {
                    return arr.Aggregate(1, (a, b) => a * b);
                };
                // initialize a table
                int[][][] table = new int[n + 1][][];
                for (int i = 0; i < table.Length; i++)
                {
                    table[i] = null;
                    if(productsOfN.Contains(i)) table[i] = new int[1][] { new int[1] {i} };
                }
                for (int i = 2; i < table.Length; i++)
                {
                    if (table[i] == null) continue;
                    for (int j = 0; j < productsOfN.Length; j++)
                    {
                        int product = productsOfN[j];
                        if (product == 1) continue; // skip the 1s or you go in an infinite loop adding 1 to everything
                        for (int k = 0; k < table[i].Length; k++)
                        {
                            var productOfArrayItems = multiplyArrayElements(table[i][k]);
                            var productTimesArray = product * productOfArrayItems;
                            if (productTimesArray > n) continue;
                            
                            var newWay = appendProductToWay(product, table[i][k]);
                            var targetCell = table[productTimesArray];
                            
                            if (targetCell == null) table[productTimesArray] = new int[1][] { newWay };
                            else
                            {
                                targetCell = addWayToTableCell(newWay, targetCell);
                                table[productTimesArray] = targetCell;
                            }
                        }
                    }
                }
                // now get the lengths
                var finalCell = table[n];


                var solutionCount = finalCell.Length;
                int[] solutions = new int[solutionCount];

                for (int i = 0; i < solutionCount; i++)
                {
                    var s = finalCell[i];
                    var arraySum = s.Sum();
                    if (s.Length > 1 && arraySum == n)
                    {
                        // don't need to add any 1s here
                        solutions[i] = s.Length;
                    }
                    if (arraySum < n)
                    {
                        solutions[i] = s.Length + n - arraySum;
                    }
                }

                return solutions;
            };

            

            var sum = 0;
            var limit = 12000;
            
            var currentK = 1;
            bool[] checks = new bool[limit + 1];
            checks[0] = true;
            checks[1] = true;
            Dictionary<int,int> boundaryBreakers = new Dictionary<int, int>();
            
            for(int i = 4; true; i++)
            {
                var psLengths = getProductSumLengths(i, CommonAlgorithms.GetFactors(i));
                for(int j = 0; j < psLengths.Length; j++)
                {
                    if (psLengths[j] > limit) continue;
                    checks[psLengths[j]] = true;
                    if(!boundaryBreakers.ContainsKey(psLengths[j]))
                    {
                        boundaryBreakers[psLengths[j]] = i;
                    }
                }
                
                if (i % 10 == 0) Console.WriteLine("i: {0}; k: {1}", i, currentK);
                if (checks.Where(x => x == false).Any() == false) break;
            }
            var skips = checks.Where(x => x == false);
            if(skips.Any())
            {
                throw new Exception("Skipped");
            }


            var answer = boundaryBreakers.Values.Distinct().Sum();
            PrintSolution(answer.ToString());
            return;

        }
        private int[][] GetAllProductFactorSums(int n)
        {
            if(factorProductSumsMemo.ContainsKey(n)) return factorProductSumsMemo[n];

            Func<int, int[], bool> doesSumArrayMultiplyToN = (n, sumArray) =>
            {
                var product = sumArray.Aggregate(1, (a, b) => a * b);
                return (product == n);
            };
            var factors = CommonAlgorithms.GetFactors(n).OrderByDescending(x => x).ToArray();
            var factorProductSums = GetDedupedWaysToSumN(n, factors)
                .Where(x => doesSumArrayMultiplyToN(n, x))
                .ToArray();
            factorProductSumsMemo[n] = factorProductSums;
            return factorProductSums;
        }
        private int[][] GetDedupedWaysToSumN(int n, int[] numbersBank)
        {
            if (dedupedWaysToSumMemo.ContainsKey(n)) return dedupedWaysToSumMemo[n];

            var withDupes = CommonAlgorithms.GetWaysToSumANumber(n, numbersBank);
            HashSet<string> uniqueEntries = new HashSet<string>();
            List<int[]> deduped = new List<int[]>();
            for(int i = 0; i < withDupes.Length; i++)
            {
                Array.Sort(withDupes[i]);
                var key = string.Join(",", withDupes[i]);
                if (uniqueEntries.Contains(key)) continue;
                deduped.Add(withDupes[i]);
                uniqueEntries.Add(key);
            }
            var result = deduped.ToArray();
            dedupedWaysToSumMemo[n] = result;
            return result;
        }
        private int[][] GetWaysToSumANumber(
            int n,
            int[] numbersBank,
            Dictionary<int, int[][]> memo = null)
        {
            if (memo == null) memo = new Dictionary<int, int[][]>();
            else if (memo.ContainsKey(n))
            {
                return memo[n];
            }
            // base cases
            if (n == 0) return new int[][] { new int[0] }; // success, we've evenly reached our original N
            if (n < 0) return null;

            Func<int, int[][], int[][]> pushNumberOntoRemainderWays = (n, rw) =>
            {
                var result = new int[rw.Length][];
                for (int i = 0; i < rw.Length; i++)
                {
                    var thisWay = rw[i];
                    if (thisWay == null) continue;
                    int[] thisWayPlus = new int[thisWay.Length + 1];
                    for (int j = 0; j < thisWay.Length; j++)
                    {
                        thisWayPlus[j] = thisWay[j];
                    }
                    thisWayPlus[thisWay.Length] = n;
                    result[i] = thisWayPlus;
                }
                return result;
            };


            List<int[]> resultList = new List<int[]>();
            for (int i = 0; i < numbersBank.Length; i++)
            {
                var remainder = n - numbersBank[i];
                var remainderWays = GetWaysToSumANumber(remainder, numbersBank, memo);
                if (remainderWays == null) continue;
                var augmentedWays = pushNumberOntoRemainderWays(numbersBank[i], remainderWays);
                resultList.AddRange(augmentedWays);
            }
            //Console.WriteLine("n: {0}; return: ", n);
            var result = resultList.ToArray();
            memo[n] = result;
            return result;
        }
    }
}
