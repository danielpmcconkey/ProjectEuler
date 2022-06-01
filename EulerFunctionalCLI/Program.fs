open Algorithms


let names = ["Justin"; "Dan"; "Jodi"; "Rachel"]

let output2 =
    names
    |> Seq.map bigHello
    |> Seq.map lineBreak
    |> Seq.sort


let output3 = System.String.Concat(output2)

let output = prefix "Hello" "Dan"

printfn "%s" output3
