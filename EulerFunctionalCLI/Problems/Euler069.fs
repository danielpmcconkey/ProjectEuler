module Euler069


open DomainTypes
open Primes
open Conversions


let run () =
    let limitPrimes = 100
    let limit = (int)1e6
    let primePack = getPrimesUpToNSieve limitPrimes
    let primes = primePack.primes

    let rec multiplyPrimes (i:int) (limit:int) (primes:int[]) (lastProduct:int) =
        let nextProduct = lastProduct * primes[i]
        if nextProduct > limit 
            then lastProduct
            else multiplyPrimes (i + 1) limit primes nextProduct

    multiplyPrimes 0 limit primes 1
    |> intToString
