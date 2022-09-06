module DomainTypes

type PrimeStarterPack = {
    primes:int[];
    primeBools:bool[];
}
type Fraction = {
    numerator:int;
    denominator:int;
}
type FractionLong = {
    numeratorLong:int64;
    denominatorLong:int64;
}
type FractionBig = {
    numeratorBig:bigint;
    denominatorBig:bigint;
}
type IntegerTriangle = {
    aLength:int;
    bLength:int;
    cLength:int;
    perimeter:int;
}
type PlayingCard = {
    suit:char;
    rank:int;
}
type PokerHand = 
    | HIGH_CARD = 0
    | ONE_PAIR = 1
    | TWO_PAIRS = 2
    | THREE_OF_A_KIND = 3
    | STRAIGHT = 4
    | FLUSH = 5
    | FULL_HOUSE = 6
    | FOUR_OF_A_KIND = 7
    | STRAIGHT_FLUSH = 8

type ContinuedFraction = { 
    firstCoefficient: int
    subsequentCoefficients: int []
    doCoefficientsRepeat: bool 
}
type DigitalFactorialChain = {
    n : int;
    length : int;
    chain : int[];
}
type XyCoordinate = {
    x: int
    y: int
}
type Route = {
    cost: int
    destination: XyCoordinate
}
type Node = {
    position: XyCoordinate
    heuristicCost: int option
    distanceTo: int option
    pathVia: XyCoordinate option
    r_up: Route option
    r_right: Route option
    r_down: Route option
    r_left: Route option
}
type MonopolySquare = {
    position: string
    name: string
    endTurnFunc: MonopolyGame -> MonopolyGame
    numTurnEnds: int
}
and MonopolyCard = {
    name: string
    cardFunc: MonopolyGame -> MonopolyGame
}
and MonopolyGame = {
    board: List<MonopolySquare>
    currentPos: int
    chanceDeck: List<MonopolyCard>
    communityChestDeck: List<MonopolyCard>
}