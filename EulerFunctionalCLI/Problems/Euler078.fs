module Euler078
open System.Collections.Generic
open System.Numerics

let run () =
    
    let target = BigInteger 100
    let cache:Dictionary<BigInteger, BigInteger> = new Dictionary<BigInteger,BigInteger>()
    cache[0] <- 1

    let start:BigInteger = BigInteger 1
    let targetDivisor:BigInteger = BigInteger 1000000

    let findAnswer =
        start 
        |> Seq.unfold(fun state ->
            let i = state
            let partition = Algorithms.partitionFunctionBig i cache
            let howMany = fst partition
            let nextI = i + BigInteger 1
            if howMany % targetDivisor = BigInteger 0 then 
                printfn "%s : %s" (i.ToString()) (howMany.ToString())
                None
            elif howMany < BigInteger 0 then 
                None
            else
                Some(state, nextI)
        )
    let add1 (n: BigInteger) = n + BigInteger 1

    let answer = findAnswer |> Seq.last |> add1
    answer.ToString()
    
