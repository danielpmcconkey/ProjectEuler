using EulerProblems.Lib;

namespace EulerProblems.Problems
{
	internal class Euler0042 : Euler
	{
		const string filePath = @"E:\ProjectEuler\ExternalFiles\p042_words.txt";
		public Euler0042() : base()
		{
			title = "Coded triangle numbers";
			problemNumber = 42;
			PrintTitle();
		}
		public override void Run()
		{
			// read the names
			string fileContents = File.ReadAllText(filePath);
			fileContents = fileContents.Replace("\"", "");
			string[] words = fileContents.Split(',');
			// what's the longest word in the file?
			string longestWord = words.OrderByDescending(x => x.Length).First();
			int longestLength = longestWord.Length;
			// now find all the triangle numbers up to longestLength * 26 
			int biggestPossibleTriangle = longestLength * 26;
			List<int> triangles = new List<int>();
			int biggestTriangleSoFar = 0;
			int n = 1;
			while(biggestTriangleSoFar <= biggestPossibleTriangle)
            {
				int t_n = (int)Math.Round(0.5d * n * (n + 1),0);
				triangles.Add(t_n);
				biggestTriangleSoFar = t_n;
				n++;
            }

			int answer = 0;
			
			foreach(var word in words)
            {
                char[] chars = word.ToCharArray();
				int sum = 0;
				foreach(char c in chars)
                {
					sum += CommonAlgorithms.GetIndexOfLetterInAlphabet(c) + 1; // the +1 is due to the zero-indexing of teh function
                }
				if (triangles.Contains(sum))
				{
					answer++;
				}
            }

			PrintSolution(answer.ToString());
			return;

		}
	}
}
