module Euler037
open Conversions
open Primes

let run () =

    (*
    The hardest part about this one is understanding the requirement. I'm 
    pretty sure that both times I solved this problem (C# first and F# here), 
    I missed that you are only shaving off one digit at a time. So, for 3797, 
    you never look at 9 alone. You have 379 and you have 97, but never just 9.

    The isTruncatablePrime looks a little different than what I used in the C#
    solution, but that's because I didn't bother to build a version that could
    support left or right shaving alone. I'm sure I'll build that later in this
    journey. Because of that, this runs significantly faster than the C# version. 
    And we don't even break here after findnig 11 of them.

    I still don't have a good basis for using 1MM as my max prime. It can be 
    shown that there are no more truncatable primes beyond the 11 this program
    finds by adding 1, 2, 3, 5, 7, or 9 to the fronts and backs of each of the
    truncatable primes. None of those combinations yields a prime, so you know
    you're at the end. However, that only works to prove there are no more. It
    assumes you know the answer. I suppose my 1MM was a lucky guess and I'll 
    just have to leave it here.

    ... unless I reworked the whole problem. I could start with all the 
    single-digit primes, add the above digits to each, and check for prime. I 
    would stop only when this yields no further primes. That would work, but 
    it'd be especially hard in FP. So I'm good with this solution.
    *)

    let isTruncatablePrime n (primeBools:bool[]) = 
        if n < 10 then false
        else
            let digits = n |> intToIntArray 
            let hasNonPrimeDigits = (
                digits |> Array.contains 4 
                || digits |> Array.contains 6 
                || digits |> Array.contains 8 
                || digits |> Array.contains 0 
            )
            if hasNonPrimeDigits then false 
            else
                let right = 
                    [|for i in 0..(digits.Length - 1) do digits |> Array.skip i |] 
                    |> Array.map (fun x -> intArrayToInt x)
                    |> Array.forall (fun x -> primeBools[x])
                let left = 
                    [|for i in 0..(digits.Length - 1) do digits |> Array.take ((Array.length digits)- i)|] 
                    |> Array.map (fun x -> intArrayToInt x)
                    |> Array.forall (fun x -> primeBools[x])
                left && right

    let limit = (int)1e6
    let primePack = getPrimesUpToNSieve limit
    let primes = primePack.primes
    let primeBools = primePack.primeBools
    primes 
    |> Array.filter (fun x -> isTruncatablePrime x primeBools)
    |> Array.sum
    |> intToString
