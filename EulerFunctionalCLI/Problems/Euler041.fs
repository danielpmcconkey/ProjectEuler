module Euler041
open DomainTypes
open Conversions
open Primes

let run () =
    let isPandigital n =
        let sorted = n |> intToListInt |> List.sort
        if sorted[0] = 0 || (sorted |> List.length) <> (sorted |> List.max) then false
        elif (sorted |> List.distinct |> List.length) <> (sorted |> List.length) then false
        else true  

    let limit = (int)1e7
    let primePack = getPrimesUpToNSieve limit
    primePack.primes
    |> Array.filter (fun x -> isPandigital x)
    |> Array.last
    |> intToString