module Euler063
open Conversions
let run () = 

    let getCountAtExp numDigits = 
        let minVal = 10.0**(numDigits - 1.0)
        let rootMax = 9 // it'll always be 9 because 10^x will always have 1 more than x digits
        let rootMin = (int)(ceil(minVal**(1.0/numDigits)))
        let count = rootMax - rootMin + 1
        if count >= 0 then count else 0
    [|1..25|]
    |> Array.map (fun i -> getCountAtExp ((float)i))
    |> Array.sum
    |> intToString