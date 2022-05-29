using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Lib.Problems
{
    public class Euler0067 : Euler
    {
        // note that the string was formatted to align center on the web page
        const string filePath = @"E:\ProjectEuler\ExternalFiles\p067_triangle.txt";
        public Euler0067() : base()
        {
            title = "Maximum path sum II";
            problemNumber = 67;
            
        }
        protected override void Run()
        {
            // note, this is an identical implementation to problem 18
            // any change you make here, make there as well

            List<List<int>> intRows = new List<List<int>>();
            // read the text file and put the values into intRows
            foreach (string row in File.ReadLines(filePath))
            {
                string rowTrimmed = row.Trim();
                if (rowTrimmed.Length > 1)
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
    }
}
