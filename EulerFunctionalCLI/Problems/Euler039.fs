module Euler039
open Conversions
open Algorithms

let run () =
    let maxPerimeter = 1000;

    getPythagoreanTriangles maxPerimeter 
    |> List.groupBy (fun t -> t.perimeter)
    |> List.maxBy (fun g -> (snd g).Length)
    |> fst
    |> intToString
