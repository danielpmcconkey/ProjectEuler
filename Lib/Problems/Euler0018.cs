using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Lib.Problems
{
    public class Euler0018 : Euler
    {
        // note that the string was formatted to align center on the web page
        const string input = @"
            75
            95 64
            17 47 82
            18 35 87 10
            20 04 82 47 65
            19 01 23 75 03 34
            88 02 77 73 07 63 67
            99 65 04 28 06 16 70 92
            41 41 26 56 83 40 80 70 33
            41 48 72 33 47 32 37 16 94 29
            53 71 44 65 25 43 91 52 97 51 14
            70 11 33 28 77 73 17 78 39 68 17 57
            91 71 52 38 17 14 91 43 58 50 27 29 48
            63 66 04 68 89 53 67 30 73 16 69 87 40 31
            04 62 98 27 23 09 70 98 73 93 38 53 60 04 23
            ";  
        public Euler0018() : base()
        {
            title = "Maximum path sum I";
            problemNumber = 18;
            
        }
        protected override void Run()
        {
            // convert the string to an array of arrays
            // okay, I used lists, but only because I'm lazy
            string[] stringRows = input.Split(Environment.NewLine);
            List<List<int>> intRows = new List<List<int>>();
            foreach (var row in stringRows)
            {
                string rowTrimmed = row.Trim();
                if (rowTrimmed.Length > 1)  // because of how I pasted the source string, there's a blank line at the beginning
                {
                    string[] intsAsStrings = rowTrimmed.Split(' ');
                    List<int> rowOfInts = new List<int>();
                    foreach (var intAsString in intsAsStrings)
                    {
                        rowOfInts.Add(Int16.Parse(intAsString));
                    }
                    intRows.Add(rowOfInts);
                }
            }
            /*
             * start from the second to last row and iterate
             * through each number in it. For each, update
             * the value by adding to it the larger of the
             * two values below it.
             * 
             * Example:
             * 
             *             73
             *          23    44
             *          
             * gets updated to:
             * 
             *             117 (73 + 44 = 117)
             *          23    44
             * 
             * Do this over and over again and, by the time
             * you reach the top, you'll have the highest
             * possible sum. Yippee
             * 
             * */


            for (int i = intRows.Count - 2; i >= 0; i--)
            {
                for (int j = 0; j < intRows[i].Count; j++)
                {
                    // find the bigger next row value
                    int valueToAdd = Math.Max(intRows[i + 1][j], intRows[i + 1][j + 1]);
                    intRows[i][j] += valueToAdd;
                }
            }                    

            PrintSolution(intRows[0][0].ToString());
            return;
        }
        public void Run_bruteForce()
        {
            // convert the string to an array of arrays
            string[] stringRows = input.Split(Environment.NewLine); 
            List<List<short>> intRows = new List<List<short>>();
            foreach(var row in stringRows)
            {
                string rowTrimmed = row.Trim();
                if (rowTrimmed.Length > 1)  // because of how I pasted the source string, there's a blank line at the beginning
                {
                    string[] intsAsStrings = rowTrimmed.Split(' ');
                    List<short> rowOfInts = new List<short>();
                    foreach (var intAsString in intsAsStrings)
                    {
                        rowOfInts.Add(Int16.Parse(intAsString));
                    }
                    intRows.Add(rowOfInts);
                }
            }
            /*
             * now you have rows of integer lists. for adjacency, when 
             * moving down, a value in teh row below is adjacent when
             * it is in the same column or column + 1
             * 
             * this means you have two choices for each row, which means
             * you have 2 ^ (number of rows - 1) possibilities. there's 
             * probably a great mathematical way to do this, but I can 
             * probably brute force it easily enough.
             * 
             * With 15 rows, that's only 16k iterations. that shouldn't
             * take the PC too long.
             * 
             * Just iterate from 0 to num possibilites, and conver each
             * i to a 14-digit binary string. Then go through that binary
             * string and, for every 0, go "left" and for every 1, go
             * "right"
             * 
             * */
            int numSteps = intRows.Count - 1;
            int totalPossibleRouts = (int)Math.Pow(2, numSteps);
            int maximumTotal = 0;

            for(int i = 0; i < totalPossibleRouts; i++)
            {
                string binary = Convert.ToString(i, 2);
                binary = binary.PadLeft(numSteps, '0');
                char[] binaryAsCharArry = binary.ToCharArray();

                // now go through this "path" and sum
                int thisSum = intRows[0][0];
                int currentColumn = 0;
                for(int j = 0; j < binaryAsCharArry.Length; j++) 
                {
                    char c = binaryAsCharArry[j];
                    if (c == '0')
                    {
                        short thisVal = intRows[j + 1][currentColumn]; // next row, same column
                        thisSum += thisVal;
                    }
                    if (c == '1')
                    {
                        short thisVal = intRows[j + 1][currentColumn + 1]; // next row, one column to the right
                        thisSum += thisVal;
                        currentColumn++;
                    }
                }
                if(thisSum > maximumTotal) maximumTotal = thisSum;
            }

            PrintSolution(maximumTotal.ToString());
            return;
        }
        
    }
}
