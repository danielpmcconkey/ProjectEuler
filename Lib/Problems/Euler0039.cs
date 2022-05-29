namespace EulerProblems.Lib.Problems
{
	public class Euler0039 : Euler
	{
		public Euler0039() : base()
		{
			title = "Integer right triangles";
			problemNumber = 39;
			
		}
		protected override void Run()
		{
			int maxPerimeter = 1000;
			Dictionary<int, List<(int a, int b, int c)>> solutions = new Dictionary<int, List<(int a, int b, int c)>>();
			int maxC = 640; // may be a bad guess. but { 384, 512, 640 } sums to 1536
			int maxA = maxC; //  (int)Math.Floor(maxC * 0.5); // another ill-advised guess
			int maxB = maxA;

			for(int a = 1; a <= maxA; a++)
            {
				for (int b = a; b <= maxB; b++)	// setting b = a keeps a, b, and c in order and prevents duplicates
				{
					int aSquared = a * a;
					int bSquared = b * b;
					int cSquared = aSquared + bSquared;
					// is cSquared a perfect square?
					if(CommonAlgorithms.IsPerfectSquare(cSquared))
                    {
						// we have a pythagorean triple of integers
						int c = (int)Math.Sqrt(cSquared);
						int p = a + b + c;
						if (p <= maxPerimeter)
						{
							if (!solutions.ContainsKey(p))
							{
								solutions.Add(p, new List<(int a, int b, int c)>());
							}
							solutions[p].Add((a, b, c));
						}
                    }
				}
			}
			var pWithMaxSolutions = solutions.OrderByDescending(x => x.Value.Count()).FirstOrDefault();
			int answer = pWithMaxSolutions.Key;
			PrintSolution(answer.ToString());
			return;
		}
		

	}
}
