//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
    /*
     * Total brute force approach here. I only had one idea and that was to 
     * completely create a Monopoly sim and run it over and over again. There 
     * are smarter ways, but this works fast enough and I didn't have any 
     * mental anxiety over finding the answer. This was a straight up easy 
     * application with given requirements. (Though he says nothing about the
     * rule to go again when you roll doubles.)
     * 
     * One thing I find interesting is that, if I restart the game (ie 
     * reshuffle the cards and start over at "Go") every 1,000 turns, instead 
     * of just going around the track for a million turns, I get more accurate
     * results. I presume this is because one "weird" shuffle results in odd 
     * perturbations. 
     * 
     * Oh well. Problem solved.
     * 
     * */
    public enum BoardPositions
    {
        GO = 0,
        A1 = 1,
        CC1 = 2,
        A2 = 3,
        T1 = 4,
        R1 = 5,
        B1 = 6,
        CH1 = 7,
        B2 = 8,
        B3 = 9,
        JAIL = 10,
        C1 = 11,
        U1 = 12,
        C2 = 13,
        C3 = 14,
        R2 = 15,
        D1 = 16,
        CC2 = 17,
        D2 = 18,
        D3 = 19,
        FP = 20,
        E1 = 21,
        CH2 = 22,
        E2 = 23,
        E3 = 24,
        R3 = 25,
        F1 = 26,
        F2 = 27,
        U2 = 28,
        F3 = 29,
        G2J = 30,
        G1 = 31,
        G2 = 32,
        CC3 = 33,
        G3 = 34,
        R4 = 35,
        CH3 = 36,
        H1 = 37,
        T2 = 38,
        H2 = 39,
    }
    public class MonopolySquare
    {
        public int position { get; set; }
        public string name { get; set; }
        public int numTurnEnds { get; set; }
        public Func<int, int> endTurnFunc;

        public MonopolySquare(int position, string name, Func<int, int> endTurnFunc)
        {
            this.position = position;
            this.name = name;
            this.endTurnFunc = endTurnFunc;
            this.numTurnEnds = 0;
        }
        public override string ToString()
        {
            return name;
        }
    }
    public class MonopolyCard
    {
        public Func<int, int> cardFunc;
        public string name;
        public MonopolyCard(Func<int, int> cardFunc, string name)
        {
            this.cardFunc = cardFunc;
            this.name = name;
        }
        public override string ToString()
        {
            return name;
        }
    }
    public class MonopolyCardDeck
    {
        public string name { get; set; }
        public List<MonopolyCard> cards { get; set; }
        public MonopolyCardDeck(string name)
        {
            this.name = name;
            cards = new List<MonopolyCard>();
        }
        public void AddCard(Func<int, int> cardFunc, string name = "default")
        {
            cards.Add(new MonopolyCard(cardFunc, name));
        }
        public void Shuffle()
        {
            List<(int card, int newPos)> newOrder = new List<(int card, int newPos)>();
            List<MonopolyCard> newCards = new List<MonopolyCard>();
            for(int i = 0; i < cards.Count; i++)
            {
                newOrder.Add((i, Utilities.RNG.getRandomInt(0, 1000)));
            }
            var sorted = newOrder.OrderBy(x => x.newPos);
            foreach(var s in sorted)
            {
                newCards.Add(cards[s.card]);
            }
            cards = newCards;
        }
        public int DrawCard(int currentPos)
        {
#if VERBOSEOUTPUT
            Console.WriteLine("Drawing card from {0} deck.", name);
#endif
            var card = cards[0];
            cards.RemoveAt(0);
            cards.Add(card);
            return card.cardFunc(currentPos);
        }
    }
    public class Board
    {
        public List<MonopolySquare> squares { get; set; }
        public Board()
        {
            squares = new List<MonopolySquare>();
        }
        public void AddSquare(int position, string name, Func<int, int> endTurnFunc)
        {
            squares.Add(new MonopolySquare(position, name, endTurnFunc));
        }
    }
	public class Euler0084 : Euler
	{
        private Board board;
        private MonopolyCardDeck chanceDeck;
        private MonopolyCardDeck communityChestDeck;

        public Euler0084() : base()
		{
			title = "Monopoly odds";
			problemNumber = 84;
		}
		protected override void Run()
		{
            #region Game Actions
            Func<int, MonopolySquare> getSquareAtPosition = (pos) =>
            {
                return board.squares.Where(x => x.position == pos).First();
            };
            Func<int, int> MoveTo = (newPos) =>
            {
                var newSquare = getSquareAtPosition(newPos);
                return newSquare.endTurnFunc(newPos);
            };
            Func<int, int> goToJail = (currentPos) =>
            {
#if VERBOSEOUTPUT
                Console.WriteLine("Go to jail.");
#endif
                return MoveTo((int)BoardPositions.JAIL);
            };
            Func<int, int> advanceToGo = (currentPos) =>
            {
#if VERBOSEOUTPUT
                Console.WriteLine("Advance to Go.");
#endif
                return MoveTo((int)BoardPositions.GO);
            };
            Func<int, int> goToC1 = (currentPos) =>
            {
#if VERBOSEOUTPUT
                Console.WriteLine("Go to St. Charles Place.");
#endif
                return MoveTo((int)BoardPositions.C1);
            };
            Func<int, int> goToE3 = (currentPos) =>
            {
#if VERBOSEOUTPUT
                Console.WriteLine("Go to Illinois Ave.");
#endif
                return MoveTo((int)BoardPositions.E3);
            };
            Func<int, int> goToH2 = (currentPos) =>
            {
#if VERBOSEOUTPUT
                Console.WriteLine("Go to Boardwalk");
#endif
                return MoveTo((int)BoardPositions.H2);
            };
            Func<int, int> goToR1 = (currentPos) =>
            {
#if VERBOSEOUTPUT
                Console.WriteLine("Go to Reading RR.");
#endif
                return MoveTo((int)BoardPositions.R1);
            };
            Func<int, int> goToNextR = (currentPos) =>
            {
#if VERBOSEOUTPUT
                Console.WriteLine("Go to next railroad.");
#endif
                if (currentPos < (int)BoardPositions.R1) return MoveTo((int)BoardPositions.R1);
                if (currentPos < (int)BoardPositions.R2) return MoveTo((int)BoardPositions.R2);
                if (currentPos < (int)BoardPositions.R3) return MoveTo((int)BoardPositions.R3);
                if (currentPos < (int)BoardPositions.R4) return MoveTo((int)BoardPositions.R4);
                return MoveTo((int)BoardPositions.R1);
            };
            Func<int, int> goToNextU = (currentPos) =>
            {
#if VERBOSEOUTPUT
                Console.WriteLine("Go to next utility.");
#endif
                if (currentPos < (int)BoardPositions.U1) return MoveTo((int)BoardPositions.U1);
                if (currentPos < (int)BoardPositions.U2) return MoveTo((int)BoardPositions.U2);
                return MoveTo((int)BoardPositions.U1);
            };
            Func<int, int> goToBack3 = (currentPos) =>
            {
#if VERBOSEOUTPUT
                Console.WriteLine("Go back 3 squares.");
#endif
                if (currentPos == (int)BoardPositions.CH1) return MoveTo((int)BoardPositions.T1);
                if (currentPos == (int)BoardPositions.CH2) return MoveTo((int)BoardPositions.D3);
                if (currentPos == (int)BoardPositions.CH3) return MoveTo((int)BoardPositions.CC3);
                throw new ArgumentException("Go back 3 called from not-chance square");
            };
            Func<int, int> doNothing = (currentPos) =>
            {
#if VERBOSEOUTPUT
                Console.WriteLine("Do nothing.");
#endif
                return currentPos;
            };
            Func<int, int> drawCommunityChest = (currentPos) =>
            {
                return communityChestDeck.DrawCard(currentPos);
            };
            Func<int, int> drawChance = (currentPos) =>
            {
                return chanceDeck.DrawCard(currentPos);
            };
            Func<int, int, int> takeTurn = null;
            takeTurn = (currentPos, numRollsSoFar) =>
            {
                const int diceSides = 4;
                var d1 = Utilities.RNG.getRandomInt(1, diceSides);
                var d2 = Utilities.RNG.getRandomInt(1, diceSides);
#if VERBOSEOUTPUT
                Console.WriteLine("Dice roll: {0}, {1}.", d1, d2);
#endif
                if (d1 == d2 && numRollsSoFar == 2)
                {
#if VERBOSEOUTPUT
                    Console.WriteLine("3 doubles in a row. Go to jail.");
#endif
                    return MoveTo((int)BoardPositions.JAIL);
                }
                var newPos = currentPos + d1 + d2;
                if(newPos >= board.squares.Count) newPos -= board.squares.Count;
                var square = getSquareAtPosition(newPos);
#if VERBOSEOUTPUT
                Console.WriteLine("Arrived at {0}.", square.name);
#endif
                newPos = square.endTurnFunc(newPos);
                
                if(newPos != (int)BoardPositions.JAIL && d1 == d2)
                {
#if VERBOSEOUTPUT
                    Console.WriteLine("Last roll was a double. Taking another turn.");
#endif
                    return takeTurn(newPos, ++numRollsSoFar);
                }
                return newPos;
            };
            

            #endregion

            #region set up board

            board = new Board();

            board.AddSquare((int)BoardPositions.GO, "Go", doNothing);
            board.AddSquare((int)BoardPositions.A1, "Mediterranean Ave", doNothing);
            board.AddSquare((int)BoardPositions.CC1, "Community Chest 1", drawCommunityChest);
            board.AddSquare((int)BoardPositions.A2, "Baltic Ave", doNothing);
            board.AddSquare((int)BoardPositions.T1, "Income Tax", doNothing);
            board.AddSquare((int)BoardPositions.R1, "Reading Railroad", doNothing);
            board.AddSquare((int)BoardPositions.B1, "Oriental Ave", doNothing);
            board.AddSquare((int)BoardPositions.CH1, "Chance 1", drawChance);
            board.AddSquare((int)BoardPositions.B2, "Vermont Ave", doNothing);
            board.AddSquare((int)BoardPositions.B3, "Connecticut Ave", doNothing);
            board.AddSquare((int)BoardPositions.JAIL, "Jail", doNothing);

            board.AddSquare((int)BoardPositions.C1, "St. Charles Pl", doNothing);
            board.AddSquare((int)BoardPositions.U1, "Electric Co", doNothing);
            board.AddSquare((int)BoardPositions.C2, "States Ave", doNothing);
            board.AddSquare((int)BoardPositions.C3, "Virginia Ave", doNothing);
            board.AddSquare((int)BoardPositions.R2, "Pennsylvania Railroad", doNothing);
            board.AddSquare((int)BoardPositions.D1, "St. James Pl", doNothing);
            board.AddSquare((int)BoardPositions.CC2, "Community Chest 2", drawCommunityChest);
            board.AddSquare((int)BoardPositions.D2, "Tennessee Ave", doNothing);
            board.AddSquare((int)BoardPositions.D3, "New York Ave", doNothing);
            board.AddSquare((int)BoardPositions.FP, "Free Parking", doNothing);

            board.AddSquare((int)BoardPositions.E1, "Kentucky Ave", doNothing);
            board.AddSquare((int)BoardPositions.CH2, "Chance 2", drawChance);
            board.AddSquare((int)BoardPositions.E2, "Indiana Ave", doNothing);
            board.AddSquare((int)BoardPositions.E3, "Illinois Ave", doNothing);
            board.AddSquare((int)BoardPositions.R3, "B&O Railroad", doNothing);
            board.AddSquare((int)BoardPositions.F1, "Atlantic Ave", doNothing);
            board.AddSquare((int)BoardPositions.F2, "Ventnor Ave", doNothing);
            board.AddSquare((int)BoardPositions.U2, "Water Works", doNothing);
            board.AddSquare((int)BoardPositions.F3, "Marvin Gardens", doNothing);
            board.AddSquare((int)BoardPositions.G2J, "Go to Jail", goToJail);

            board.AddSquare((int)BoardPositions.G1, "Pacific Ave", doNothing);
            board.AddSquare((int)BoardPositions.G2, "North Carolina Ave", doNothing);
            board.AddSquare((int)BoardPositions.CC3, "Community Chest 3", drawCommunityChest);
            board.AddSquare((int)BoardPositions.G3, "Pennsylvania Ave", doNothing);
            board.AddSquare((int)BoardPositions.R4, "Short Line", doNothing);
            board.AddSquare((int)BoardPositions.CH3, "Chance 3", drawChance);
            board.AddSquare((int)BoardPositions.H1, "Park Place", doNothing);
            board.AddSquare((int)BoardPositions.T2, "Luxury Tax", doNothing);
            board.AddSquare((int)BoardPositions.H2, "Boardwalk", doNothing);
            #endregion

            #region set up decks
            chanceDeck = new MonopolyCardDeck("Chance");
            chanceDeck.AddCard(advanceToGo, "Advance to Go");
            chanceDeck.AddCard(goToJail, "Go to Jail");
            chanceDeck.AddCard(goToC1, "Go to C1");
            chanceDeck.AddCard(goToE3, "Go to E3");
            chanceDeck.AddCard(goToH2, "Go to H2");
            chanceDeck.AddCard(goToR1, "Go to R1");
            chanceDeck.AddCard(goToNextR, "Go to next R");
            chanceDeck.AddCard(goToNextR, "Go to next R");
            chanceDeck.AddCard(goToNextU, "Go to next U");
            chanceDeck.AddCard(goToBack3, "Go back 3");
            chanceDeck.AddCard(doNothing, "Do nothing");
            chanceDeck.AddCard(doNothing, "Do nothing");
            chanceDeck.AddCard(doNothing, "Do nothing");
            chanceDeck.AddCard(doNothing, "Do nothing");
            chanceDeck.AddCard(doNothing, "Do nothing");
            chanceDeck.AddCard(doNothing, "Do nothing");
            

            communityChestDeck = new MonopolyCardDeck("Community Chest");
            communityChestDeck.AddCard(advanceToGo, "Advance to Go");
            communityChestDeck.AddCard(goToJail, "Go to Jail");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            communityChestDeck.AddCard(doNothing, "Do nothing");
            
            #endregion

            const int numTurns = 1000;
            const int numGames = 1000;
            for (int gameNum = 0; gameNum < numGames; gameNum++)
            {
                int currentPosition = (int)BoardPositions.GO;
                chanceDeck.Shuffle();
                communityChestDeck.Shuffle();
                for (int turnNum = 0; turnNum < numTurns; turnNum++)
                {
                    currentPosition = takeTurn(currentPosition, 0);
                    var newSquare = getSquareAtPosition(currentPosition);
#if VERBOSEOUTPUT
                Console.WriteLine("Turn {1} ended on {0}.", newSquare.name, turnNum);
                Console.WriteLine();
#endif
                    newSquare.numTurnEnds++;
                }
            }

            var orderedSquares = board.squares.OrderByDescending(x => x.numTurnEnds).ToArray();
#if VERBOSEOUTPUT
            foreach(var square in orderedSquares)
            {
                Console.WriteLine("{0} was an end turn square {1} times.", square.name, square.numTurnEnds); 
            }
#endif

            var first = orderedSquares[0].position;
            var second = orderedSquares[1].position;
            var third = orderedSquares[2].position;
            string answer = string.Format("{0}{1}{2}"
                , first.ToString("0#")
                , second.ToString("0#")
                , third.ToString("0#")
                );
			PrintSolution(answer.ToString());
			return;
		}
        
	}
}
