module Euler051
open Primes
open Conversions
open Algorithms

let run () =
    (*
    This one was brutal for me to translate to F# and the functional paradigm. 
    It took 49 lines of meaningful code in F# versus the 37 lines in my C# 
    solution. And it just looks a lot more complicated, too. Usually my C# code 
    is a mess to understand while F# is practically self-documenting. Not the 
    case with problem 51.
    *)
    let getAnswerFromOption o = match o with | Some(n) -> n | None -> -1
    let binaryCombosAtNumDigits n =
        let max = (int) (2.0 ** ((float) (n - 1))) - 2
        [| 0..max |]
        |> Array.map (fun x -> System.Convert.ToString(x, 2).PadLeft((n - 1), '0').ToCharArray())

    let swapDigits (n: int) (replacementCombos: char [] []) =
        let nAsArray = intToIntArray n
        replacementCombos
        |> Array.map (fun combo ->
            [|0..9|]
            |> Array.map (fun replacement ->
                let perms =
                    [|0..(nAsArray.Length - 1)|]
                    |> Array.map (fun i -> 
                        if i = (nAsArray.Length - 1) 
                        then nAsArray[i] 
                        elif combo[i] = '0' then replacement 
                        else nAsArray[i])
                intArrayToInt perms)
                |> Array.filter (fun x -> orderOfMagnitude x = orderOfMagnitude n )
            )
    let countPrimeSwaps (binarayPerms:char[][]) (primeBools:bool[]) n =
        let swapped = swapDigits n binarayPerms
        swapped
        |> Array.filter (fun a -> Array.contains n a) // do this to make sure n is part of the family, otherwise 12038 would win
        |> Array.map (fun a -> a |> Array.map (fun n -> if primeBools[n] then 1 else 0))
        |> Array.map (fun a -> a |> Array.sum)
        |> Array.max

    // create all the possible binary permutations for later digit replacement
    // we only need to create 2^3 combos for 4-digit numbers because we won't 
    // replace the last digit. Why? because doing so would generate a series 
    // that has a number each that ends in 2, 4, 5, 6, 8, or 0. No way to get 8
    // primes when 6 of them end in impossible-to-be-prime numbers.
    let binaryPermutations =
        [| 5; 6 |]
        |> Array.map (fun x -> binaryCombosAtNumDigits x)


    let limit = (int) 1e6
    let primePack = getPrimesUpToNSieve limit
    let primes = primePack.primes
    let primeBools = primePack.primeBools
    // only start after 56003, the number given as the first 7-prime combo
    let startIndex = Array.findIndex (fun x -> x = 56003) primes
    let shortPrimes = primes[startIndex .. (primes.Length - 1)]

    shortPrimes
    |> Array.tryFind (fun p -> (
        let permsIndex  = if p |> orderOfMagnitude = 4 then 0 else 1
        let swaps = p |> countPrimeSwaps binaryPermutations[permsIndex] primeBools 
        swaps = 8
        )
    )
    |> getAnswerFromOption 
    |> intToString

