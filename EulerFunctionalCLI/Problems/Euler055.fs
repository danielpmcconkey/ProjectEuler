module Euler055
open Conversions
open Algorithms
let run () = 

    (*
    I should have thought to use recursion instead of the unfold.
    *)

    let reverse (b: bigint) = 
        let bArr = b |> bigIntToCharArray
        let chars = 
            [|0..(bArr.Length - 1)|]
            |> Array.map (fun i -> bArr[bArr.Length - 1 - i])
        let s = System.String.Join("", chars)
        System.Numerics.BigInteger.Parse(s)

    let target = 50
    let limit = (int)1e4
    [|1..limit|]
    |> Array.map (fun i ->
        let unfolded = 
            (System.Numerics.BigInteger(i), 1)
            |> Array.unfold (fun state ->
                    let current, count = state
                    if count > target then None
                    elif count > 1 && (current |> bigIntToString |> isPalindromeString) then None
                    else
                        let next = (current + (reverse current), count + 1)
                        Some(state, next)
                )
        (i, unfolded |> Array.length)
        )
    |> Array.filter (fun (i, count) -> count >= target)
    |> Array.length
    |> intToString