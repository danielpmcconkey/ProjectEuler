module Euler046
open DomainTypes
open Conversions
open Primes
open Algorithms

let run () =
    let getAnswerFromOption o = match o with | Some(n) -> n | None -> -1

    let start = 33
    let limit = (int)1e5
    let primePack = getPrimesUpToNSieve limit
    let primeBools = primePack.primeBools
    let primes = primePack.primes
    let squares = getPerfectSquaresUpToN limit

    [|start..2..limit|]
    |> Array.filter (fun i -> primeBools[i] = false)
    |> Array.tryFind (fun i ->
        let primesLessThan = primes |> Array.filter (fun x -> x < i)
        let squaresLessThan = squares |> Array.filter (fun x -> x < i)
        let joined = crossJoinArrays primesLessThan squaresLessThan
        let canBeWrittenO = joined |> Array.tryFind (fun (prime, square) ->
            (prime + (2 * square)) = i
            ) 
        match canBeWrittenO with 
        | Some(t) -> false
        | None -> true
        )
    |> getAnswerFromOption
    |> intToString