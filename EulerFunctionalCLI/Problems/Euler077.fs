module Euler077
open Primes
open DomainTypes
open Conversions

let run () =
    (*
    While working on my original C# solution, I recognized this as the same 
    problem as #31. But, for some reason, I couldn't adapt it to this problem 
    and ended up stumbling on prime partitions 
    (https://mathworld.wolfram.com/PrimePartition.html). A couple of months 
    later, I'm taking a fresh look at the problem while re-solving them all in
    F# and was rather easily able to copy/paste from my problem 31 solution. 
    All I had to do was change "coins" to "primes".

    Note: if you look through the change history in BitBucket, you'll see that
    I originally implemented a prime partition function here in F# as well. 
    That was right after I'd solved it in C# and was just learning F#. 
    The solution worked and ran very fast, but it was ugly. I'd much rather 
    sacrifice 100ms for readability.
    *)
    
    let rec howManyWays targetSum primes = 
        if targetSum = 0 then 1
        else
            primes 
            |> Array.map (fun x -> (targetSum - x, primes |> Array.filter (fun y -> y <= x))) 
            |> Array.filter (fun (remainder, primesLeft) -> remainder >= 0) 
            |> Array.map (fun (remainder, primesLeft) -> howManyWays remainder primesLeft)
            |> Array.sum

    let target = 5000
    let primes = (getPrimesUpToNSieve target).primes
    [|1..target|]
    |> Array.find (fun i -> (howManyWays i primes) > target)
    |> intToString

    