module Euler078
open System.Collections.Generic
open System.Numerics

let run () =
    let subtract1 (n:BigInteger) = n - BigInteger 1
    let target = BigInteger 100
    let mutable cache:Dictionary<BigInteger, BigInteger> = new Dictionary<BigInteger,BigInteger>()
    cache[0] <- 1

    let start:BigInteger = 1
    let targetDivisor:BigInteger = 1000000

    let zerbet =
        (start, cache)
        |> Seq.unfold(fun state ->
            let i = fst state
            let dict = snd state
            let partition = (Algorithms.partitionFunctionBig i dict)
            let howMany = fst partition
            let newDict = snd partition
            let nextI = i + BigInteger 1
            if howMany % targetDivisor = BigInteger 0 then 
                None
            elif howMany < 0 then 
                None
            else
                Some(state, (nextI, newDict))
        )
    let add1 (n: BigInteger) = n + BigInteger 1

    //let limit:BigInteger = 60000
    //let mutable found = false
    //let mutable answer = 0
    //for i in start .. limit do
    //    if found 
    //        then ()
    //    else
    //        let burp = Algorithms.partitionFunctionBig i cache
    //        cache <- snd burp
    //        let count = fst burp
    //        if count % targetDivisor = 0 then
    //            found <- true
    //            ()
    //        else ()
    
    let answer = zerbet |> Seq.last |> fst |> add1
    answer.ToString()
    
