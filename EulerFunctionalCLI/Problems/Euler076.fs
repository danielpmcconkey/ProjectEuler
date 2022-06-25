module Euler076


let run () =

    let howManyWaysToSumANumber n =
        let cache:int[] = Array.create (n+1) -1
        cache[0] <- 1
        let rec P n = // (n : int) :int =
            if n < 0 then 0
            elif cache[n] > -1 then cache[n]
            else 
                let result = 
                    seq<int> { 
                        for k in 1 .. n do 
                            let n1 = n - k * (3 * k - 1) / 2
                            let n2 = n - k * (3 * k + 1) / 2
                            let Pn1 = P n1
                            let Pn2 = P n2
                            (Pn1 + Pn2) * pown -1 (k+1)
                    } 
                    |>Seq.sum
                cache[n] <- result
                result
        P n - 1

    let answer = howManyWaysToSumANumber 100 
    answer.ToString()
    
