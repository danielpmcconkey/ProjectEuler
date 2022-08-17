module Euler068
open Algorithms
open Conversions

let run () =

    (*
    This isn't a whole lot like my C# version. Similar logic at the macro, but
    at the micro level, everything's a lot more functional. I used this as an
    opportunity to build my own permute function based on Heap's algorithm. I'd
    initially used a permutation function I stole off of Stack Overflow, but I 
    didn't really understand it and it only worked with lists. It was slow, 
    too.

    But I just couldn't convert my C# Heap's algorithm over to FP. It turns 
    out, that I still haven't. I sort of brute-force how to determine the 
    swaps. It still only uses the minimal movement as Heap's does, but it 
    doesn't use any of the cool recursion that Heap did. Maybe someday I'll 
    take another go at it. But this version runs just as fast and I can explain
    how it works.
    *)

    let toMagicRing (digits:int[]) =
        let node0Set = digits[0..2]
        let node3Set = [|digits[3]; digits[2]; digits[4]|]
        let node5Set = [|digits[5]; digits[4]; digits[6]|]
        let node7Set = [|digits[7]; digits[6]; digits[8]|]
        let node9Set = [|digits[9]; digits[8]; digits[1]|]
        [|(0, node0Set); (3, node3Set); (5, node5Set); (7, node7Set); (9, node9Set)|]
    let isValid (nodes:(int * int[])[]) =
        let sums = nodes |> Array.map (fun (i, n) -> n |> Array.sum)
        sums |> Array.forall (fun s -> s = sums[0])
    let nodeAtI (nodes:(int * int[])[]) i = 
        nodes 
        |> Array.filter (fun (num, node) -> num = i)
        |> (fun l -> l[0])
        |> snd
    let toDigits (nodes:(int * int[])[]) =
        let lowestNode = nodes |> Array.minBy (fun (i, n) -> n[0]) 
        let nodeOrder = 
            if fst lowestNode = 3 then   [|3; 5; 7; 9; 0|]
            elif fst lowestNode = 5 then [|5; 7; 9; 0; 3|]
            elif fst lowestNode = 7 then [|7; 9; 0; 3; 5|]
            else [|9; 0; 3; 5; 7|]
        nodeOrder 
        |> Array.map (fun i -> nodeAtI nodes i)
        |> Array.concat
        |> Array.map (fun d -> intToString d)
        |> Array.fold (+) ""
    
    [|1..9|] 
        |> permuteArray 
        |> Array.map (fun p -> Array.concat [|[|10|];p|])
        |> Array.map (fun p -> toMagicRing p)
        |> Array.filter (fun p -> isValid p)
        |> Array.map (fun p -> toDigits p)
        |> Array.max
