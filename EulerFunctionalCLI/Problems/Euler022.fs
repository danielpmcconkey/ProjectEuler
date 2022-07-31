module Euler022
open Conversions
open IO
let run () = 
    let sort a = Array.sort a    
    let scoreWord (w:string) = stringToChars w |> charsArrayToListInt |> List.sum
    let scoreWords (words:string[]) = [for i in 0..(words.Length - 1) do scoreWord words[i]]
    let multiplyScores (scores:List<int>) = [for i in 0..(scores.Length - 1) do scores[i] * (i + 1)]


    get22Input ()
    |> sort
    |> scoreWords
    |> multiplyScores
    |> List.sum
    |> intToString


