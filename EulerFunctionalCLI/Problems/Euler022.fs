module Euler022

open System.Text.RegularExpressions
open System.IO
let run () = 
    let removeQuotes s = Regex.Replace(s, "\"", "")
    let split (s:string) = s.Split ","
    let sort a = Array.sort a
    let getInput filePath = File.ReadAllText(filePath)
    let toChars (w:string) = w.ToCharArray()
    let toNums (chars:char[]) = [for i in 0..(chars.Length - 1) do (int)chars[i] - (int)'A' + 1]
    let scoreWord (w:string) = toChars w |> toNums |> List.sum
    let scoreWords (words:string[]) = [for i in 0..(words.Length - 1) do scoreWord words[i]]
    let multiplyScores (scores:List<int>) = [for i in 0..(scores.Length - 1) do scores[i] * (i + 1)]
    let toString n = n.ToString()

    let filePath = @"E:\ProjectEuler\ExternalFiles\p022_names.txt"

    getInput filePath
    |> removeQuotes
    |> split
    |> sort
    |> scoreWords
    |> multiplyScores
    |> List.sum
    |> toString


