module Euler001


let run () =

    let multiples = seq { for i in 1..999 do if (i % 3 = 0 || i % 5 = 0) then yield i}
    
    
    multiples
    |> Seq.sum
    


