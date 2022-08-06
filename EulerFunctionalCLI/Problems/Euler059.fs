module Euler059

open System.Collections.Generic
open IO
open Conversions

let run () = 
    let decrypt (text:int[]) (password:int[]) = 
        let tLength = text.Length
        [|0..(tLength - 1)|]
        |> Array.map (fun i -> 
            let pwInt = password[i % 3]
            let inputInt = text[i]
            let intXor = inputInt ^^^ pwInt
            (char)intXor 
            )
    let score (decrypted:char[]) (expectedLetterFrequencies:IDictionary<char,float>) = 
        let uppers = decrypted |> Array.map (fun c -> c |> charToUpper)
        let lettersOnly = uppers |> Array.filter (fun c -> (int)c >= (int)'A' && (int)c <= (int)'Z')
        let totalCount = lettersOnly.Length
        [|(int)'A'..(int)'Z'|]
        |> Array.map (fun i ->
            let charToCHeck = (char)i
            let expected = expectedLetterFrequencies[charToCHeck]
            let actual = lettersOnly |> Array.filter (fun c -> c = charToCHeck) |> Array.length
            let ratio = if actual > 0 then (float)actual / (float)totalCount else 0.0
            abs (expected - ratio)
            )
        |> Array.sum
    let expectedLetterFrequencies: IDictionary<char, float> = //:Dictionary<char, float> =
        dict[
        // https://en.wikipedia.org/wiki/Letter_frequency
        'A', 0.082
        'B', 0.015
        'C', 0.028
        'D', 0.043
        'E', 0.13
        'F', 0.022
        'G', 0.02
        'H', 0.061
        'I', 0.07
        'J', 0.002
        'K', 0.008
        'L', 0.04
        'M', 0.024
        'N', 0.067
        'O', 0.075
        'P', 0.019
        'Q', 0.001
        'R', 0.06
        'S', 0.063
        'T', 0.091
        'U', 0.028
        'V', 0.01
        'W', 0.024
        'X', 0.002
        'Y', 0.02
        'Z', 0.001]

    let inputInts =
        get59Input ()
        |> Array.map (fun x -> stringToInt x)
    let letters = [|(int)'a'..(int)'z'|]
    let passKeys = 
        letters 
        |> Array.collect (fun char1 ->
            letters 
            |> Array.collect (fun char2 ->
                letters
                |> Array.map (fun char3 -> [|char1; char2; char3|])
                )
            )
    passKeys
    |> Array.map (fun p -> decrypt inputInts p)
    |> Array.map (fun m -> (m, score m expectedLetterFrequencies))
    |> Array.minBy (fun (m, score) -> score)
    |> fst
    |> Array.map (fun c -> (int)c)
    |> Array.sum
    |> intToString