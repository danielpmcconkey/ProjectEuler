module Euler044
open Conversions
open Algorithms

let run () =

(*
This was a very hard conversion for me. I first wrote a solution that took 
forever to run (upwards of 20 mins?). It cross joined the list of pentagonal 
numbers and tried to filter on the sum and differences both being pentagonal. 
It tried creating a bool[] for isPentagonal by mapping 0..(some big number) to 
see if "i" was in my list of pentagonal numbers. It took a minute just to 
create that list.

I went through various iterations. Some with varying degrees of idiomaticness. 
One of those, I tried using recursion to break when I found a solution and kept
getting a stack overflow. That finally lead me to the tryFind method for 
getting the first success. I wanted to run a tryFind on j and another on k but 
I couldn't keep the K value--only that j had a success--without using a mutable
integer. I toyed with unfolding the answer, but decided I wasn't using that 
method properly and wanted to do it right.

In the end, I landed on a very FP idomatic solution and I think the tryFind was
the correct way to do it. Now I need to learn how to deal with options better.
*)

    let getAnswerFromOption o = match o with | Some((j,k)) -> k - j; | None -> -1
    let calcPentAtN n = (int)(round (((n * ((3.0 * n) - 1.0)) * 0.5)))
    let getPentagonalNums limit = [|1.0..((float)limit)|] |> Array.map (fun i -> calcPentAtN i)

    let limit = 2500
    let pentNums = getPentagonalNums limit
    pentNums 
    |> Array.collect (fun x -> pentNums |> Array.filter (fun y -> y > x) |> Array.map (fun z -> x,z))
    |> Array.tryFind (fun (j, k) -> if isPentagonal (j + k) && isPentagonal (k - j) then true else false)
    |> getAnswerFromOption 
    |> intToString 
