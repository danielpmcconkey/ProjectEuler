
module Euler010


let run () =
    
    let limit = (int)2e6
    let primesPack = Algorithms.getPrimesUpToNSieve limit


    let answer = primesPack.primes |> Seq.fold(fun (acc:int64) (a:int) -> acc + (int64)a) 0

    answer.ToString()