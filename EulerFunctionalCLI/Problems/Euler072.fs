module Euler072
open Conversions
open Primes

let run () = 
    (*
    This was difficult to optimize and my method for pre-fetching all the prime
    factors is not idiomatically functional. I just couldn't figure out a 
    fast-running algorithm that didn't break functional ideals.

    Runs in about 1800 ms.
    *)

    let eulersTotient (n:int) (primeFactors:int[]) = 
        primeFactors
        |> Array.fold (fun acc elem -> acc * (1.0 - (1.0 / (float)elem))) ((float)n)
        |> floatToInt

    let limit = (int)1e6
    let primeFactors:int[][] = primeFactorsOf0ToN limit
    [|2..limit|]
    |> Array.map (fun n -> eulersTotient n primeFactors[n])
    |> Array.map (fun n -> intToLong n)
    |> Array.sum
    |> intToString

