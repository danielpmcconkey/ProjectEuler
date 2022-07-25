module Euler030

let run () =

    (*
    This one shows off function composition really well. A couple of easy 
    functions and used in the right sequence can produce something that is both
    powerful and easy to read.
    *)

    let toString n = n.ToString()
    let floatToInt f = (int)f
    let toFifth n = (float)n ** 5.0
    let intToDigitList n = n.ToString().ToCharArray() |> Array.toList |> List.map (fun c -> (int)c - (int)'0')
    let listToTheFifth l = l |> List.map (fun x -> x |> toFifth |> floatToInt)
    let isFancy n = n |> intToDigitList |> listToTheFifth |> List.sum = n

    let lowLimit, hiLimit = 2, (int)1e6
    [lowLimit..hiLimit] |> List.filter (fun x -> isFancy x) |> List.sum |> toString
