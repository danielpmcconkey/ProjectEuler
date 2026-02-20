module Euler087
open Primes
open DomainTypes
open Conversions
let run () =

    let limit = (int)5e7
    let maxPrime = (int)(ceil (sqrt ((float)limit)))
    let maxPrimeToBeCubed = (int)(floor ((float)limit ** (1.0/3.0)))
    let maxPrimeToBeQuad = (int)(floor ((float)limit ** (1.0/4.0)))
    let primes = (getPrimesUpToNSieve maxPrime).primes
    let primesSquared = primes |> Array.map (fun p -> p * p)
    let primesCubed = primes |> Array.filter (fun p -> p <= maxPrimeToBeCubed) |> Array.map (fun p -> p * p * p)
    let primesQuad = primes |> Array.filter (fun p -> p <= maxPrimeToBeQuad) |> Array.map (fun p -> p * p * p * p)

    primesSquared
    |> Array.collect (fun square ->
        primesCubed |> Array.map (fun cube -> square + cube)
        )
    |> Array.collect (fun squarePlusCube ->
        primesQuad |> Array.map (fun quad -> squarePlusCube + quad)
        )
    |> Array.filter (fun x -> x < limit)
    |> Array.distinct
    |> Array.length
    |> intToString

