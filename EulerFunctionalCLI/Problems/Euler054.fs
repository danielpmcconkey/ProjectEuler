module Euler054
open DomainTypes
open IO
open Conversions

let run () = 
    


    let linesToHands (lines:string[]) (cardRanks:char[]) = 
        lines
        |> Array.map (fun s ->
            s 
            |> splitOnSpace
            |> Array.map (fun s2 -> { rank = (Array.findIndex (fun x -> x = s2[0]) cardRanks); suit = s2[1] })
            )
        |> Array.map (fun a -> (a[0..4], a[5..9]))
    let rankHand (h:PlayingCard[]) = 
        let suitGroups = h |> Array.groupBy (fun c -> c.suit)
        let rankGroups = h |> Array.groupBy (fun c -> c.rank) |> Array.sortByDescending (fun g -> (Array.length (snd g)))
        let orderedRanks = h |> Array.sortByDescending (fun c -> c.rank) |> Array.map (fun c -> c.rank)
    
        // check for straight, flush, and straightflush
        let isStraight = rankGroups.Length = 5 && orderedRanks[0] - orderedRanks[4] = 4
        let isFlush = suitGroups.Length = 1
        if isStraight && isFlush then (PokerHand.STRAIGHT_FLUSH, orderedRanks)
        elif isFlush then (PokerHand.FLUSH, orderedRanks)
        elif isStraight then (PokerHand.STRAIGHT, orderedRanks)
        else 
            match rankGroups.Length with
            // check for 4 of a kind and full house at the same time. both have 2 groups of ranks
            | 2 -> 
                let tieBreakers = [|(fst rankGroups[0]);(fst rankGroups[1])|]
                if Array.length (snd rankGroups[0]) = 4 then (PokerHand.FOUR_OF_A_KIND, tieBreakers)        
                else (PokerHand.FULL_HOUSE, tieBreakers)
            // check for 3 of a kind and 2 pairs at the same time. both have 3 groups of ranks
            | 3 ->
                let tieBreakers = [|(fst rankGroups[0]); (fst rankGroups[1]); (fst rankGroups[2])|]
                if Array.length (snd rankGroups[0]) = 3 then (PokerHand.THREE_OF_A_KIND, tieBreakers)        
                else (PokerHand.TWO_PAIRS, tieBreakers)
            // just a pair?
            | 4 ->
                let tieBreakers = [|(fst rankGroups[0]); (fst rankGroups[1]); (fst rankGroups[2]); (fst rankGroups[3]) |]
                (PokerHand.ONE_PAIR, tieBreakers)
            // high card
            | 5 -> (PokerHand.HIGH_CARD, orderedRanks)
            | _ -> failwith "CHEATER!!!"
    let whoWinsTieBreakerRound n m =
        if n > m then Some(1)
        elif m > n then Some(2)
        else None
    let player1Wins hands =
        let h1 = fst hands
        let h2 = snd hands
        let rank1 = rankHand h1
        let rank2 = rankHand h2
        if (fst rank1) > (fst rank2) then true
        elif (fst rank1) < (fst rank2) then false
        // tie breaker
        else
            let tieBreakers1 = snd rank1
            let tieBreakers2 = snd rank2
            match (whoWinsTieBreakerRound tieBreakers1[0] tieBreakers2[0]) with
            | Some(1) -> true
            | Some(2) -> false
            | None -> 
                match (whoWinsTieBreakerRound tieBreakers1[1] tieBreakers2[1]) with
                | Some(1) -> true
                | Some(2) -> false
                | None -> failwith "STILL TIED"
                | _ -> failwith "Uhhh..."
            | _ -> failwith "Dealer won?"
 

    let cardRanks = [|'2';'3';'4';'5';'6';'7';'8';'9';'T';'J';'Q';'K';'A'|]

    let input = get54Input ()
    let hands = linesToHands input cardRanks
    hands 
    |> Array.filter (fun h -> player1Wins h)
    |> Array.length
    |> intToString
