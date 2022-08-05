module Euler057
open System.Numerics
open Conversions
let run () =
    (*
    How does this work? Well the fractions shown in the problem. 3/2, 7/5, 
    17/12...The follow the pattern of new denominator = previous numerator + 
    previous denominator and the new numerator is the old numerator + 2 * the 
    old denominator. So you can extend that series. And you also don't need to 
    actually count the digits. If you take a log 10, that'll tell you the 
    number of digits (so long as you round down).
    *)
    let limit = 1000
    let answerT = 
        [|1..limit|]
        |> Array.fold (fun acc elem -> 
            let numerator, denominator, answer = acc
            let newDen = numerator + denominator
            let newNum = (2I * denominator) + numerator
            if (int)(BigInteger.Log10 numerator) > (int)(BigInteger.Log10 denominator)
                then (newNum, newDen, answer + 1)
                else (newNum, newDen, answer)
        ) (3I, 2I, 0)
    let numerator, denominator, answer = answerT
    answer |> intToString
