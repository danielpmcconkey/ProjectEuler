module Euler078
open System.Collections.Generic
open System.Numerics
open Conversions

let run () =
    
    (*
    This forced me to finally write an FP-idiomatic partition function. I'd 
    been avoiding it every way I could. I don't like the way the cache is 
    implemented here but it's the lesser of the few evils I could think of.
    *)

    let cache = new Dictionary<bigint,bigint>()
    cache[0I] <- 1I


    let rec partitionFunctionBig (n:bigint) =
        let rec subPartition (k:bigint) (Pn:bigint) (n:bigint) = 
            let n1 = n - k * (3I * k - 1I) / 2I
            let n2 = n - k * (3I * k + 1I) / 2I
            if n1 < 0I && n2 < 0I then Pn
            else
                let Pn1 = partitionFunctionBig n1
                let Pn2 = partitionFunctionBig n2
                let Pn_combined = Pn1 + Pn2
                let newPn = if k % 2I = 1I then Pn + Pn_combined else Pn - Pn_combined
                subPartition (k + 1I) newPn n
        if n < 0I then 0I
        elif cache.ContainsKey n then cache[n]
        else
            let initialK = 1I
            let initialPn = 0I
            let result = subPartition initialK initialPn n
            cache[n] <- result
            result

    let start = 1I
    let targetDivisor = 1000000I
    [|start..targetDivisor|]
    |> Array.find (fun i -> (partitionFunctionBig i) % targetDivisor = 0I)
    |> intToString    
    
