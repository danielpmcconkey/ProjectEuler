module Euler016
open System.Numerics

let run () =

    (*
    This one was boring. There's nothing hard if your language / framework has
    a big number capability. Nothing interesting about converting to F# either.
    *)
    let toString (n:BigInteger) = n.ToString()
    let toChars (s:string) = s.ToCharArray()
    let toNums (c:char[]) = 
        seq {for i in 0..(c.Length - 1) do (int)c[i] - (int)'0' }

    let start = 2:BigInteger
    let power = 1000
    
    let answer = 
        seq { 2..power } 
        |> Seq.fold (fun acc elem -> acc * (2:BigInteger)) start
        |> toString
        |> toChars
        |> toNums
        |> Seq.sum

    answer.ToString()


