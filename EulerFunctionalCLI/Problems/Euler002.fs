module Euler002

let run () =
    let limit = 4000000



    let getEvenNumberedValuesFromSeq sequence = 
        seq { 
            for x in sequence do 
                if x % 2 = 0 then
                    x
        }

    let answer = 
        Algorithms.getFibonacciSeq limit 
        |> getEvenNumberedValuesFromSeq
        |> Seq.sum

    answer.ToString()



    
