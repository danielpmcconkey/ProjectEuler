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
            /*
             * Wow. This one was a spectacular challenge for me, spanning four 
             * days of consistent thought and effort for me. My initial 
             * instinct was to create a recursive "how many ways can I sum to
             * N from the factors of N" method. And that did work. But that 
             * sort of a problem is O(m^n) complexity and some of those numbers
             * <= 12,000 have a LOT of factors. So m^n get's really big, really
             * quick.
             * 
             * So then I tried dynamic programming trick #2 and tried creating 
             * a tabular solution. It sets up a table of 0 to N and computes 
             * all the ways that factors of N can multiply to N. Then, knowing
             * that, I could append 1s to my list until the sum also equaled N.
             * 
             * Quite faster, but still O(m^n). But I was now day 3 into this 
             * problem. I had tried researching throughout. Unfortunately, 
             * there's a different product-sum number that's a totally 
             * different type of weird math quirk.
             * 
             * https://en.wikipedia.org/wiki/Sum-product_number
             * 
             * I got no where and spent a long time doing it. So I ended up 
             * optimizing my m^n complexity solution and letting it run. It
             * takes 142 minutes and it spits out the right answer. Yay. That's
             * the Run_slow() function below.
             * 
             * I then went into the problem thread and learned to think about
             * this problem in a completely different way. Instead of trying to
             * attack it from the direction of K (the length of the products 
             * arary) or N (the product-sum number), I should attack it from 
             * the direction of products. 
             * 
             * Euler gives a great explanation in the thread about finding 
             * minimum and maximum boundaries. For any given K length, the 
             * minimum N must be greater than K, as N=K would only be 
             * possible if you summed up to N by using K number of 1s. And K 1s
             * would multiply only to 1. 
             * 
             * Next we find the upper boundary, meaning, when we can stop 
             * incrementing because we know any higher N couldn't possibly 
             * yield a lower minimum for that K. And the boundary that Euler
             * recommended is 2 * K because 2*K is a guaranteed solution. If K
             * is 6 then 2 * k is 12, which can be arrived at by 6 * 2 and 
             * another 6 1s. So if 2K is guaranteed, there's no point in going 
             * higher(§).
             * 
             * So, we have our absolute minimum value of 2 and absolute max of 
             * 2400, so let's multiply numbers together.
             * 
             * 2 * 2 = 4 and also sums to 4. It's sum-product length is 2, so 
             * 4 is a candidate for K = 2
             * 
             * 2 * 3 = 6. It sums to 5. Stick a one on it and now 6 is the 
             * lowest K = 3 so far.
             * 
             * 2 * 4 = 8. 2 + 4 is only six, so tack 2 1s on this time and 
             * you've got the best K = 5 solution so far.
             * 
             * Keep doing this all the way to 2 * 12,000. Any higher and you'll
             * exceed the upper boundary max K. Then go to 3 * 3, all the way 
             * up to 3 * 8,000. 4 * 4 ... 4 * 6,000. Eventually, you'll hit
             * 154 * 155 and won't be able to try any more 2-factor chains.
             * So you go to 2 * 2 * 2, 2 * 2 * 3, ... etc.
             * 
             * A little trial and error shows me that multiplying 11 numbers
             * together yields the right answer. It's funny, though. I could 
             * keep going to 12 numbers, 13, 14, whatever. Doing so yields 
             * different minimum values for specific Ks, but those numbers make
             * it into the solution through other K values and the problem only
             * wants you to sum unique answers. 
             * 
             * Maybe something that maked the answer seem a lot harder at first
             * actually makes it easier? I dunno.
             * 
             *     (§) In practice, the high boundary can be a lot lower, 
             *         but the final code runs so quickly, it doesn't matter
             * 
             * */

            //Run_slow(); // takes 2.5 hours
            Run_fast(); // Elapsed time: 27.6784 milliseconds
        }
        protected void Run_fast() 
        {
            const int limit = 12000;
            int maxProduct = limit * 2;
            int mpSqrt = (int)Math.Ceiling(Math.Sqrt(maxProduct));
            int mpCbrt = (int)Math.Ceiling(Math.Pow(maxProduct, (1/3D)));
            int mpQdrt = (int)Math.Ceiling(Math.Pow(maxProduct, (1 / 4D)));



            Func<int, int, int, Dictionary< int, int>, (bool isNew, int l) > isNewBestK = 
                (product, sum, numFactors, dict) =>
            {
                // 2 * 3 * 3 = 18
                // 2 + 3 + 3 = 8
                // num 1s to add = 10
                // solution length = 13
                // solution length = product - sum + num factors
                var length = product - sum + numFactors;
                if(dict.ContainsKey(length))
                {
                    var currentBest = dict[length];
                    if(product < currentBest) return (true, length);
                    return (false, length);
                }
                return (true, length);
            };

            Dictionary<int, int> bestFoundSoFar = new Dictionary<int, int>();

            Func<int, int, int, bool> updateDict = (product, sum, numFactors) =>
            {
                var result = isNewBestK(product, sum, numFactors, bestFoundSoFar);
                if (result.isNew && result.l <= limit && result.l > 1)
                    bestFoundSoFar[result.l] = product;
                return true;
            };
            
            
            // calculate all the n * m * l * k * j * ... products up to the max product
            
            for (int n = 2; n <= mpSqrt; n++)
            {
                for (int m = n; true; m++)
                {
                    var productN = n * m;
                    var sumN = n + m;
                    updateDict(productN, sumN, 2);
                    if (productN > maxProduct) break;

                    for (int l = m; true; l++)
                    {
                        var productL = n * m * l;
                        var sumL = n + m + l;
                        updateDict(productL, sumL, 3);
                        if (productL > maxProduct) break;

                        for (int k = l; true; k++)
                        {
                            var productK = n * m * l * k;
                            var sumK = n + m + l + k;
                            updateDict(productK, sumK, 4);
                            if (productK > maxProduct) break;

                            for (int j = k; true; j++)
                            {
                                var productJ = n * m * l * k * j;
                                var sumJ = n + m + l + k + j;
                                updateDict(productJ, sumJ, 5);
                                if (productJ > maxProduct) break;

                                for (int i = j; true; i++)
                                {
                                    var productI = n * m * l * k * j * i;
                                    var sumI = n + m + l + k + j + i;
                                    updateDict(productI, sumI, 6);
                                    if (productI > maxProduct) break;

                                    for (int h = i; true; h++)
                                    {
                                        var productH = n * m * l * k * j * i * h;
                                        var sumH = n + m + l + k + j + i + h;
                                        updateDict(productH, sumH, 7);
                                        if (productH > maxProduct) break;

                                        for (int g = h; true; g++)
                                        {
                                            var productG = n * m * l * k * j * i * h * g;
                                            var sumG = n + m + l + k + j + i + h + g;
                                            updateDict(productG, sumG, 8);
                                            if (productG > maxProduct) break;

                                            for (int f = g; true; f++)
                                            {
                                                var productF = n * m * l * k * j * i * h * g * f;
                                                var sumF = n + m + l + k + j + i + h + g + f;
                                                updateDict(productF, sumF, 9);
                                                if (productF > maxProduct) break;

                                                for (int e = f; true; e++)
                                                {
                                                    var productE = n * m * l * k * j * i * h * g * f * e;
                                                    var sumE = n + m + l + k + j + i + h + g + f + e;
                                                    updateDict(productE, sumE, 10);
                                                    if (productE > maxProduct) break;

                                                    for (int d = e; true; d++)
                                                    {
                                                        var productD = n * m * l * k * j * i * h * g * f * e * d;
                                                        var sumD = n + m + l + k + j + i + h + g + f + e + d;
                                                        updateDict(productD, sumD, 11);
                                                        if (productD > maxProduct) break;                                                        
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }



            var answer = bestFoundSoFar
                .Values
                .Distinct()
                .Sum();
            var burp = bestFoundSoFar.OrderByDescending(x => x.Key);
            PrintSolution(answer.ToString());
            return;
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
            //if(factorProductSumsMemo.ContainsKey(n)) return factorProductSumsMemo[n];

            Func<int, int[], bool> doesSumArrayMultiplyToN = (n, sumArray) =>
            {
                var product = sumArray.Aggregate(1, (a, b) => a * b);
                return (product == n);
            };
            var factors = CommonAlgorithms.GetFactors(n).OrderByDescending(x => x).ToArray();
            var factorProductSums = GetDedupedWaysToSumN(n, factors)
                .Where(x => doesSumArrayMultiplyToN(n, x))
                .ToArray();
            //factorProductSumsMemo[n] = factorProductSums;
            return factorProductSums;
        }
        private int[][] GetDedupedWaysToSumN(int n, int[] numbersBank)
        {
            //if (dedupedWaysToSumMemo.ContainsKey(n)) return dedupedWaysToSumMemo[n];

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
            //dedupedWaysToSumMemo[n] = result;
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
