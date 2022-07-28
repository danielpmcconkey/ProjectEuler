module Euler027
open Primes

let run () = 

    (*
    This one was a pain in the arse to port from C#. I had tons of errors while 
    creating this one. One was my dumb ass trusting past me to code properly. 
    The C# comments say I only need to get primes up to 12989 so I used my 
    getPrimesUpToN function. Only problem is that that code has a bug in it and 
    only returns primes *under* N. Yeah, that took frickin' forever to figure 
    out.

    I also don't like how slow this runs. It takes 2155 milliseconds. Mind, the
    C# version takes 1658 milliseconds, so it's not bad. But I feel like there
    should be a faster way to do this. I don't know. Anytime you need to find
    the max of something, you gotta run every damned one of 'em...unless 
    there's a trick...
    
    *)
    let crossJoin lx ly =
        lx |> List.collect (fun x -> ly |> List.map (fun y -> x, y))

    let limit = 1000
    let numPrimes = 13001
    let primes = getPrimesUpToNSieve numPrimes

    let howManyPrimes a b =
        let l =
            (0, 2)
            |> List.unfold (fun (n, priorQuad) ->
                if priorQuad < 0 || primes.primeBools[priorQuad] = false then None
                else
                    let quadraticResult = (n * n) + (a * n) + b
                    let newState = (n + 1, quadraticResult)
                    Some(newState, newState)
            )
        if l.Length > 0 then 
            let lastN = l |> Seq.last |> fst
            lastN - 1
        else 0

    let values = [(limit * - 1)..limit] 
    let maxSet = 
        crossJoin values values 
        |> List.maxBy (fun (x, y) -> howManyPrimes x y)
    
    let answer = (fst maxSet) * (snd maxSet)
    answer.ToString()

