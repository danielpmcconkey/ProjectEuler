module Euler071

open DomainTypes
open Conversions
open FractionCalculator


let run () =
    (*
    This is a very different approach from the C# solution, learned from 
    reading the problem write-up.
    
    For each potential denominator, the numerator that forms a fraction just 
    less than 3/7 can be expressed as follows:

         num               3
        -----      <     -----
         den               7

    Or, said another way, you want the whole number just less than:

         num               3
        -----      =     -----
         den               7

    So my program takes all the possible denominators from 2 to 1MM, takes a 
    ceiling of the equation above, then subtracts 1. I then take the max of the 
    set and reduce it.

    Runs in about 100 ms.
    *)

    let leftOf = {numerator = 3; denominator = 7}
    let limit = (int)1e6

    [|2..limit|]
    |> Array.map (fun denominator -> 
        { 
            numerator = (int)(ceil (
                (float)denominator * 
                (float)leftOf.numerator / 
                (float)leftOf.denominator)
                ) - 1
            denominator = denominator 
        })
    |> Array.maxBy (fun f -> (float)f.numerator / (float)f.denominator)
    |> (fun f -> reduce f)
    |> (fun f -> f.numerator)
    |> intToString