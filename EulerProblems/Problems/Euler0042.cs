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
            Dictionary<char, int> alphabet = new Dictionary<char, int>();
			alphabet.Add('A', 1);
			alphabet.Add('B', 2);
			alphabet.Add('C', 3);
			alphabet.Add('D', 4);
			alphabet.Add('E', 5);
			alphabet.Add('F', 6);
			alphabet.Add('G', 7);
			alphabet.Add('H', 8);
			alphabet.Add('I', 9);
			alphabet.Add('J', 10);
			alphabet.Add('K', 11);
			alphabet.Add('L', 12);
			alphabet.Add('M', 13);
			alphabet.Add('N', 14);
			alphabet.Add('O', 15);
			alphabet.Add('P', 16);
			alphabet.Add('Q', 17);
			alphabet.Add('R', 18);
			alphabet.Add('S', 19);
			alphabet.Add('T', 20);
			alphabet.Add('U', 21);
			alphabet.Add('V', 22);
			alphabet.Add('W', 23);
			alphabet.Add('X', 24);
			alphabet.Add('Y', 25);
			alphabet.Add('Z', 26);
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
					sum += alphabet[c];
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
