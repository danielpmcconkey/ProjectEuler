module Euler052
open Conversions
open Algorithms

let run () =
    let do2xAnd6xHaveTheSameNumDigits n = 
        (orderOfMagnitude n) = (orderOfMagnitude (n * 2)) && (orderOfMagnitude n) = (orderOfMagnitude (n * 6))
    let hasSameDigits a b = 
        let dig_a = intToIntArray a |> Array.sort
        let dig_b = intToIntArray b |> Array.sort
        [|0..(dig_a.Length - 1)|] |> Array.forall (fun i -> dig_a[i] = dig_b[i])


    let limit = (int) 1e6
    [|1..limit|]
    |> Array.filter (fun n -> do2xAnd6xHaveTheSameNumDigits n)
    |> Array.filter (fun n -> hasSameDigits n (n * 2))
    |> Array.filter (fun n -> hasSameDigits n (n * 3))
    |> Array.filter (fun n -> hasSameDigits n (n * 4))
    |> Array.filter (fun n -> hasSameDigits n (n * 5))
    |> Array.filter (fun n -> hasSameDigits n (n * 6))
    |> Array.min
    |> intToString