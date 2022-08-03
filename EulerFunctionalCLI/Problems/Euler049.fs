module Euler049
open Primes
open DomainTypes
open Conversions
open Algorithms

let run () = 

    (*
    This code is pretty ugly. I've got functions where I'm doing multiple 
    things and I'm certain I could've optimized it better. Also, if you remove 
    the filter that takes out the solution that starts with 1487, you'll see 
    that my code returns multiple rows for that one solution. So my joins and 
    groups aren't right here. Basically, this produces the right answer despite 
    itself.
    *)
    let filterPermsForPrimes (l:List<int>) (primeBools:bool[]) = 
        l |> List.filter (fun n -> n >= 1000 && primeBools[n])
    let joinAndDiff (l:List<int>) = 
            l 
            |> List.collect (fun x -> 
                    l 
                    |> List.filter (fun z -> z > x) 
                    |> List.map (fun y -> x, y, y-x)
                )
    let groupByDiff l = 
        l 
        |> List.groupBy (fun (p1, p2, diff) -> diff) 
        |> List.filter (fun t -> (List.length (snd t)) >= 2)
        |> List.filter (fun t -> 
                let a = snd t
                let p1_0, p2_0, diff_0 = a[0]
                let p1_1, p2_1, diff_1 = a[1]
                p1_0 <> 1487 && p2_0 = p1_1
                )
    let maxPrime = 9999
    let primePack = getPrimesUpToNSieve maxPrime
    let answerRow = 
        primePack.primes
        |> Array.filter (fun p -> p > 1000)
        |> Array.map (fun p -> 
            p 
            |> intToListInt 
            |> getAllPermutationsOfList
            |> List.map (fun l -> listIntToInt l)
            |> List.distinct
            )
        |> Array.map (fun l -> filterPermsForPrimes l primePack.primeBools)
        |> Array.filter (fun l -> l |> List.length >= 3)
        |> Array.map (fun l -> l |> joinAndDiff)
        |> Array.map (fun l -> l |> groupByDiff)
        |> Array.filter (fun l -> l |> List.length >= 1)
    let diff, l = answerRow[0][0]
    let p1_0, p2_0, diff_0 = l[0]
    let p1_1, p2_1, diff_1 = l[1]
    sprintf "%d%d%d" p1_0 p2_0 p2_1
