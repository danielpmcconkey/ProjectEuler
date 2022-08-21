module Euler073
open DomainTypes
open Conversions

let run () =
    (*
    This uses the idea of Farey sequences described in the overview PDF posted 
    to the problem. This is not how I originally solved the problem but it is 
    how I did my F# conversion. I also don't understand why this works at this 
    point. The math in that write was a little heavy.

    Runs in 194 ms
    *)

    let rec howMany f1 f2 count limit targetRightFraction =
        if f2 = targetRightFraction then count
        else
            let k = (limit + f1.denominator) / f2.denominator
            let e = (k * f2.numerator) - f1.numerator // k * c - a
            let f = (k * f2.denominator) - f1.denominator // k * d - b
            let newF1 = {numerator = f2.numerator; denominator = f2.denominator} // a = c; b = d
            let newF2 = {numerator = e; denominator = f} // c = e; d = f
            let newCount = count + 1
            howMany newF1 newF2 newCount limit targetRightFraction
    let limit = 12000
    let initalLeftFraction = {numerator = 1; denominator = 3}
    let targetRightFraction = {numerator = 1; denominator = 2}
    let k_0 = (limit - targetRightFraction.denominator) / initalLeftFraction.denominator
    let e_0 = targetRightFraction.numerator + k_0
    let f_0 = targetRightFraction.denominator + k_0 * initalLeftFraction.denominator 
    let initialRightFraction = {numerator = e_0; denominator = f_0}
    howMany initalLeftFraction initialRightFraction 0 limit targetRightFraction
    |> intToString