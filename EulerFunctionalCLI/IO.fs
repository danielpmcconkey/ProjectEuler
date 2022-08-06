module IO
open System.IO
open System.Text.RegularExpressions

// formatting helper functions
let getAllText filePath = File.ReadAllText(filePath)
let getAllLines filePath = File.ReadAllLines(filePath)
let removeQuotes s = Regex.Replace(s, "\"", "")
let split (s:string) = s.Split ","
let splitOnSpace (s:string) = s.Split " "

// end formatting helpers

let get22Input () =
    @"E:\ProjectEuler\ExternalFiles\p022_names.txt"
    |> getAllText 
    |> removeQuotes
    |> split
let get42Input () =
    @"E:\ProjectEuler\ExternalFiles\p042_words.txt"
    |> getAllText 
    |> removeQuotes
    |> split
let get54Input () =
    @"E:\ProjectEuler\ExternalFiles\p054_poker.txt"
    |> getAllLines
let get59Input () =
    @"E:\ProjectEuler\ExternalFiles\p059_cipher.txt"
    |> getAllText
    |> split