//#define VERBOSEOUTPUT
using System.Text;

namespace EulerProblems.Lib.Problems
{
    
	public class Euler0093 : Euler
	{
		public Euler0093() : base()
		{
            /*
             * This one was both fun and terrifying. I knew C# doesn't have an 
             * eval function so I was dreading the process of evaluating 
             * different combinations. In the end, I decided to google "C# 
             * eval" just in case I was wrong and stumbled upod a "cheater" 
             * eval function in Stack Overflow. I guess .Net data tables can be
             * used for some weird crap.
             * 
             * Once there, coming up with the possible permutations of digits, 
             * operators, and parentheses wasn't hard. I was worried this would
             * take forever to run. My initial version ran in 70-something 
             * seconds and gave the right answer. That was a relief on both 
             * fronts. I still decided to optimize a little.
             * 
             * My original function used a boolean array for parentheses. 
             * Something the first bool meant surround the 0th and 1st digits,
             * the second bool meant surround the 0th and 2nd digits, and so 
             * on. By having multiple true records you could result in stacked
             * parentheses that the problem requires like in the example in the
             * problem:
             * 
             *      (4 * (1 + 3)) / 2
             *      
             * This gave a correct answer, but was wasteful and generated many 
             * equivalent expressions. After reading the problem thread, I saw 
             * a user describe that you really only had 5 different 
             * configurations (as opposed to the 32 that my mechanism created).
             * These five are shown in the switch statement in my K loop below.
             * 
             * The code now runs in about 12 seconds and that's good enough for
             * me.
             * 
             * */

            title = "Arithmetic expressions";
			problemNumber = 93;
		}
        public static double Evaluate(string expression)
        {
            // stolen from https://stackoverflow.com/questions/6052640/is-there-an-eval-function-in-c
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }
        protected override void Run()
        {
            Func<int[][]> getAllDigitsPermutations = () =>
            {
                var counter = 0;
                int[][] allDigits = new int[210][];
                for (int i = 0; i < 10; i++)
                {
                    for (int j = i + 1; j < 10; j++)
                    {
                        for (int k = j + 1; k < 10; k++)
                        {
                            for (int l = k + 1; l < 10; l++)
                            {
                                allDigits[counter] = new int[] { i, j, k, l };
                                counter++;
                            }
                        }
                    }
                }
                return allDigits;
            };
            Func<string[][]> getAllOperatorPermutations = () =>
            {
                var operators = new string[] { "+", "-", "*", "/" };
                var counter = 0;
                string[][] allOperators = new string[64][]; // 4 ^ 3, you want to take 3 of any 4
                for (int i = 0; i < operators.Length; i++)
                {
                    var o_i = operators[i];
                    for (int j = 0; j < operators.Length; j++)
                    {
                        var o_j = operators[j];
                        for (int k = 0; k < operators.Length; k++)
                        {
                            var o_k = operators[k];
                            allOperators[counter] = new string[] { o_i, o_j, o_k };
                            counter++;
                        }
                    }
                }
                return allOperators;
            };            
            Func<int[], int> findHighest = (distinctResults) =>
            {
                int highest = 0;
                for (int i = 0; i < distinctResults.Length; i++)
                {
                    if (distinctResults[i] == highest + 1) highest++;
                    else return highest;
                }
                return highest;
            };


            var operatorsPermutations = getAllOperatorPermutations();
            var allDigitsPermutations = getAllDigitsPermutations();

            var globalHighest = 0;
            var globalDigits = new int[] { 0, 0, 0, 0 };

            for (int l = 0; l < allDigitsPermutations.Length; l++)
            {
                var digits = allDigitsPermutations[l];
                var digitsPermutations = CommonAlgorithms.GetAllPermutationsOfArray(digits);

                List<int> integerResults = new List<int>();

                for (int i = 0; i < digitsPermutations.Length; i++)
                {
                    var digit0 = digitsPermutations[i][0];
                    var digit1 = digitsPermutations[i][1];
                    var digit2 = digitsPermutations[i][2];
                    var digit3 = digitsPermutations[i][3];
                    for (int j = 0; j < operatorsPermutations.Length; j++)
                    {
                        var opp0 = operatorsPermutations[j][0];
                        var opp1 = operatorsPermutations[j][1];
                        var opp2 = operatorsPermutations[j][2];

                        for (int k = 0; k < 5; k++)
                        {

                            string unformatted = "";
                            switch(k)
                            {
                                case 0:
                                    unformatted = "(({0} {1} {2}) {3} {4}) {5} {6}";
                                    break;
                                case 1:
                                    unformatted = "({0} {1} {2}) {3} ({4} {5} {6})";
                                    break;
                                case 2:
                                    unformatted = "({0} {1} ({2} {3} {4})) {5} {6}";
                                    break;
                                case 3:
                                    unformatted = "{0} {1} (({2} {3} {4}) {5} {6})";
                                    break;
                                case 4:
                                    unformatted = "{0} {1} ({2} {3} ({4} {5} {6}))";
                                    break;
                            }

                            string formatted = string.Format(unformatted, digit0, opp0, digit1, opp1, digit2, opp2, digit3);
                            

                            double result = Evaluate(formatted);
                            if (CommonAlgorithms.IsInteger(result))
                            {
                                integerResults.Add((int)result);
                            }
                        }
                    }
                }

                var distinctResults = integerResults
                    .Distinct()
                    .Where(y => y > 0)
                    .OrderBy(x => x)
                    .ToArray();
                var localHighest = findHighest(distinctResults);
                if (localHighest > globalHighest)
                {
                    globalHighest = localHighest;
                    globalDigits = digits;
                }
            }

            string answer = string.Join("", globalDigits);
            PrintSolution(answer);
            return;
        }
	}
}
