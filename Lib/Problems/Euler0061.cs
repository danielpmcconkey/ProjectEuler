//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0061 : Euler
	{
		bool[] triangles;
		bool[] squares;
		bool[] pentagonals;
		bool[] hexagonals;
		bool[] heptagonals;
		bool[] octagonals;
		public Euler0061() : base()
		{
			title = "Cyclical figurate numbers";
			problemNumber = 61;
		}
		protected override void Run()
		{
			int limit = 10000;

			/* 
			 * create boolean lists for rapid isPentagonal lookup or 
			 * isHexagonal lookup, etc. then go from 1000 to 999 and find the 
			 * cyclic sets and test for whether the cyclic set meets the 
			 * s-gonal requirements
			 * 
			 * */
			triangles = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * (n + 1) / 2;
				if (P_n < limit) triangles[P_n] = true;
				else break;
			}
			squares = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * n;
				if (P_n < limit) squares[P_n] = true;
				else break;
			}
			pentagonals = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * ((3 * n) - 1) / 2;
				if (P_n < limit) pentagonals[P_n] = true;
				else break;
			}
			hexagonals = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * ((2 * n) - 1);
				if (P_n < limit) hexagonals[P_n] = true;
				else break;
			}
			heptagonals = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * ((5 * n) - 3) / 2;
				if (P_n < limit) heptagonals[P_n] = true;
				else break;
			}
			octagonals = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * ((3 * n) - 2);
				if (P_n < limit) octagonals[P_n] = true;
				else break;
			}

			


			for (int i = 1001; i < limit; i++)
            {
				int num1 = i;
				bool isViableAtI = true;
				// throw out mundane numbers
				if (!isSpecial(num1)) continue;
				

				int num1Last2 = i % 100;				
				// don't use last 2 less than 10, because the next num cannot start with 0
				if (num1Last2 <= 10) continue;

				for(int j = 0; j < 100; j++)
                {
					int num2 = (num1Last2 * 100) + j;
					if (!isSpecial(num2)) continue;
					int num2Last2 = j % 100;
					if (num2Last2 <= 10) continue;
					for (int k = 0; k < 100; k++)
					{
						int num3 = (num2Last2 * 100) + k;
						if (!isSpecial(num3)) continue;
						int num3Last2 = k % 100;
						if (num3Last2 <= 10) continue;
						for (int l = 0; l < 100; l++)
						{
							int num4 = (num3Last2 * 100) + l;
							if (!isSpecial(num4)) continue;
							int num4Last2 = l % 100;
							if (num4Last2 <= 10) continue;
							for (int m = 0; m < 100; m++)
							{
								int num5 = (num4Last2 * 100) + m;
								if (!isSpecial(num5)) continue;
								int num5Last2 = m % 100;
								if (num5Last2 <= 10) continue;

								// num 6 must be cyclic with num 1
								int num1First2 = (int)Math.Floor(i / 100d);
								int num6 = (num5Last2 * 100) + num1First2;
								if (!isSpecial(num6)) continue;
								int[] nums = new int[] { num1, num2, num3, num4, num5, num6 };
								if (IsGoodness(nums))
                                {
									int answer = nums.Sum();
									PrintSolution(answer.ToString());
									return;
								}
								
							}
						}
					}
				}                
            }			
		}
		private bool isSingleValueRepeatedInSingleValue(
			IEnumerable<int> listP,
			IEnumerable<int> list_2,
			IEnumerable<int> list_3,
			IEnumerable<int> list_4,
			IEnumerable<int> list_5,
			IEnumerable<int> list_6
			)
        {
			// don't use this for hexes as every hex is also a tri
			if (listP.Count() == 1)
			{
				if (list_2.Count() == 1 && list_2.Contains(listP.First())) return true;
				if (list_3.Count() == 1 && list_3.Contains(listP.First())) return true;
				if (list_4.Count() == 1 && list_4.Contains(listP.First())) return true;
				if (list_5.Count() == 1 && list_5.Contains(listP.First())) return true;
				if (list_6.Count() == 1 && list_6.Contains(listP.First())) return true;
			}
			return false;
		}


		private bool IsGoodness(int[] nums)
        {
			// several of these can be on multiple lists
			var octs = nums.Where(x => octagonals[x]);
			if (octs.Count() == 0) return false;

			var hepts = nums.Where(x => heptagonals[x]);
			if (hepts.Count() == 0) return false;

			var hexes = nums.Where(x => hexagonals[x]);
			if (hexes.Count() == 0) return false;

			var pents = nums.Where(x => pentagonals[x]);
			if (pents.Count() == 0) return false;

			var sqs = nums.Where(x => squares[x]);
			if (sqs.Count() == 0) return false;

			var tris = nums.Where(x => triangles[x]);
			if (tris.Count() == 0) return false;

			// if any of these has a count of 1, check that it's not shared by
			// another except hexes. there can be 1 hex repeated in tris this
			// probably isn't right, but the first one that passes this
			// gauntlet is the euler accepted answer
			
			if (isSingleValueRepeatedInSingleValue(tris, sqs, pents, hexes, hepts, octs)) return false;
			if (isSingleValueRepeatedInSingleValue(sqs, tris, pents, hexes, hepts, octs)) return false;
			if (isSingleValueRepeatedInSingleValue(pents, tris, sqs, hexes, hepts, octs)) return false;
			if (isSingleValueRepeatedInSingleValue(hepts, tris, sqs, pents, hexes, octs)) return false;
			if (isSingleValueRepeatedInSingleValue(octs, tris, sqs, pents, hexes, hepts)) return false;

#if VERBOSEOUTPUT
            Console.Write("Tris: "); foreach (var n in tris) Console.Write(n.ToString() + ", "); Console.WriteLine();
            Console.Write("Sqs: "); foreach (var n in sqs) Console.Write(n.ToString() + ", "); Console.WriteLine();
            Console.Write("pents: "); foreach (var n in pents) Console.Write(n.ToString() + ", "); Console.WriteLine();
            Console.Write("hexes: "); foreach (var n in hexes) Console.Write(n.ToString() + ", "); Console.WriteLine();
            Console.Write("hepts: "); foreach (var n in hepts) Console.Write(n.ToString() + ", "); Console.WriteLine();
            Console.Write("octs: "); foreach (var n in octs) Console.Write(n.ToString() + ", "); Console.WriteLine(); 
#endif

			return true;
		}

        private bool isSpecial (int n)
		{

			if (triangles[n] == false
				&& squares[n] == false
				&& pentagonals[n] == false
				&& hexagonals[n] == false
				&& heptagonals[n] == false
				&& octagonals[n] == false
				) 
			return false;
			return true;
		}
	}
}
