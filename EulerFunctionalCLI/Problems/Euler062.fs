module Euler062
open Conversions

let run () =
    let target = 5
    let minValToCube = 345L
    let maxValToCube = 10000L
    [minValToCube..maxValToCube]
    |> List.map (fun x -> 
        let xCubed = x * x * x
        let digits = xCubed |> longToCharArray |> Array.sort |> charArrayToString 
        (xCubed, digits)
        )
    |> List.groupBy (fun (xCubed, digits) -> digits)
    |> List.map (fun (digits, group) ->
        group |> List.map (fun (xCubed, digits) -> xCubed)
        )
    |> List.filter (fun group -> 
        let count = group |> List.length
        count = target
        )
    |> List.map (fun group -> group |> List.min)
    |> List.min
    |> longToString