module Euler065

open DomainTypes
open FractionCalculator
open Conversions


let run () =

    let getECoefficientAtPosition p = 
        let mod3 = p % 3
        if mod3 = 0 then 1
        else if mod3 = 1 then 1
        else ((p / 3) + 1) * 2
    let getConvergenceAtCoefficient (priorFraction:FractionBig) (coefficient:int) = 
        // add the primary coefficient and invert
        priorFraction 
        |> addBig { numerator = coefficient; denominator = 1 }
        |> reciprocateBig


    let targetPosition = 100
    let starterFraction = { numerator = 0I; denominator = 1I }
    
    {1 .. (targetPosition - 1)}
    |> Seq.map (fun i -> getECoefficientAtPosition i)
    |> Seq.rev
    |> Seq.fold (fun x y -> getConvergenceAtCoefficient x y) starterFraction
    |> addBig { numerator = 2I; denominator = 1I }
    |> (fun f -> f.numerator)
    |> bigIntToIntArray
    |> Array.sum
    |> intToString