module Euler048

let run () =

    (*
    So, instead of utilizing the BigInteger class and just boring my way 
    through all the arithemetic, I used a modulo to "clip" the answer at 10 
    characters each step of the way. It runs lickety split.

    Lickety.
    *)

    let longToPaddedString (n:int64) = n.ToString("0000000000")
    let addAndCrop (l1: int64) (l2: int64) = (l1 + l2) % (int64) 1e10
    let multiplyAndCrop (l1: int64) (l2: int64) = (l1 * l2) % (int64) 1e10
    let raiseToSelf i = 
        Array.create i i 
        |> Array.fold (fun acc elem -> multiplyAndCrop ((int64)acc) ((int64)elem)) 1L
    [|1..1000|] 
    |> Array.fold (fun acc elem -> addAndCrop acc (raiseToSelf elem)) 0L
    |> longToPaddedString
