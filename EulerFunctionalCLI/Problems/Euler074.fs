module Euler074

open DomainTypes
open Algorithms
open Conversions



let run () =

    (*
    This is a re-write of my original F# take on problem 74. Back 2 months ago,
    when I was first learning F#, I took a stab at converting my C# solution. 
    It was horrible. I set up a mutable dictionary that I kept overwriting with
    every pass of a "for i in 1..1000000 do" loop.

        mutableDict <- processRow i mutableDict

    Something like that. Now, in truth, I'm not doing it too terribly 
    differently, but it just feels more FP. I *do* rely on the fact that Arrays
    in F# are inherently mutable. But I use recurrsion now to pass my updated
    dict array to the next generation and, when I reach my base state, send it 
    back out the stack.

    I replaced a lot of clumsy ways of doing things with much more elegant F#.
    Things like using pipelines or built in array functions like iteri. This is
    also a good showcase of recursion being used instead of for loops.
    *)

    let rec getRepeatChain n unknownValue (currentChain:DigitalFactorialChain) (dict: int[]) =
        if dict[n] = unknownValue then
            let digitFact = sumOfDigitFactorials n
            let newCurrentChain = {
                n = currentChain.n
                length = currentChain.length + 1
                chain = Array.concat [|currentChain.chain; [|n|]|]
                }
            getRepeatChain digitFact unknownValue newCurrentChain dict
        else ({
                n = currentChain.n
                length = currentChain.length + dict[n]
                chain = currentChain.chain
            }, dict)

    let rec processRow i (dict:int[]) limit unknownValue =
        if i > limit then dict
        else
            if dict[i] = unknownValue then
                // process value
                let startChain = {n = i; length = 0; chain = [||]}
                let (dfc, newDict) = getRepeatChain i unknownValue startChain dict
                // add new chain elements to newDict
                dfc.chain
                |> Array.iteri (fun i chainVal -> newDict[chainVal] <- dfc.length - i)
                // process the next row
                processRow (i + 1) newDict limit unknownValue 
            else 
                // row already processed, move to the next
                processRow (i + 1) dict limit unknownValue

    let limit = (int)1e6
    let start = 1
    let target = 60
    let maxFactorialSum = 1 + sumOfDigitFactorials (limit - 1)
    let unknownValue = -1
    let dict = Array.create maxFactorialSum unknownValue
    // add the integers whose factorial sums are themselves (like 145 in 
    // the problem statement) to the dictionary to prevent recurrsive loops
    dict[1]      <- 1
    dict[2]      <- 1
    dict[145]    <- 1
    dict[40585]  <- 1

    // now add the integers whose chain lengths are given in the problem 
    // statement
    dict[169]    <- 3
    dict[871]    <- 2
    dict[872]    <- 2
    dict[1454]   <- 3
    dict[45361]  <- 2
    dict[45362]  <- 2
    dict[363601] <- 3

    processRow start dict limit unknownValue
    |> Array.filter (fun x -> x = target)
    |> Array.length
    |> intToString
    
