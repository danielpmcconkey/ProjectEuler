module Euler066
open DomainTypes
open Algorithms
open Conversions
open FractionCalculator

let run () = 

    let doesXYDResolve (x:bigint) (y:bigint) (d:bigint) = if (x * x) - (d * y * y) = 1L then true else false 
    let fundamentalX d =
        let f = continuedFractionOfSqrtN d
        let cfToUse = { 
            firstCoefficient = f.firstCoefficient
            subsequentCoefficients = Array.concat [|f.subsequentCoefficients; f.subsequentCoefficients|]
            doCoefficientsRepeat = true
            }
        [|0..(cfToUse.subsequentCoefficients.Length - 1)|]
        |> Array.map (fun i -> continuedFractionConvergenceAtIBig cfToUse i)
        |> Array.tryFind (fun convergence ->
            let x = convergence.numeratorBig
            let y = convergence.denominatorBig
            let d64 = System.Numerics.BigInteger(d)
            doesXYDResolve x y d64
            )
        |> (fun o ->  match o with | Some(f:FractionBig)  -> (d, f.numeratorBig); | None -> (d, -1I)) 

    let limit = (int)1e5

    [|1..limit|]
    |> Array.filter (fun x -> isPerfectSquare x = false)
    |> Array.map (fun d -> fundamentalX d)
    |> Array.maxBy (fun (d, x) -> x)
    |> fst
    |> intToString


