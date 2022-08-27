module Euler076
open Algorithms
open Conversions

let run () =

    (*
    The mathNerdSolution function is a reproduction of the C# partition 
    function. It runs super fast, but I don't really understand the math. So I
    created the computerNerdSolution as a dynamic programming exercize. It's 
    almost identical to the solution to problem 31. The dictionary caches
    repeat results. Without it, it'd take 2 minutes to run.
    *)

    let computerNerdSolution target = 
        let dict = new System.Collections.Generic.Dictionary<int * int, int>()
        let rec howMany n limit =
            if dict.ContainsKey((n, limit)) then dict[(n, limit)]
            else 
                if n = 0 then 1
                elif n < 0 then 0
                else
                    let result = 
                        [|1..limit|]
                        |> Array.map (fun x -> howMany (n - x) x)
                        |> Array.sum
                    dict.Add((n, limit), result)
                    result
        
        howMany target target
        |> (fun x -> x - 1)
        |> intToString

    let mathNerdSolution target =        
        let cache:int[] = Array.create (target + 1) -1
        partitionFunction target cache
        |> fst
        |> (fun x -> x - 1)
        |> intToString

    let target = 100
    //mathNerdSolution target // runs in 4 ms
    computerNerdSolution target // runs in 28 ms