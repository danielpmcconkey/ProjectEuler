module Euler020
open System.Numerics

let run () =

    (*
    This problem lends itself so very well to FP. It's really just 5 lines of 
    code with some line breaks to make it easier to read. But it's 5 
    instructions.
    *)
    let factorial (n:int) = 
        [(1:BigInteger)..(n:BigInteger)] 
        |> List.fold (fun acc a -> acc * a) (1:BigInteger)
    let toString n = n.ToString()
    let toChars (s:string) = s.ToCharArray()
    let toNums (chars:char[]) =
        [for i in 0 .. (chars.Length - 1) do (int)chars[i] - (int)'0']

    factorial 100
    |> toString
    |> toChars
    |> toNums
    |> Seq.sum
    |> toString
