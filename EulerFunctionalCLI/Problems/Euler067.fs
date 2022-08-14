module Euler067
open Conversions
open IO
let run () = 
    (*
    This is the same logic as the C# solution. Only, instead of updating the 
    triangle as I go, I use recursion and send each row into the next call.
    *)
    let rec calcRow (rows: int[][]) (i:int) (row:int[]) (rowBelow:int[]) =
        let newRow = 
            [|0..(row.Length - 1)|]
            |> Array.map (fun i ->
                let left = rowBelow[i]
                let right = rowBelow[i + 1]
                if left >= right then row[i] + left else row[i] + right
                )
        if i = 0 then newRow
        else calcRow rows (i - 1) rows[i - 1] newRow 
    let rows = get68Input ()
    let result = calcRow rows (rows.Length - 2) (rows[rows.Length - 2]) (rows[rows.Length - 1])
    result[0] |> intToString