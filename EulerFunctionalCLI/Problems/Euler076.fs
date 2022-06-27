module Euler076


let run () =
    let subtract1 n = n - 1
    let target = 100
    let cache:int[] = Array.create (target + 1) -1
    let answer = (Algorithms.partitionFunction target cache) |> fst |> subtract1
    answer.ToString()
    
