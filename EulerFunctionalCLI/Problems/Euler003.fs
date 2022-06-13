module Euler003

open Algorithms
open System

let run () =

    let answer = getPrimeFactorsOfLong 600851475143L |> Seq.max
    printfn "answer: %d" answer



