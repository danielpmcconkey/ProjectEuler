module Euler075

open Algorithms
open Conversions

let run () =
    (*
    This is the exact same problem as 39 but with a bigger max perimeter. The 
    code is almost identical.
    *)
    let maxPerimeter = (int)1.5e6

    getPythagoreanTriangles maxPerimeter 
    |> List.groupBy (fun t -> t.perimeter)
    |> List.filter (fun g -> (snd g).Length = 1)
    |> List.length
    |> intToString
