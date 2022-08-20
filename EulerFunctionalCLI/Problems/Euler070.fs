module Euler070



open DomainTypes
open Primes
open Algorithms
open Conversions
// open FractionCalculator
// open IO

let run () =

    (*
    If you want to minimize n / phi of n then you want to maximize phi of n, or 
    the number of relative primes. Actual primes have the highest relative phi, 
    but their phi values are always n - 1 and will never render n and phi of n 
    that are permutations of one another. So that means I want to find primes 
    multiplied by primes to get the next highest relative phi values.

    Here, I cross join an array of primes between 0.5 times the square root of 
    1e7 and 2 times that same square root. I then use the fact that the phi of 
    mn = phi of m * phi of n (part of Euler's product formula) to quickly come 
    up with a short list of (n, phi of n) pairs to test for sharing the same 
    digits.

    It runs in about 1800 milliseconds. Too long for my tastes, but I'm 
    struggling with run-time optimization during this journey toward F# 
    proficiency.
    *)
    
    let limit = (int)1e7
    let primePack = getPrimesUpToNSieve limit
    let primes = primePack.primes

    let sqrtLimit = (int)(ceil (sqrt ((float)limit)))
    let maxPrime = sqrtLimit * 2
    let minPrime = sqrtLimit / 2
    let viablePrimes =
        primes
        |> Array.filter (fun p -> p >= minPrime && p <= maxPrime)
    
    viablePrimes 
    |> Array.collect (fun m -> viablePrimes |> Array.filter (fun n -> n > m) |> Array.map (fun n -> (m, n)) )
    |> Array.filter (fun (m, n) -> (int64)m * (int64)n <= (int64)limit)
    |> Array.map (fun (m, n) -> (m * n, (m - 1) * (n - 1)))
    |> Array.filter (fun (n, phiOfN) -> areIntegersPermutations n phiOfN)
    |> Array.minBy (fun (n, phiOfN) -> (float)n / (float)phiOfN)
    |> fst
    |> intToString