module Euler050
open Primes
let run () =
    let getMaxRange primes limit =
        [| 0..1000 |]
        |> Array.map (fun i -> Array.sub primes 0 i |> Array.sum)
        |> Array.filter (fun x -> x < limit)
        |> Array.length

    let limit = (int) 1e6
    let primePack = getPrimesUpToNSieve limit
    let primes = primePack.primes
    let primeBools = primePack.primeBools
    let maxRange = getMaxRange primes limit
    let biggestPrime = primes[primes.Length - 1]
    let minStreak = 20
    let startVals = [| 0 .. (primes.Length - minStreak) |]
    let ranges = [| minStreak .. (primes.Length) |]

    let answer =
        startVals
        |> Array.collect (fun startVal ->
            ranges
            |> Array.filter (fun range -> range <= maxRange - startVal)
            |> Array.map (fun range -> startVal, range))
        |> Array.filter (fun (start, range) -> start + range < primes.Length)
        |> Array.map (fun (start, range) -> Array.sub primePack.primes start range)
        |> Array.map (fun a -> (a.Length, a |> Array.sum))
        |> Array.filter (fun (len, sum) -> sum <= biggestPrime && primeBools[sum])
        |> Array.maxBy (fun (len, sum) -> len)

    (snd answer).ToString()
