module IO
open System.IO
open System.Text.RegularExpressions
open Conversions

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
let get68Input () =
    @"E:\ProjectEuler\ExternalFiles\p067_triangle.txt"
    |> getAllLines
    |> Array.map (fun line ->
        line 
        |> splitOnSpace
        |> Array.map (fun s -> System.Int32.Parse s)
        )
let get79Input () =
    @"E:\ProjectEuler\ExternalFiles\p079_keylog.txt"
    |> getAllLines
    |> Array.map (fun line ->
        line 
        |> stringToInt
        |> intToIntArray
        )
