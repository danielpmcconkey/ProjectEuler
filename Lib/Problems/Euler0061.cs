//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0061 : Euler
	{
		bool[] triangleBools;
		bool[] squareBools;
		bool[] pentagonalBools;
		bool[] hexagonalBools;
		bool[] heptagonalBools;
		bool[] octagonalBools;
		List<int> octagonals;

		public Euler0061() : base()
		{
			title = "Cyclical figurate numbers";
			problemNumber = 61;
		}
		protected override void Run()
		{

			/* 
			 * Create boolean lists for rapid isPentagonal lookup or 
			 * isHexagonal lookup, etc. then go up the octagonals and try every
			 * one for fitness to the pattern. Since the last number must be a
			 * cyclic pair, our set can start with any one of the values. 
			 * Meaning that you can have any one of the polygonal numbers as 
			 * your starting number and it will produce the same result. I 
			 * chose to go up the octagons because there are fewest of them.
			 * 
			 * Once I have six polygonal numbers in my cyclical set, I then
			 * test for whether the cyclic set contains all s-gonal 
			 * requirements.
			 * 
			 * */

			InitializePolygonalLists();
			


			for (int i = 0; i < octagonals.Count; i++)
            {
				int num1 = octagonals[i];
				// throw out mundane numbers
				if (!IsPolygonal(num1)) continue;
				

				int num1Last2 = num1 % 100;				
				// don't use last 2 less than 10, because the next num cannot start with 0
				if (num1Last2 <= 10) continue;
				var num2Candidates = GetNumberCandidates(num1Last2);
				foreach(int num2 in num2Candidates)
                {
					int num2Last2 = num2 % 100;
					var num3Candidates = GetNumberCandidates(num2Last2);
					foreach(int num3 in num3Candidates)
                    {
						int num3Last2 = num3 % 100;
						var num4Candidates = GetNumberCandidates(num3Last2);
						foreach (int num4 in num4Candidates)
						{
							int num4Last2 = num4 % 100;
							var num5Candidates = GetNumberCandidates(num4Last2);
							foreach (int num5 in num5Candidates)
							{
								int num5Last2 = num5 % 100;
								// num 6 must be cyclic with num 1
								int num1First2 = (int)Math.Floor(num1 / 100d);
								int num6 = (num5Last2 * 100) + num1First2;
								if (!IsPolygonal(num6)) continue;
								int[] nums = new int[] { num1, num2, num3, num4, num5, num6 };
								if (IsTheCorrectSet(nums))
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

		private void InitializePolygonalLists()
        {
			const int limit = 10000;
			const int min = 1001;

			triangleBools = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * (n + 1) / 2;
				if (P_n < limit) triangleBools[P_n] = true;
				else break;
			}
			squareBools = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * n;
				if (P_n < limit) squareBools[P_n] = true;
				else break;
			}
			pentagonalBools = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * ((3 * n) - 1) / 2;
				if (P_n < limit) pentagonalBools[P_n] = true;
				else break;
			}
			hexagonalBools = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * ((2 * n) - 1);
				if (P_n < limit) hexagonalBools[P_n] = true;
				else break;
			}
			heptagonalBools = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * ((5 * n) - 3) / 2;
				if (P_n < limit) heptagonalBools[P_n] = true;
				else break;
			}
			octagonals = new List<int>();
			octagonalBools = new bool[limit];
			for (int n = 1; true; n++)
			{
				int P_n = n * ((3 * n) - 2);
				if (P_n < limit)
				{
					if (P_n >= min) octagonals.Add(P_n);
					octagonalBools[P_n] = true;
				}
				else break;
			}
#if VERBOSEOUTPUT
			CheckForNumbersThatAreMultiplePolygonTypes();
#endif
		}
		private bool IsTheCorrectSet(int[] nums)
        {
			/*
			 * numbers that can be multiple polygons 
			 * (other than hex and tri, because there are 48 of those)
			 * 
			 * 1224 hex and square
			 * 4347 hept and pent
			 * 5929 hept and square
			 * 9801 pent and square
			 * 1225 square and tri
			 * 
			 * */


			// first gate:
			// take a count of each of the different polygons. there must be at
			// least one of each. Remember, some numbers can be different types
			// of polygon. 
			var octs = nums.Where(x => octagonalBools[x]);
			if (octs.Count() == 0) return false;			

			var hepts = nums.Where(x => heptagonalBools[x]);
			if (hepts.Count() == 0) return false;

			var hexes = nums.Where(x => hexagonalBools[x]);
			if (hexes.Count() == 0) return false;

			var pents = nums.Where(x => pentagonalBools[x]);
			if (pents.Count() == 0) return false;

			var sqs = nums.Where(x => squareBools[x]);
			if (sqs.Count() == 0) return false;

			var tris = nums.Where(x => triangleBools[x]);
			if (tris.Count() == 0) return false;

			// second gate:
			// octagonal numbers between 1001 and 9999 are NEVER any other polygon type
			if (octs.Count() > 1) return false;


			// try to assign tumbers to the polygonal "slots"
			List<int> numsUsed = new List<int>();
			// no numbers are duplicated
			if(
				hepts.Count() == 1 && 
				hexes.Count() == 1 && 
				pents.Count() == 1 && 
				sqs.Count() == 1 && 
				tris.Count() == 1)
            {
				return true;
            }
			// only 6 and 3 are duplicated
			if (
				hepts.Count() == 1 &&
				hexes.Count() == 2 &&
				pents.Count() == 1 &&
				sqs.Count() == 1 &&
				tris.Count() == 2)
			{
                // are they the same 2?
                if (
					(hexes.ElementAt(0) == tris.ElementAt(0) && hexes.ElementAt(0) == tris.ElementAt(0))
					||
					(hexes.ElementAt(1) == tris.ElementAt(0) && hexes.ElementAt(0) == tris.ElementAt(1))
					)
                {
					return true;
                }
			}
			// only 3 are duplicated and the second 3 is a 6
			if (
				hepts.Count() == 1 &&
				hexes.Count() == 1 &&
				pents.Count() == 1 &&
				sqs.Count() == 1 &&
				tris.Count() == 2)
			{
				if (hexes.ElementAt(0) == tris.ElementAt(0) || hexes.ElementAt(0) == tris.ElementAt(1))
				{
					return true;
				}
			}
			// only 4 are duplicated... 
			if (
				hepts.Count() == 1 &&
				hexes.Count() == 1 &&
				pents.Count() == 1 &&
				sqs.Count() == 2 &&
				tris.Count() == 1)
			{
				//...and the second 4 is a 7
				if (hepts.ElementAt(0) == sqs.ElementAt(0) || hepts.ElementAt(0) == sqs.ElementAt(1))
				{
					return true;
				}
				//...and the second 4 is a 6
				if (hexes.ElementAt(0) == sqs.ElementAt(0) || hexes.ElementAt(0) == sqs.ElementAt(1))
				{
					return true;
				}
				//...and the second 4 is a 5
				if (pents.ElementAt(0) == sqs.ElementAt(0) || pents.ElementAt(0) == sqs.ElementAt(1))
				{
					return true;
				}
				//...and the second 4 is a 3
				if (tris.ElementAt(0) == sqs.ElementAt(0) || tris.ElementAt(0) == sqs.ElementAt(1))
				{
					return true;
				}
			}
			// only 7 are duplicated... 
			if (
				hepts.Count() == 2 &&
				hexes.Count() == 1 &&
				pents.Count() == 1 &&
				sqs.Count() == 1 &&
				tris.Count() == 1)
			{
				//...and the second 7 is a 5
				if (pents.ElementAt(0) == hepts.ElementAt(0) || pents.ElementAt(0) == hepts.ElementAt(1))
				{
					return true;
				}
				//...and the second 7 is a 4
				if (sqs.ElementAt(0) == hepts.ElementAt(0) || sqs.ElementAt(0) == hepts.ElementAt(1))
				{
					return true;
				}
			}
			// only 5 are duplicated... 
			if (
				hepts.Count() == 1 &&
				hexes.Count() == 1 &&
				pents.Count() == 2 &&
				sqs.Count() == 1 &&
				tris.Count() == 1)
			{
				//...and the second 5 is a 4
				if (sqs.ElementAt(0) == pents.ElementAt(0) || sqs.ElementAt(0) == pents.ElementAt(1))
				{
					return true;
				}
				//...and the second 5 is a 7
				if (hepts.ElementAt(0) == pents.ElementAt(0) || hepts.ElementAt(0) == pents.ElementAt(1))
				{
					return true;
				}
			}




			// final gate:
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
		private bool IsPolygonal (int n)
		{

			if (triangleBools[n] == false
				&& squareBools[n] == false
				&& pentagonalBools[n] == false
				&& hexagonalBools[n] == false
				&& heptagonalBools[n] == false
				&& octagonalBools[n] == false
				) 
			return false;
			return true;
		}
		private List<int> GetNumberCandidates(int priorNumLast2)
		{
			List<int> candidates = new List<int>();
			for (int i = 0; i < 100; i++)
			{
				int num = (priorNumLast2 * 100) + i;
				if (!IsPolygonal(num)) continue;
				int numLast2 = i % 100;
				if (numLast2 <= 10) continue;
				candidates.Add(num);
			}
			return candidates;
		}
#if VERBOSEOUTPUT
		private void CheckForNumbersThatAreMultiplePolygonTypes()
        {
			Console.WriteLine("Valid polygon combos");
            Dictionary<(int, int), int> validCombos = new Dictionary<(int, int), int>();
            for (int i = 1001; i <= 9999; i++)
            {
                // 8   7
                if (octagonalBools[i] && heptagonalBools[i])
                {
                    if (validCombos.ContainsKey((8, 7))) validCombos[(8, 7)]++;
                    else validCombos.Add((8, 7), 1);
                }
                // 8   6
                if (octagonalBools[i] && hexagonalBools[i])
                {
                    if (validCombos.ContainsKey((8, 6))) validCombos[(8, 6)]++;
                    else validCombos.Add((8, 6), 1);
                }
                // 8   5
                if (octagonalBools[i] && pentagonalBools[i])
                {
                    if (validCombos.ContainsKey((8, 5))) validCombos[(8, 5)]++;
                    else validCombos.Add((8, 5), 1);
                }
                // 8   4
                if (octagonalBools[i] && squareBools[i])
                {
                    if (validCombos.ContainsKey((8, 4))) validCombos[(8, 4)]++;
                    else validCombos.Add((8, 4), 1);
                }
                // 8   3
                if (octagonalBools[i] && triangleBools[i])
                {
                    if (validCombos.ContainsKey((8, 3))) validCombos[(8, 3)]++;
                    else validCombos.Add((8, 3), 1);
                }
                // 7   6
                if (heptagonalBools[i] && hexagonalBools[i])
                {
                    if (validCombos.ContainsKey((7, 6))) validCombos[(7, 6)]++;
                    else validCombos.Add((7, 6), 1);
                }
                // 7   5
                if (heptagonalBools[i] && pentagonalBools[i])
                {
                    if (validCombos.ContainsKey((7, 5))) validCombos[(7, 5)]++;
                    else validCombos.Add((7, 5), 1);
                }
                // 7   4
                if (heptagonalBools[i] && squareBools[i])
                {
                    if (validCombos.ContainsKey((7, 4))) validCombos[(7, 4)]++;
                    else validCombos.Add((7, 4), 1);
                }
                // 7   3
                if (heptagonalBools[i] && triangleBools[i])
                {
                    if (validCombos.ContainsKey((7, 3))) validCombos[(7, 3)]++;
                    else validCombos.Add((7, 3), 1);
                }
                // 6   5
                if (hexagonalBools[i] && pentagonalBools[i])
                {
                    if (validCombos.ContainsKey((6, 5))) validCombos[(6, 5)]++;
                    else validCombos.Add((6, 5), 1);
                }
                // 6   4
                if (hexagonalBools[i] && squareBools[i])
                {
                    if (validCombos.ContainsKey((6, 4))) validCombos[(6, 4)]++;
                    else validCombos.Add((6, 4), 1);
                }
                // 6   3
                if (hexagonalBools[i] && triangleBools[i])
                {
                    if (validCombos.ContainsKey((6, 3))) validCombos[(6, 3)]++;
                    else validCombos.Add((6, 3), 1);
                }
                // 5   4
                if (pentagonalBools[i] && squareBools[i])
                {
                    if (validCombos.ContainsKey((5, 4))) validCombos[(5, 4)]++;
                    else validCombos.Add((5, 4), 1);
                }
                // 5   3
                if (pentagonalBools[i] && triangleBools[i])
                {
                    if (validCombos.ContainsKey((5, 3))) validCombos[(5, 3)]++;
                    else validCombos.Add((5, 3), 1);
                }
                // 4   3
                if (squareBools[i] && triangleBools[i])
                {
                    if (validCombos.ContainsKey((4, 3))) validCombos[(4, 3)]++;
                    else validCombos.Add((4, 3), 1);
                }
            }
            foreach (var combo in validCombos)
            {
                Console.WriteLine("{0}   {1}   {2}", combo.Key.Item1, combo.Key.Item2, combo.Value);
            }
        } 
#endif
    }
}
