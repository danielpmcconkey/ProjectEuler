module Euler003

open Algorithms
open System

let run () =

    getPrimeFactorsOfLong 600851475143L |> Seq.max
    



