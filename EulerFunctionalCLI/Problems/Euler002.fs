module Euler002

let run () =
    let limit = 4000000

    let getFibSeq limit = 
        (1, 1) // initial state
        |> Seq.unfold ( 
            fun state -> 
                if ( fst state + snd state > limit ) 
                    then None 
                    else Some (fst state + snd state, (snd state, fst state + snd state))
        )

    let getEvenNumberedValuesFromSeq sequence = 
        seq { 
            for x in sequence do 
                if x % 2 = 0 then
                    x
        }

    let answer = 
        getFibSeq limit 
        |> getEvenNumberedValuesFromSeq
        |> Seq.sum

    answer



    
