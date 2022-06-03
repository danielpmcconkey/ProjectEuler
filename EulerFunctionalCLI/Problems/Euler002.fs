module Euler002

let run () =
    let fibgen (x,y) =
        if(x < 4000000) then
            Some(x+y, (y,x+y)) // return a tuple of the next x y pair to run through fibgen
        else None

    let fibseq = Seq.unfold fibgen (1,1) // create a sequence of fibgen results starting w/ the tuple (1,1)
    let fibevens = seq{for i in fibseq do if i % 2 = 0 then yield i}
    let result = Seq.sum fibevens
    
    printfn "%d" result