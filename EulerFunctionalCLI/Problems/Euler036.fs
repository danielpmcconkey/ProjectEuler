module Euler036
open Conversions
open Algorithms

let run () = 

    let limit = (int)1e6
    [1..(limit - 1)] 
    |> List.filter (fun i -> i |> isPalindromeBase10 && i |> isPalindromeBase2)
    |> List.sum
    |> intToString

