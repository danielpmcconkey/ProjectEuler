module Euler085
open Conversions

let run () =
    let triangleOfN n = n * (n + 1) / 2
    let triangleMN m n = triangleOfN m * triangleOfN n
    let diff target m n = abs (target - triangleMN m n)
    let target = (int)2e6
    let minSide = 30
    let maxSide = 90
    [|minSide..maxSide|]
    |> Array.collect (fun m -> 
        [|m..maxSide|]
        |> Array.map (fun n -> (m, n))
        )
    |> Array.map (fun (m, n) -> (m, n, diff target m n))
    |> Array.minBy (fun (m, n, difference) -> difference)
    |> (fun (m, n, difference) -> m * n)
    |> intToString
