module Euler064

open DomainTypes
open Algorithms
open FractionCalculator
open Conversions

let run () =
    (*
    This is a pretty cheap recursive function that runs until the next 
    coefficient is twice the first. Read the comments in the C# version if you
    want to know the journey I went down to figure out how to calculate the 
    continued fraction. For ease, I eventually landed on this:

        https://web.archive.org/web/20151221205104/http://web.math.princeton.edu/mathlab/jr02fall/Periodicity/mariusjp.pdf

    My F# implementation differs from C# in that it uses recursive logic 
    instead of a while loop and I also decided that I didn't need the 
    palindrome check. That turns out to just be a neat-o thing all these 
    continued fractions have in common.
    *)
    let limit = 10000
    [|1..limit|]
    |> Array.filter (fun x -> isPerfectSquare x = false)
    |> Array.map (fun x -> continuedFractionOfSqrtN x)
    |> Array.filter (fun x -> x.subsequentCoefficients.Length % 2 = 1)
    |> Array.length
    |> intToString