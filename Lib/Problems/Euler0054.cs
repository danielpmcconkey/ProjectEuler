//#define VERBOSEOUTPUT
using System.Text;

namespace EulerProblems.Lib.Problems
{
	internal enum HandRank { 
		HIGH_CARD, ONE_PAIR, TWO_PAIRS, THREE_OF_A_KIND, 
		STRAIGHT, FLUSH, FULL_HOUSE, FOUR_OF_A_KIND,
		STRAIGHT_FLUSH
	}
	internal struct Card
    {
		internal string suit;
		internal int rank;
    }
	internal struct Hand
    {
		internal Card[] cards;
		internal HandRank rank;
		internal List<int> tieBreakers; // ordered by descending
    }
	public class Euler0054 : Euler
	{
		private static Dictionary<string, int> CardRank;
		public Euler0054() : base()
		{
			title = "Poker hands";
			problemNumber = 54;
		}
		protected override void Run()
		{
			CardRank = new Dictionary<string, int>()
			{
				{"2", 2},{"3", 3},{"4", 4},{"5", 5},{"6", 6},{"7", 7},{"8", 8},
				{"9", 9 },{"T", 10},{"J", 11},{"Q", 12},{ "K", 13},{ "A", 14 }
			};

		
			List<string> lines = ReadFileIntoLines();

			int p1TotalScore = 0;
			int p2TotalScore = 0;

			foreach (var line in lines)
			{
#if VERBOSEOUTPUT
                StringBuilder sb = new StringBuilder();
                sb.Append(line.PadRight(35)); 
#endif
				var hands = DealHands(line);
				(HandRank rank, List<int> tieBreakers) p1Score = RankHand(hands.p1);
				(HandRank rank, List<int> tieBreakers) p2Score = RankHand(hands.p2);

#if VERBOSEOUTPUT
                sb.Append(string.Format("p1 hand: {0}; p2 hand: {1}", p1Score.rank, p2Score.rank)); 
#endif

				if ((int)(p1Score.rank) > (int)(p2Score.rank))
				{
					p1TotalScore++;
#if VERBOSEOUTPUT
                    sb.Append("   P1 wins   "); 
#endif
				}
				else if ((int)(p1Score.rank) < (int)(p2Score.rank))
				{
					p2TotalScore++;
#if VERBOSEOUTPUT
                    sb.Append("   P2 wins   "); 
#endif
				}
				else if ((int)(p1Score.rank) == (int)(p2Score.rank))
				{
					for (int i = 0; i < p1Score.tieBreakers.Count; i++)
					{
						if (p1Score.tieBreakers[i] > p2Score.tieBreakers[i])
						{
							p1TotalScore++;
#if VERBOSEOUTPUT
							sb.Append("   P1 wins   "); 
#endif
							break;
						}
						else if (p1Score.tieBreakers[i] < p2Score.tieBreakers[i])
						{
							p2TotalScore++;
#if VERBOSEOUTPUT
							sb.Append("   P2 wins   "); 
#endif

							break;
						}
					}
				}
#if VERBOSEOUTPUT
                Console.WriteLine(sb.ToString()); 
#endif
			}
#if VERBOSEOUTPUT
            Console.WriteLine(string.Format("Player 1: {0}, Player 2: {1}", p1TotalScore, p2TotalScore)); 
#endif
			PrintSolution(p1TotalScore.ToString());
			return;
		}

        private List<string> ReadFileIntoLines()
        {
			const string filePath = @"E:\ProjectEuler\ExternalFiles\p054_poker.txt";
			return File.ReadLines(filePath).ToList();
		}

        private (Hand p1, Hand p2) DealHands(string line)
        {
			string[] cardsAsStrings = line.Split(' ');
			Hand p1 = new Hand();
			p1.cards = new Card[5];
			Hand p2 = new Hand();
			p2.cards = new Card[5];
			for (int i = 0; i < 10; i++)
            {
				string digits = cardsAsStrings[i];
				Card c	= new Card();
				if(digits.Length != 2)
                {
					throw new ArgumentException();
                }
				c.suit = digits.Substring(1, 1);
				c.rank = CardRank[digits.Substring(0, 1)];
				
				if (i < 5) p1.cards[i] = c;
				else p2.cards[i - 5] = c;
			}
			return (p1, p2);
        }
		private (HandRank rank, List<int> tieBreakers) RankHand(Hand h)
        {
			var suitGroups = from c in h.cards
							 group c by c.suit into g
							 select new { suit = g.Key };
			var rankGroups = from c in h.cards
							 group c by c.rank into g
							 select new { rank = g.Key, count = g.Count() };
			int[] orderedRanks = h.cards
				.OrderByDescending(x => x.rank)
				.Select(y => y.rank)
				.ToArray();

			List<int> tieBreakers = new List<int>();


			// check for straight, flush, and straightflush
			bool isStraight = (rankGroups.Count() == 5 && orderedRanks[0] - orderedRanks[4] == 4) 
				? true : false;
			bool isFlush = (suitGroups.Count() == 1) ? true : false;
			if (isStraight || isFlush)
			{
				if (isStraight && isFlush)
					return (HandRank.STRAIGHT_FLUSH, orderedRanks.ToList());
				if (isFlush) return (HandRank.FLUSH, orderedRanks.ToList());
				if (isStraight) return (HandRank.STRAIGHT, orderedRanks.ToList());
			}

			// check for 4 of a kind and full house at the same time. both have 2 groups of ranks
			if(rankGroups.Count() == 2)
            {
				// we know it's one of these
				var rankGroupsOrdered = rankGroups.OrderByDescending(x => x.count).ToList();
				if (rankGroupsOrdered.First().count == 4)
				{
					tieBreakers.Add(rankGroupsOrdered[0].rank);
                    tieBreakers.Add(rankGroupsOrdered[1].rank);
					return (HandRank.FOUR_OF_A_KIND, tieBreakers);
				}
				// just a full house
				tieBreakers.Add(rankGroupsOrdered[0].rank);
				tieBreakers.Add(rankGroupsOrdered[1].rank);
				return (HandRank.FULL_HOUSE, tieBreakers);
			}

			// check for 3 of a kind and 2 pairs at the same time. both have 3 groups of ranks
			if (rankGroups.Count() == 3)
			{
				// we know it's one of these
				var rankGroupsOrdered = rankGroups.OrderByDescending(x => x.count)
					.ThenByDescending(y => y.rank);
				if (rankGroupsOrdered.First().count == 3)
				{
					tieBreakers.Add(rankGroupsOrdered.First().rank);
					tieBreakers.AddRange(orderedRanks[3..5]);
					return (HandRank.THREE_OF_A_KIND, tieBreakers);
				}
				// just 2 pairs
				tieBreakers.Add(rankGroupsOrdered.First().rank);
                tieBreakers.Add(rankGroupsOrdered.ToList()[1].rank);
				tieBreakers.Add(orderedRanks[4]);
				return (HandRank.TWO_PAIRS, tieBreakers);
			}
			// do we have a pair?
			if (rankGroups.Count() == 4)
			{
				var rankGroupsOrdered = rankGroups.OrderByDescending(x => x.count);
				tieBreakers.Add(rankGroupsOrdered.First().rank);
				tieBreakers.AddRange(
					rankGroupsOrdered
					.Where(x => x.count < 2)
					.OrderByDescending(y => y.rank).Select(z => z.rank)
					);
				return (HandRank.ONE_PAIR, tieBreakers);
			}

			return (HandRank.HIGH_CARD, orderedRanks.ToList());
		}
	}
}
