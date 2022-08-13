module Euler063
open Conversions
let run () = 
    (*
    My approach was to determine a min value for x such that x raised to y has 
    y digits. This is done raising 10 to the y-1 power and then raising that 
    value to the 1/y power to find your minimum x. The max x will always be 9. 
    Subtract min from max and you get your count at each y exponent.

    Note: I arbitrarily chose 25 as my maximum exponent / number of digits. I 
    figured that, since 9^25 has 23 digits and 10^25 has 26 digits, then that 
    means there are no exponent / number of digits that fit. I don't know if 
    this is right. (Yes, the last number that fits this pattern is 9^21, so I 
    could've used 22 as my max instead, but I went with 25 for...I don't 
    know...reasons.
    *)

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