//#define VERBOSEOUTPUT
using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Lib.Problems
{
    public class Euler0022 : Euler
    {
        const string filePath = @"E:\ProjectEuler\ExternalFiles\p022_names.txt";
        public Euler0022() : base()
        {
            title = "Names scores";
            problemNumber = 22;
            
        }
        protected override void Run()
        {
            // read the names
            string fileContents = File.ReadAllText(filePath);
            fileContents = fileContents.Replace("\"", "");
            string[] names = fileContents.Split(',');

            // now sort. List is large, so sorting normally takes a while
            // use a faster algorithm
            string[] namesSorted = CommonAlgorithms.AlphabeticalSort(names);


            // now that they're sorted, let's total the names scores
            long answer = 0;
            for (int i = 0; i < namesSorted.Length; i++)
            {
                var thisName = namesSorted[i];
                var thisScore = GetNameScore(thisName);
#if VERBOSEOUTPUT
                if (thisName == "COLIN")
                {
                    Console.WriteLine(String.Format("COLIN is in position {0} in the list", i + 1));
                    Console.WriteLine(String.Format("The score for COLIN is {0}", (i + 1) * thisScore));
                }
#endif
                answer += thisScore * (i + 1);
            }


            PrintSolution(answer.ToString());
            return;
        }
        private long GetNameScore(string name)
        {
            var chars = name.ToCharArray();
            long score = 0;
            foreach (char c in chars)
            {
                int x = (int)c - 64;
                score += x;
            }
            return score;
        }
    }
}
