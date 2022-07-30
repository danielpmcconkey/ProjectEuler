module Euler038
open Conversions
open Algorithms

let run () = 
    
    let nByIProducts n =
        let unfolded =  
            (1, [])
            |> List.unfold (fun state -> 
                let i, l = state
                if l.Length >= 9 then None
                else
                    let product = n * i
                    let productList = intToListInt product
                    let next =(i + 1, l @ productList)
                    Some(next, next)
                )
        unfolded |> List.last |> snd

    let minN = 1
    let maxN = 9876
    [minN..maxN] 
    |> List.map (fun n -> nByIProducts n) 
    |> List.filter (fun l -> isListPandigital l)
    |> List.map (fun l -> l |> listIntToInt)
    |> List.max 
    |> intToString
