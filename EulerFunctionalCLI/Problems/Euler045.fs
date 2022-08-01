module Euler045
open Conversions
open Algorithms

let run ()  =

    (*
    This was trivially easy after the last problem. I already had the 
    isPentagonal function in my Algorithms library and I already knew how to 
    use tryFind.
    *)
    let getAnswerFromOption o = match o with | Some(n) -> n | None -> -1

    let min = 144
    let max = (int)1e6
    [min..max]
    |> List.map (fun n -> n * ((2 * n) - 1))
    |> List.tryFind (fun x -> isPentagonal x)
    |> getAnswerFromOption
    |> intToString
