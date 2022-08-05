module DomainTypes

type PrimeStarterPack = {
    primes:int[];
    primeBools:bool[];
}
type Fraction = {
    numerator:int;
    denominator:int;
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