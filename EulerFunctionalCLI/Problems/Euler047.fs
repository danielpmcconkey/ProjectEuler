module Euler047
open Conversions
open Primes

let run () =
    (*
    This is close to the C# solution, though the C# determines prime numbers 
    based on the fact that numbers in the array are still 0 by the time you get
    to them. This was just a lot less mental energy to convert to FP. Lazy, I 
    know, but this still runs in 230ms.
    *)
    let isWinner (n:int) (factorCounts:int[]) (target:int) =
        Array.sub factorCounts n target 
        |> Array.forall (fun i -> i >= target)
    let getAnswerFromOption o = match o with | Some(n) -> n | None -> -1
    let limit = (int)1e6
    let primePack = getPrimesUpToNSieve limit
    let primes = primePack.primes

    let factorCounts = Array.create (limit + 1) 0
    primes
    |> Array.iter (fun p ->
        [|p..p..limit|] |> Array.iter (fun pFactor -> 
                factorCounts[pFactor] <- factorCounts[pFactor] + 1
            )
        )
    let target = 4
    [|1..(limit - target)|] 
    |> Array.tryFind (fun n -> isWinner n factorCounts target) 
    |> getAnswerFromOption
    |> intToString
